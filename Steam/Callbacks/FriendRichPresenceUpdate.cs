using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendRichPresenceUpdate : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.FriendRichPresenceUpdate;

        public CSteamID m_steamIDFriend; // friend who's rich presence has changed
        public AppId m_nAppID;           // the appID of the game (should always be the current game)

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
