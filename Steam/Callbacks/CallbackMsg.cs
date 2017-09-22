using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CallbackMsg
    {
        public HSteamUser m_hSteamUser;
        public CallbackType m_iCallback;
        public IntPtr m_pubParam;
        public int m_cubParam;

        public byte[] GetRawData()
        {
            byte[] data = new byte[this.m_cubParam];
            Marshal.Copy(this.m_pubParam, data, 0, this.m_cubParam);
            return data;
        }

        public ICallbackData GetCallbackData()
        {
            FieldInfo fieldInfo = typeof(CallbackType).GetField(this.m_iCallback.ToString());
            if (fieldInfo == null) { return null; }
            CallbackAttribute callbackAttribute = fieldInfo.GetCustomAttribute<CallbackAttribute>();
            if (callbackAttribute == null) { return null; }
            
            ICallbackData callbackData = (ICallbackData)Marshal.PtrToStructure(this.m_pubParam, callbackAttribute.StructType);

            int marshalSize = Marshal.SizeOf(callbackAttribute.StructType);
            if (marshalSize != this.m_cubParam)
            {
                Debug.WriteLine("CallbackMsg.GetCallbackData : " + this.m_iCallback.ToString() + " has " + this.m_cubParam.ToString() + " B, but only " + marshalSize.ToString() + " B is known.");
                Debug.WriteLine("\t" + BitConverter.ToString(this.GetRawData()).Replace('-', ' '));
            }

            return callbackData;
        }

        public T GetCallbackData<T>()
            where T : ICallbackData
        {
            return (T)this.GetCallbackData();
        }
    }
}
