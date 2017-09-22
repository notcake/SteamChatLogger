using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EChatAction : uint
    {
        k_EChatActionInviteChat            =  1,
        k_EChatActionKick                  =  2,
        k_EChatActionBan                   =  3,
        k_EChatActionUnBan                 =  4,
        k_EChatActionStartVoiceSpeak       =  5,
        k_EChatActionEndVoiceSpeak         =  6,
        k_EChatActionLockChat              =  7,
        k_EChatActionUnlockChat            =  8,
        k_EChatActionCloseChat             =  9,
        k_EChatActionSetJoinable           = 10,
        k_EChatActionSetUnjoinable         = 11,
        k_EChatActionSetOwner              = 12,
        k_EChatActionSetInvisibleToFriends = 13,
        k_EChatActionSetVisibleToFriends   = 14,
        k_EChatActionSetModerated          = 15,
        k_EChatActionSetUnmoderated        = 16
    }
}
