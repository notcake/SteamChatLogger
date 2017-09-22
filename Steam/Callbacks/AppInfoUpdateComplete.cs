using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AppInfoUpdateComplete : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.AppInfoUpdateComplete;

        public EResult m_EResult;
        public uint m_cAppsUpdated;
        public bool m_bSteam2CDDBChanged;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
