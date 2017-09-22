using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AppEventStateChange : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.AppEventStateChange;

        public AppId m_nAppID;
        public uint m_eOldState;
        public uint m_eNewState;
        public EAppUpdateError m_eAppError;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
