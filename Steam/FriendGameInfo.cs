using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FriendGameInfo
    {
        public CGameID m_gameID;
        public uint m_unGameIP;
        public ushort m_usGamePort;
        public ushort m_usQueryPort;
        public CSteamID m_steamIDLobby;

        public IPAddress m_GameIP
        {
            get
            {
                uint ip = ((this.m_unGameIP & 0xFF) << 24) |
                          (((this.m_unGameIP >>  8) & 0xFF) << 16) |
                          (((this.m_unGameIP >> 16) & 0xFF) <<  8) |
                          ((this.m_unGameIP >> 24));
                return new IPAddress(ip);
            }
        }
    }
}
