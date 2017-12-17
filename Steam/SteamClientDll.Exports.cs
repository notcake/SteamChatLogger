using Steam.Callbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public partial class SteamClientDll
    {
		private static class Exports
        {
            [DllImport("steamclient64.dll")]
            public static extern IntPtr CreateInterface(string name, out bool success);

            [DllImport("steamclient64.dll")]
            public static extern bool Steam_BGetCallback(HSteamPipe hSteamPipe, out CallbackMsg callbackMsg);

            [DllImport("steamclient64.dll")]
            public static extern void Steam_FreeLastCallback(HSteamPipe hSteamPipe);

            [DllImport("steamclient64.dll")]
            public static extern void Steam_ReleaseThreadLocalMemory();

            public static IntPtr CreateInterface(string name)
            {
                return Exports.CreateInterface(name, out _);
            }

            public static T CreateInterface<T>()
                where T : class
            {
                return Interface.Bind<T>(Exports.CreateInterface(Interface.GetName<T>()));
            }
        }
    }
}
