using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HAuthTicket
    {
        public uint Value;

        public static readonly HAuthTicket k_HAuthTicketInvalid = new HAuthTicket { Value = 0 };
        public static readonly HAuthTicket Invalid = new HAuthTicket { Value = 0 };
    }
}
