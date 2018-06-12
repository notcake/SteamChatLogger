using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    public enum EClientUINotificationType : uint
    {
        k_EClientUINotificationTypeGroup = 0x00000001,
        k_EClientUINotificationTypeUser = 0x00000002
    }
}
