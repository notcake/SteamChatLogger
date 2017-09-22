using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendMessageHistoryChatLog : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.FriendMessageHistoryChatLog;

        public EResult m_EResult;
        public CSteamID m_ulFriendID;
        public int m_iFirstMessageID;
        public int m_iLastMessageID;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
