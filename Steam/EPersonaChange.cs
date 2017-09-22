using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EPersonaChange
    {
        k_EPersonaChangeName                = 0x0001,
        k_EPersonaChangeStatus              = 0x0002,
        k_EPersonaChangeComeOnline          = 0x0004,
        k_EPersonaChangeGoneOffline         = 0x0008,
        k_EPersonaChangeGamePlayed          = 0x0010,
        k_EPersonaChangeGameServer          = 0x0020,
        k_EPersonaChangeAvatar              = 0x0040,
        k_EPersonaChangeJoinedSource        = 0x0080,
        k_EPersonaChangeLeftSource          = 0x0100,
        k_EPersonaChangeRelationshipChanged = 0x0200,
        k_EPersonaChangeNameFirstSet        = 0x0400,
        k_EPersonaChangeFacebookInfo        = 0x0800,
        k_EPersonaChangeNickname            = 0x1000,
        k_EPersonaChangeSteamLevel          = 0x2000
    }
}
