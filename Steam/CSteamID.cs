using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CSteamID : IEquatable<CSteamID>
    {
        public uint m_unAccountID;
        public uint m_high;

        public uint m_unAccountInstance
        {
            get { return this.m_high & 0x000FFFFF; }
            set { this.m_high = (this.m_high & 0xFFF00000) | value; }
        }

        public EAccountType m_EAccountType
        {
            get { return (EAccountType)((this.m_high >> 20) & 0x0F); }
            set { this.m_high = (this.m_high & 0xFF0FFFFF) | ((uint)value << 20); }
        }

        public EUniverse m_EUniverse
        {
            get { return (EUniverse)(this.m_high >> 24); }
            set { this.m_high = (this.m_high & 0x00FFFFFF) | ((uint)value << 24); }
        }

        public ulong UInt64 => this.m_unAccountID + (((ulong)this.m_high) << 32);

        public CSteamID(ulong value)
        {
            this.m_unAccountID = (uint)(value & 0x00000000FFFFFFFF);
            this.m_high        = (uint)(value >> 32);
        }

        #region Object
        public override bool Equals(object obj)
        {
            if (!(obj is CSteamID)) { return false; }

            return this == (CSteamID)obj;
        }

        public override int GetHashCode()
        {
            return this.UInt64.GetHashCode();
        }
        #endregion

        #region IEquatable<CSteamID>
        public bool Equals(CSteamID other)
        {
            return this.m_unAccountID == other.m_unAccountID && this.m_high == other.m_high;
        }
        #endregion

        public static bool operator ==(CSteamID a, CSteamID b) { return a.m_unAccountID == b.m_unAccountID && a.m_high == b.m_high; }
        public static bool operator !=(CSteamID a, CSteamID b) { return a.m_unAccountID != b.m_unAccountID || a.m_high != b.m_high; }
    }
}
