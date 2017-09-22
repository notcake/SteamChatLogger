using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EUniverse : uint
    {
        k_EUniverseInvalid  = 0,
        k_EUniversePublic   = 1,
        k_EUniverseBeta     = 2,
        k_EUniverseInternal = 3,
        k_EUniverseDev      = 4,
        // k_EUniverseRC      = 5, // no such universe anymore
        k_EUniverseMax
    }
}
