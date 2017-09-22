using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct WebAuthRequestCallback : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.WebAuthRequestCallback;

        [MarshalAs(UnmanagedType.U1)]
        public bool m_bSuccessful;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] m_rgchToken;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
