using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendAdded : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.FriendAdded;
        
        public EResult m_eResult;
        public CSteamID m_ulSteamID; // steamID of the friend who was just added

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
