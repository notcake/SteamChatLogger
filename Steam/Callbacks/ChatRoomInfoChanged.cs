using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomInfoChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatRoomInfoChanged;

        public CSteamID m_ulSteamIDChat;
        public uint m_rgfChatRoomDetails;
        private uint m_Padding;
        public CSteamID m_ulSteamIDMakingChange; // Cannot use CSteamID here due to padding issues.

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
