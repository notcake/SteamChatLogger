using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamChatLogger.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HMODULE : IEquatable<HMODULE>
    {
        public IntPtr Value;

        #region Object
        public override int GetHashCode() { return this.Value.GetHashCode(); }

        public override bool Equals(object obj)
        {
            if (!(obj is HMODULE)) { return false; }

            return this == (HMODULE)obj;
        }
        #endregion

        #region IEquatable<HMODULE>
        public bool Equals(HMODULE hModule) { return this.Value == hModule.Value; }
        #endregion

        public static bool operator ==(HMODULE a, HMODULE b) { return a.Value == b.Value; }
        public static bool operator !=(HMODULE a, HMODULE b) { return a.Value != b.Value; }

        public static readonly HMODULE Null = new HMODULE { Value = IntPtr.Zero };
    }
}
