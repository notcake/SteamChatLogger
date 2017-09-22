using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EClanRelationship : uint
    {
        eClanRelationshipNone    = 0,
        eClanRelationshipBlocked = 1,
        eClanRelationshipInvited = 2,
        eClanRelationshipMember  = 3,
        eClanRelationshipKicked  = 4
    }
}
