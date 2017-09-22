using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EChatActionResult : uint
    {
	    k_EChatActionResultSuccess                =  1,
	    k_EChatActionResultError                  =  2,
	    k_EChatActionResultNotPermitted           =  3,
	    k_EChatActionResultNotAllowedOnClanMember =  4,
	    k_EChatActionResultNotAllowedOnBannedUser =  5,
	    k_EChatActionResultNotAllowedOnChatOwner  =  6,
	    k_EChatActionResultNotAllowedOnSelf       =  7,
	    k_EChatActionResultChatDoesntExist        =  8,
	    k_EChatActionResultChatFull               =  9,
	    k_EChatActionResultVoiceSlotsFull         = 10
    }
}
