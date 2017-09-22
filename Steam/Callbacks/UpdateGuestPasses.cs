using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct UpdateGuestPasses : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.UpdateGuestPasses;

        public EResult m_eResult;

        public uint m_cGuestPassesToGive;
        public uint m_cGuestPassesToRedeem;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
