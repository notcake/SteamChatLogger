using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HSteamPipe
    {
        public uint Value;

        public static readonly HSteamPipe Null = new HSteamPipe { Value = 0 };
    }
}
