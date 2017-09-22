using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DownloadScheduleChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.DownloadScheduleChanged;

        public bool m_bDownloadEnabled;
        public uint m_nTotalAppsScheduled;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public uint[] m_rgunAppSchedule;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
