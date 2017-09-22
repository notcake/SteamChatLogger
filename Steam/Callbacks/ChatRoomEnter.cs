using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomEnter : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatRoomEnter;

        public CSteamID m_ulSteamIDChat;

        public EChatRoomType m_EChatRoomType;
        private uint m_Padding;

        public CSteamID m_ulSteamIDOwner;
        public CSteamID m_ulSteamIDClan;
        public CSteamID m_ulSteamIDFriendChat;

        public bool m_bLocked;
        public EChatPermission m_rgfChatPermissions;
        public EChatRoomEnterResponse m_EChatRoomEnterResponse;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_rgchChatRoomName;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
