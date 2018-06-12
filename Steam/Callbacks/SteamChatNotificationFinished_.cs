using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SteamChatNotificationFinished_ : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.SteamChatNotificationFinished_;

        public uint m_uiIndex;
        public uint m_uiUnknown;
        public uint m_uiUnknown2;
        public uint m_iUnknown3;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
