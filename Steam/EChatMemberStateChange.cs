using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [Flags]
    public enum EChatMemberStateChange : uint
    {
        k_EChatMemberStateChangeEntered      = 0x0001, // This user has joined or is joining the chat room
        k_EChatMemberStateChangeLeft         = 0x0002, // This user has left or is leaving the chat room
        k_EChatMemberStateChangeDisconnected = 0x0004, // User disconnected without leaving the chat first
        k_EChatMemberStateChangeKicked       = 0x0008, // User kicked
        k_EChatMemberStateChangeBanned       = 0x0010  // User kicked and banned
    }
}
