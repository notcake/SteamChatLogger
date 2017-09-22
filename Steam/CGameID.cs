using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct CGameID
    {
        [FieldOffset(0)]
        public uint m_low;
        [FieldOffset(3)]
        public EGameIDType m_nType;
        [FieldOffset(4)]
        public uint m_nModID;

        public AppId m_nAppID
        {
            get { return new AppId(this.m_low & 0x00FFFFFF); }
            set { this.m_low = (this.m_low & 0xFF000000) | value.Value; }
        }
    }
}
