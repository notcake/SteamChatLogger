﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HVoiceCall
    {
        public uint Value;

        public static readonly HVoiceCall Null = new HVoiceCall { Value = 0 };
    }
}
