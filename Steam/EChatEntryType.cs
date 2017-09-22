using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EChatEntryType : uint
    {
        k_EChatEntryTypeInvalid          =  0,
        k_EChatEntryTypeChatMsg          =  1, // Normal text message from another user
        k_EChatEntryTypeTyping           =  2, // Another user is typing (not used in multi-user chat)
        k_EChatEntryTypeInviteGame       =  3, // Invite from other user into that users current game
        k_EChatEntryTypeEmote            =  4, // text emote message (deprecated, should be treated as ChatMsg)
        // k_EChatEntryTypeLobbyGameStart   =  5, // lobby game is starting (dead - listen for LobbyGameCreated_t callback instead)
        k_EChatEntryTypeLeftConversation =  6, // user has left the conversation ( closed chat window )
                                               // Above are previous FriendMsgType entries, now merged into more generic chat entry types
        k_EChatEntryTypeEntered          =  7, // user has entered the conversation (used in multi-user chat and group chat)
        k_EChatEntryTypeWasKicked        =  8, // user was kicked (data: 64-bit steamid of actor performing the kick)
        k_EChatEntryTypeWasBanned        =  9, // user was banned (data: 64-bit steamid of actor performing the ban)
        k_EChatEntryTypeDisconnected     = 10, // user disconnected
        k_EChatEntryTypeHistoricalChat   = 11, // a chat message from user's chat history or offilne message
        // k_EChatEntryTypeReserved1        = 12, // No longer used
        // k_EChatEntryTypeReserved2        = 13, // No longer used
        k_EChatEntryTypeLinkBlocked      = 14, // a link was removed by the chat filter.
    }
}
