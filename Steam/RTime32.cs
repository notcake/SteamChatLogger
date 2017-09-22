using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RTime32
    {
        public uint Value;

        public static RTime32 Now => new RTime32 { Value = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
    }
}
