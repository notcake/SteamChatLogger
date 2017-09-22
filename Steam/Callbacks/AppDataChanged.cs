﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AppDataChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.AppDataChanged;

        public AppId m_nAppID;

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
