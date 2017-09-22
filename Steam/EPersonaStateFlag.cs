using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EPersonaStateFlag : uint
    {
        k_EPersonaStateFlagHasRichPresence = 1,
        k_EPersonaStateFlagInJoinableGame  = 2,
    }
}
