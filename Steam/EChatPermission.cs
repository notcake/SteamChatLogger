using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EChatPermission : uint
    {
        k_EChatPermissionClose                    =    1,
        k_EChatPermissionInvite                   =    2,
        k_EChatPermissionTalk                     =    8,
        k_EChatPermissionKick                     =   16,
        k_EChatPermissionMute                     =   32,
        k_EChatPermissionSetMetadata              =   64,
        k_EChatPermissionChangePermissions        =  128,
        k_EChatPermissionBan                      =  256,
        k_EChatPermissionChangeAccess             =  512,
        k_EChatPermissionEveryoneNotInClanDefault =    8,
        k_EChatPermissionEveryoneDefault          =   10,
        k_EChatPermissionMemberDefault            =  282,
        k_EChatPermissionOfficerDefault           =  282,
        k_EChatPermissionOwnerDefault             =  891,
        k_EChatPermissionMask                     = 1019
    }
}
