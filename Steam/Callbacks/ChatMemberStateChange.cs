using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatMemberStateChange : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatMemberStateChange;

        public CSteamID m_ulSteamIDChat;
        public CSteamID m_ulSteamIDUserChanged;
        public EChatMemberStateChange m_rgfChatMemberStateChange;
        private uint m_Padding;
        public CSteamID m_ulSteamIDMakingChange;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
