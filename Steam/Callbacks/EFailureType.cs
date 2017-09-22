using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    public enum EFailureType : byte
    {
        k_EFailureFlushedCallbackQueue,
        k_EFailurePipeFail,
    }
}
