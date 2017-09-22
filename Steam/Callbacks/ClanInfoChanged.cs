using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ClanInfoChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ClanInfoChanged;

        public CSteamID m_ulSteamID;

        [MarshalAs(UnmanagedType.U1)]
        public bool m_bNameChanged;
        [MarshalAs(UnmanagedType.U1)]
        public bool m_bAvatarChanged;
        [MarshalAs(UnmanagedType.U1)]
        public bool m_bAccountInfoChanged;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
