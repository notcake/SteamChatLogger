using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct LoginInformationChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.LoginInformationChanged;

        public CSteamID m_steamID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] m_username;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
