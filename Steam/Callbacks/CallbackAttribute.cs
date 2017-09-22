using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CallbackAttribute : Attribute
    {
        public CallbackAttribute(Type structType)
        {
            this.StructType = structType;
        }

        public Type StructType { get; }
    }
}
