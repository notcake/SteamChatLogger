using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct UserRequestingFriendship : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.UserRequestingFriendship;

        public CSteamID m_ulSteamID; // steamID of the friend who just added us

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
