using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EPersonaState : uint
    {
        k_EPersonaStateOffline        = 0, // Friend is not currently logged on.
        k_EPersonaStateOnline         = 1, // Friend is logged on.
        k_EPersonaStateBusy           = 2, // User is on, but busy.
        k_EPersonaStateAway           = 3, // Auto-away feature.
        k_EPersonaStateSnooze         = 4, // Auto-away for a long time.
        k_EPersonaStateLookingToTrade = 5, // Online, trading.
        k_EPersonaStateLookingToPlay  = 6, // Online, wanting to play.
        k_EPersonaStateMax            = 7  // The total number of states. Only used for looping and validation.
    }

    public static class EPersonaStateExtension
    {
        public static string ToDisplayString(this EPersonaState personaState)
        {
            switch (personaState)
            {
                case EPersonaState.k_EPersonaStateOffline:        return "Offline";
                case EPersonaState.k_EPersonaStateOnline:         return "Online";
                case EPersonaState.k_EPersonaStateBusy:           return "Busy";
                case EPersonaState.k_EPersonaStateAway:           return "Away";
                case EPersonaState.k_EPersonaStateSnooze:         return "Snooze";
                case EPersonaState.k_EPersonaStateLookingToTrade: return "Looking to Trade";
                case EPersonaState.k_EPersonaStateLookingToPlay:  return "Looking to Play";
                default: return personaState.ToString();
            }
        }
    }
}
