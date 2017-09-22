using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomMsg : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ChatRoomMsg;

        public CSteamID m_ulSteamIDChat;
        public CSteamID m_ulSteamIDUser;
        private byte _m_eChatEntryType;
        public int m_iChatID;

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
