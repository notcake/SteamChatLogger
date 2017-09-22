using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public static class Interface
    {
        public static T Bind<T>(IntPtr pInstance)
            where T : class
        {
            if (pInstance == IntPtr.Zero) { return null; }

            Type classType = Interface.CreateClassFromInterface(typeof(T));
            T instance = (T)classType.GetConstructor(Type.EmptyTypes).Invoke(null);

            classType.GetField("Pointer", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, pInstance);

            MethodInfo[] methods = typeof(T).GetMethods().OrderBy(x => x.MetadataToken).ToArray();
            IntPtr[] vtable = new IntPtr[methods.Length];
            IntPtr pVtable = Marshal.ReadIntPtr(pInstance);
            Marshal.Copy(pVtable, vtable, 0, methods.Length);
            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo methodInfo = methods[i];
                FieldInfo fieldInfo = classType.GetField("_" + methodInfo.Name, BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(instance, Marshal.GetDelegateForFunctionPointer(vtable[i], fieldInfo.FieldType));
            }

            return instance;
        }

        public static string GetName<T>()
            where T : class
        {
            return Interface.GetName(typeof(T));
        }

        public static string GetName(Type interfaceType)
        {
            foreach (CustomAttributeData customAttributeData in interfaceType.GetCustomAttributesData())
            {
                if (customAttributeData.AttributeType == typeof(InterfaceNameAttribute))
                {
                    return customAttributeData.ConstructorArguments.FirstOrDefault().Value as string;
                }
            }

            return null;
        }

        private static AssemblyBuilder AssemblyBuilder = null;
        private static ModuleBuilder ModuleBuilder = null;
        private static uint DelegateTypeId = 0;

        private static void Initialize()
        {
            if (Interface.AssemblyBuilder != null) { return; }

            Interface.AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Steam.VTable.ClassGenerator"), AssemblyBuilderAccess.Run);
            Interface.ModuleBuilder = Interface.AssemblyBuilder.DefineDynamicModule("Steam.VTable.ClassGenerator");
        }

        private static Dictionary<Type, Type> InterfaceClassCache = new Dictionary<Type, Type>();
        private static Type CreateClassFromInterface(Type interfaceType)
        {
            if (Interface.InterfaceClassCache.ContainsKey(interfaceType))
            {
                return Interface.InterfaceClassCache[interfaceType];
            }

            Interface.Initialize();

            TypeBuilder typeBuilder = Interface.ModuleBuilder.DefineType(
                "Steam.VTable.ClassGenerator." + interfaceType.Name.Substring(1),
                TypeAttributes.Public
            );
            typeBuilder.AddInterfaceImplementation(interfaceType);

            FieldBuilder pointerFieldBuilder = typeBuilder.DefineField("Pointer", typeof(IntPtr), FieldAttributes.Private);

            MethodInfo[] methods = interfaceType.GetMethods();
            foreach (MethodInfo methodInfo in methods)
            {
                Type delegateType = Interface.CreateDelegateTypeFromMethod(methodInfo);

                FieldBuilder delegateFieldBuilder = typeBuilder.DefineField("_" + methodInfo.Name, delegateType, FieldAttributes.Private);
                MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, methodInfo.Attributes & ~MethodAttributes.Abstract, methodInfo.ReturnType, methodInfo.GetParameters().Select(x => x.ParameterType).ToArray());
                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
                ILGenerator ilGenerator = methodBuilder.GetILGenerator();
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, delegateFieldBuilder);
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, pointerFieldBuilder);

                ParameterInfo[] parameters = methodInfo.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    ilGenerator.Emit(OpCodes.Ldarg_S, i + 1);
                }
                ilGenerator.Emit(OpCodes.Callvirt, delegateType.GetMethod("Invoke"));

                if (methodInfo.ReturnType == typeof(string))
                {
                    ilGenerator.Emit(OpCodes.Call, typeof(Interface).GetMethod("PtrToStringUtf8"));
                }

                ilGenerator.Emit(OpCodes.Ret);
            }

            Type classType = typeBuilder.CreateType();
            Interface.InterfaceClassCache[interfaceType] = classType;
            return classType;
        }

        private static Type CreateDelegateTypeFromMethod(MethodInfo methodInfo)
        {
            Interface.Initialize();

            TypeBuilder typeBuilder = Interface.ModuleBuilder.DefineType(
                "Steam.VTable.ClassGenerator.Delegate$" + Interface.DelegateTypeId.ToString(),
                TypeAttributes.AutoClass | TypeAttributes.Sealed | TypeAttributes.Public,
                typeof(MulticastDelegate)
            );
            Interface.DelegateTypeId++;
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
                CallingConventions.Standard,
                new Type[] { typeof(object), typeof(IntPtr) }
            );
            constructorBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            ParameterInfo[] parameters = methodInfo.GetParameters();
            Type[] parameterTypes = new Type[parameters.Length + 1];
            parameterTypes[0] = typeof(IntPtr);
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterTypes[i + 1] = parameters[i].ParameterType;
            }
            Type returnType = methodInfo.ReturnType;
            if (returnType == typeof(string))
            {
                returnType = typeof(IntPtr);
            }
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "Invoke",
                MethodAttributes.VtableLayoutMask | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public,
                returnType,
                parameterTypes
            );
            methodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(0, methodInfo.ReturnParameter.Attributes, methodInfo.ReturnParameter.Name);
            Interface.CopyParameterAttributes(parameterBuilder, methodInfo.ReturnParameter);
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterBuilder = methodBuilder.DefineParameter(i + 2, parameters[i].Attributes, parameters[i].Name);
                Interface.CopyParameterAttributes(parameterBuilder, parameters[i]);
            }

            // [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            ConstructorInfo constructorInfo = typeof(UnmanagedFunctionPointerAttribute).GetConstructor(new[] { typeof(CallingConvention) });
            typeBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructorInfo, new object[] { CallingConvention.ThisCall }));

            return typeBuilder.CreateType();
        }

        public static string PtrToStringUtf8(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) { return null; }

            int length = 0;
            while (Marshal.ReadByte(ptr, length) != 0) { length++; }
            byte[] bytes = new byte[length];
            Marshal.Copy(ptr, bytes, 0, length);
            return Encoding.UTF8.GetString(bytes);
        }

        private static CustomAttributeBuilder CreateCustomAttributeBuilderFromData(CustomAttributeData customAttributeData)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            List<object> propertyValues = new List<object>();
            List<FieldInfo> fields = new List<FieldInfo>();
            List<object> fieldValues = new List<object>();
            foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
            {
                if (customAttributeNamedArgument.IsField)
                {
                    fields.Add((FieldInfo)customAttributeNamedArgument.MemberInfo);
                    fieldValues.Add(customAttributeNamedArgument.TypedValue.Value);
                }
                else
                {
                    properties.Add((PropertyInfo)customAttributeNamedArgument.MemberInfo);
                    propertyValues.Add(customAttributeNamedArgument.TypedValue.Value);
                }
            }

            return new CustomAttributeBuilder(customAttributeData.Constructor, customAttributeData.ConstructorArguments.Select(x => x.Value).ToArray(), properties.ToArray(), propertyValues.ToArray(), fields.ToArray(), fieldValues.ToArray());
        }

        private static void CopyParameterAttributes(ParameterBuilder destination, ParameterInfo source)
        {
            foreach (CustomAttributeData customAttributeData in source.CustomAttributes)
            {
                destination.SetCustomAttribute(Interface.CreateCustomAttributeBuilderFromData(customAttributeData));
            }
        }
    }
}
