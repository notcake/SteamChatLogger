﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EVoiceResult : uint
    {
        k_EVoiceResultOK                   = 0,
        k_EVoiceResultNotInitialized       = 1,
        k_EVoiceResultNotRecording         = 2,
        k_EVoiceResultNoData               = 3,
        k_EVoiceResultBufferTooSmall       = 4,
        k_EVoiceResultDataCorrupted        = 5,
        k_EVoiceResultRestricted           = 6,
        k_EVoiceResultUnsupportedCodec     = 7,
        k_EVoiceResultReceiverOutOfDate    = 8,
        k_EVoiceResultReceiverDidNotAnswer = 9
    }
}
