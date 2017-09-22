using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NotifyChatRoomVoiceStateChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.NotifyChatRoomVoiceStateChanged;

        public CSteamID m_steamChatRoom;
        public CSteamID m_steamUser;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
