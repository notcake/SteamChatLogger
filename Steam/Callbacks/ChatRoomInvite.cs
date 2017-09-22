using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomInvite : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatRoomInvite;

        public CSteamID m_ulSteamIDChat;
        public CSteamID m_ulSteamIDPatron;
        public CSteamID m_ulSteamIDFriendChat;

        public EChatRoomType m_EChatRoomType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_rgchChatRoomName;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
