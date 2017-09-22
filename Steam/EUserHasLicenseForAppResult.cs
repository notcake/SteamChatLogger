using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EUserHasLicenseForAppResult : uint
    {
        k_EUserHasLicenseResultHasLicense         = 0, // User has a license for specified app
        k_EUserHasLicenseResultDoesNotHaveLicense = 1, // User does not have a license for the specified app
        k_EUserHasLicenseResultNoAuth             = 2  // User has not been authenticated
    }
}
