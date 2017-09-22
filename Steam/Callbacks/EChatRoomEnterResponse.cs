using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    public enum EChatRoomEnterResponse : uint
    {
        k_EChatRoomEnterResponseSuccess            =  1, // Success
        k_EChatRoomEnterResponseDoesntExist        =  2, // Chat doesn't exist (probably closed)
        k_EChatRoomEnterResponseNotAllowed         =  3, // General Denied - You don't have the permissions needed to join the chat
        k_EChatRoomEnterResponseFull               =  4, // Chat room has reached its maximum size
        k_EChatRoomEnterResponseError              =  5, // Unexpected Error
        k_EChatRoomEnterResponseBanned             =  6, // You are banned from this chat room and may not join
        k_EChatRoomEnterResponseLimited            =  7, // Joining this chat is not allowed because you are a limited user (no value on account)
        k_EChatRoomEnterResponseClanDisabled       =  8, // Attempt to join a clan chat when the clan is locked or disabled
        k_EChatRoomEnterResponseCommunityBan       =  9, // Attempt to join a chat when the user has a community lock on their account
        k_EChatRoomEnterResponseMemberBlockedYou   = 10, // Join failed - some member in the chat has blocked you from joining
        k_EChatRoomEnterResponseYouBlockedMember   = 11, // Join failed - you have blocked some member already in the chat
        k_EChatRoomEnterResponseNoRankingDataLobby = 12, // There is no ranking data available for the lobby 
        k_EChatRoomEnterResponseNoRankingDataUser  = 13, // There is no ranking data available for the user
        k_EChatRoomEnterResponseRankOutOfRange     = 14, // The user is out of the allowable ranking range
    }
}
