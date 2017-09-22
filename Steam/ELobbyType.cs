using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum ELobbyType : uint
    {
        k_ELobbyTypePrivate     = 0,
        k_ELobbyTypeFriendsOnly = 1,
        k_ELobbyTypePublic      = 2,
        k_ELobbyTypeInvisible   = 3
    }
}
