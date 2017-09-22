using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EConfigStore : int
    {
        k_EConfigStoreInvalid     = 0,
        k_EConfigStoreInstall     = 1,
        k_EConfigStoreUserRoaming = 2,
        k_EConfigStoreUserLocal   = 3,
        k_EConfigStoreMax         = 4,
    }
}
