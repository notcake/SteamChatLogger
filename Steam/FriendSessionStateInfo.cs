using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FriendSessionStateInfo
    {
        uint m_uiOnlineSessionInstances;
        byte m_uiPublishedToFriendsSessionInstance;
    }
}
