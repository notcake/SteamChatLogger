using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EUserRestriction : uint
    {
        k_nUserRestrictionNone        =  0, // No known chat/content restriction.
        k_nUserRestrictionUnknown     =  1, // We don't know yet, the user is offline.
        k_nUserRestrictionAnyChat     =  2, // User is not allowed to (or can't) send/recv any chat.
        k_nUserRestrictionVoiceChat   =  4, // User is not allowed to (or can't) send/recv voice chat.
        k_nUserRestrictionGroupChat   =  8, // User is not allowed to (or can't) send/recv group chat.
        k_nUserRestrictionRating      = 16, // User is too young according to rating in current region.
        k_nUserRestrictionGameInvites = 32, // User cannot send or recv game invites, for example if they are on mobile.
        k_nUserRestrictionTrading     = 64, // User cannot participate in trading, for example if they are on a console or mobile.
    }
}
