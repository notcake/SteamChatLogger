using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EFriendFlags : uint
    {
        k_EFriendFlagNone                 = 0x0000, // None.
        k_EFriendFlagBlocked              = 0x0001, // Users that the current user has blocked from contacting.
        k_EFriendFlagFriendshipRequested  = 0x0002, // Users that have sent a friend invite to the current user.
        k_EFriendFlagImmediate            = 0x0004, // The current user's "regular" friends.
        k_EFriendFlagClanMember           = 0x0008, // Users that are in one of the same (small) Steam groups as the current user.
        k_EFriendFlagOnGameServer         = 0x0010, // Users that are on the same game server; as set by SetPlayedWith.
        k_EFriendFlagHasPlayedWith        = 0x0020,
        k_EFriendFlagFriendOfFriend       = 0x0040,
        k_EFriendFlagRequestingFriendship = 0x0080, // Users that the current user has sent friend invites to.
        k_EFriendFlagRequestingInfo       = 0x0100, // Users that are currently sending additional info about themselves after a call to RequestUserInformation
        k_EFriendFlagIgnored              = 0x0200, // Users that the current user has ignored from contacting them.
        k_EFriendFlagIgnoredFriend        = 0x0400, // Users that have ignored the current user; but the current user still knows about them.
        k_EFriendFlagSuggested            = 0x0800,
        k_EFriendFlagChatMember           = 0x1000, // Users in one of the same chats.
        k_EFriendFlagAll                  = 0xFFFF
    }
}
