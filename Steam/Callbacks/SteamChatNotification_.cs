using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SteamChatNotification_ : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.SteamChatNotification_;
        
        public EClientUINotificationType m_uiType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24 * 1024)]
        public byte[] _m_rgchData;
        public uint m_uiIndex;

        public string m_rgchData
        {
            get
            {
                return Encoding.UTF8.GetString(this._m_rgchData, 0, Array.IndexOf<byte>(this._m_rgchData, 0));
            }
        }

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
