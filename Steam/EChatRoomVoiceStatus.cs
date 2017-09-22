using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EChatRoomVoiceStatus : int
    {
        eChatRoomVoiceStatus_Invalid                = -1,
        eChatRoomVoiceStatus_Bad                    =  0,
        eChatRoomVoiceStatus_UnknownRoom            =  1,
        eChatRoomVoiceStatus_UnknownUser            =  2,
        eChatRoomVoiceStatus_NotSpeaking            =  3,
        eChatRoomVoiceStatus_Connected_Speaking     =  4,
        eChatRoomVoiceStatus_Connected_SpeakingData =  5,
        eChatRoomVoiceStatus_NotConnected_Speaking  =  6,
        eChatRoomVoiceStatus_Connecting             =  7,
        eChatRoomVoiceStatus_Unreachable            =  8,
        eChatRoomVoiceStatus_Disconnected           =  9,
        eChatRoomVoiceStatus_Count                  = 10
    }
}
