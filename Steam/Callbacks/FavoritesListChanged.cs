using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FavoritesListChanged : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.FavoritesListChanged;

        public uint m_nIP; // an IP of 0 means reload the whole list, any other value means just one server
        public uint m_nQueryPort;
        public uint m_nConnPort;
        public AppId m_nAppID;
        public uint m_nFlags;
        public bool m_bAdd; // true if this is adding the entry, otherwise it is a remove

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
