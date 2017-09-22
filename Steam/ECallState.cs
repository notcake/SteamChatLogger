using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum ECallState : uint
    {
        k_ECallStateUnknown = 0,
        k_ECallStateWaiting = 1,
        k_ECallStateDialing = 2,
        k_ECallStateRinging = 3,
        k_ECallStateInCall  = 4
    }
}
