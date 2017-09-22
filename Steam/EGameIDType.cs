using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EGameIDType : byte
    {
        k_EGameIDTypeApp      = 0,
        k_EGameIDTypeGameMod  = 1,
        k_EGameIDTypeShortcut = 2,
        k_EGameIDTypeP2P      = 3
    }
}
