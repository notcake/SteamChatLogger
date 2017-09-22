using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct UserAchievementIconFetched : ICallbackData
    {
        public const CallbackType k_iCallback = CallbackType.UserAchievementIconFetched;

        public CGameID m_nGameID;              // Game this is for
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_rgchAchievementName;       // name of the achievement
        public bool m_bAchieved;       // Is the icon for the achieved or not achieved version?
        public int m_nIconHandle; // Handle to the image, which can be used in ClientUtils()->GetImageRGBA(), 0 means no image is set for the achievement

        #region ICallbackData
        public CallbackType Type => k_iCallback;
        #endregion
    }
}
