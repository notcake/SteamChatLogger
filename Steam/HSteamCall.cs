using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HSteamCall
    {
        public int Value;

        public static readonly HSteamCall Null = new HSteamCall { Value = 0 };
    }
}
