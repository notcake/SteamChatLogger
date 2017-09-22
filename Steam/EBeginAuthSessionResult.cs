using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EBeginAuthSessionResult : uint
    {
        k_EBeginAuthSessionResultOK               = 0, // Ticket is valid for this game and this steamID.
        k_EBeginAuthSessionResultInvalidTicket    = 1, // Ticket is not valid.
        k_EBeginAuthSessionResultDuplicateRequest = 2, // A ticket has already been submitted for this steamID
        k_EBeginAuthSessionResultInvalidVersion   = 3, // Ticket is from an incompatible interface version
        k_EBeginAuthSessionResultGameMismatch     = 4, // Ticket is not for this game
        k_EBeginAuthSessionResultExpiredTicket    = 5  // Ticket has expired
    }
}
