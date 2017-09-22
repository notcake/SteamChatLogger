using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AppLifetimeNotice : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.AppLifetimeNotice;

        public AppId m_nAppID;
        public int m_nInstanceID;
        public bool m_bExiting;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
