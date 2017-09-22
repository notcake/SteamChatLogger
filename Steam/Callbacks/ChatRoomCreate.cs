using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomCreate : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatRoomCreate;

        public EResult m_eResult;
        private uint m_Padding;

        public CSteamID m_ulSteamIDChat;
        public CSteamID m_ulSteamIDFriendChat;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
