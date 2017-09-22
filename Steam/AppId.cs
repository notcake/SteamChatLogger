using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AppId
    {
        public uint Value;

        public AppId(uint value) { this.Value = value; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
