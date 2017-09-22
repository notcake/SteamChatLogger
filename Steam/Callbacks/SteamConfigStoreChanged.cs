using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SteamConfigStoreChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.SteamConfigStoreChanged;

        public EConfigStore m_eConfigStore;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public char[] m_szRootOfChanges;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
