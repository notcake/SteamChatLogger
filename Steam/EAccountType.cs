using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EAccountType : uint
    {
        k_EAccountTypeInvalid        =  0,
        k_EAccountTypeIndividual     =  1, // single user account
        k_EAccountTypeMultiseat      =  2, // multiseat (e.g. cybercafe) account
        k_EAccountTypeGameServer     =  3, // game server account
        k_EAccountTypeAnonGameServer =  4, // anonymous game server account
        k_EAccountTypePending        =  5, // pending
        k_EAccountTypeContentServer  =  6, // content server
        k_EAccountTypeClan           =  7,
        k_EAccountTypeChat           =  8,
        k_EAccountTypeConsoleUser    =  9, // Fake SteamID for local PSN account on PS3 or Live account on 360, etc.
        k_EAccountTypeAnonUser       = 10,

        // Max of 16 items in this field
        k_EAccountTypeMax
    }
}
