using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendChatMsg : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.FriendChatMsg;

        public CSteamID m_ulFriendID;   // other participant in the msg
        public CSteamID m_ulSenderID;   // steamID of the friend who has sent this message
        private byte _m_eChatEntryType;
        public byte m_bLimitedAccount;
        public int m_iChatID;           // chat id

        public EChatEntryType m_eChatEntryType
        {
            get { return (EChatEntryType)this._m_eChatEntryType; }
            set { this._m_eChatEntryType = (byte)value; }
        }

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
