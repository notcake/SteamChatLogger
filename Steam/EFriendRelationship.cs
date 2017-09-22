using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EFriendRelationship : uint
    {
        k_EFriendRelationshipNone                 = 0, // The users have no relationship.
        k_EFriendRelationshipBlocked              = 1, // The user has just clicked Ignore on an friendship invite. This doesn't get stored.
        k_EFriendRelationshipRequestRecipient     = 2, // The user has requested to be friends with the current user.
        k_EFriendRelationshipFriend               = 3, // A "regular" friend.
        k_EFriendRelationshipRequestInitiator     = 4, // The current user has sent a friend invite.
        k_EFriendRelationshipIgnored              = 5, // The current user has explicit blocked this other user from comments/chat/etc. This is stored.
        k_EFriendRelationshipIgnoredFriend        = 6, // The user has ignored the current user.
        k_EFriendRelationshipSuggested_DEPRECATED = 7, // Deprecated -- Unused.

        k_EFriendRelationshipMax                  = 8  //	The total number of friend relationships used for looping and verification.
    }
}
