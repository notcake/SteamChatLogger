using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ComputerActiveStateChange : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.ComputerActiveStateChange;
        
        public uint m_uiUnknown;
        public uint m_uiTimestamp;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
