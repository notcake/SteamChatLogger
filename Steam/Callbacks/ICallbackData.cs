using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    public interface ICallbackData
    {
        CallbackType Type { get; }
    }
}
