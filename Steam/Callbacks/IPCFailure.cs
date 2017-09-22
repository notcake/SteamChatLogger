using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct IPCFailure : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.IPCFailure;
        
        public EFailureType m_eFailureType;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
