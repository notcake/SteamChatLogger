using Microsoft.Win32;
using Steam.Callbacks;
using SteamChatLogger.WinApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public partial class SteamClientDll : IDisposable
    {
        private HMODULE hSteamClientDll;

        private SteamClientDll(HMODULE hSteamClientDll)
        {
            this.hSteamClientDll = hSteamClientDll;
        }

        public IntPtr CreateInterface(string name)
        {
            return Exports.CreateInterface(name);
        }

        public T CreateInterface<T>()
            where T : class
        {
            return Exports.CreateInterface<T>();
        }

        public bool Steam_BGetCallback(HSteamPipe hSteamPipe, out CallbackMsg callbackMsg)
        {
            return Exports.Steam_BGetCallback(hSteamPipe, out callbackMsg);
        }

        public void Steam_FreeLastCallback(HSteamPipe hSteamPipe)
        {
            Exports.Steam_FreeLastCallback(hSteamPipe);
        }

        public void Steam_ReleaseThreadLocalMemory()
        {
            Exports.Steam_ReleaseThreadLocalMemory();
        }

        public ISteamClient017 SteamClient { get; } = Exports.CreateInterface<ISteamClient017>();
        public IClientEngine005 ClientEngine { get; } = Exports.CreateInterface<IClientEngine005>();

        #region IDisposable
        public void Dispose()
        {
            Kernel.FreeLibrary(this.hSteamClientDll);
            this.hSteamClientDll = HMODULE.Null;
        }
        #endregion

        public static SteamClientDll Wrangle()
        {
            object steamPath = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamPath", null);
            if (steamPath == null) { return null; }
            if (!(steamPath is string)) { return null; }

            string tier0DllPath = Path.Combine((string)steamPath, "tier0_s64.dll");
            string vstdlibDllPath = Path.Combine((string)steamPath, "vstdlib_s64.dll");
            string steamClientDllPath = Path.Combine((string)steamPath, "steamclient64.dll");
            HMODULE hTier0Dll = Kernel.LoadLibrary(tier0DllPath);
            HMODULE hVstdlibDll = Kernel.LoadLibrary(vstdlibDllPath);
            HMODULE hSteamClientDll = Kernel.LoadLibrary(steamClientDllPath);
            Kernel.FreeLibrary(hTier0Dll);
            Kernel.FreeLibrary(hVstdlibDll);
            if (hSteamClientDll == HMODULE.Null) { return null; }
            return new SteamClientDll(hSteamClientDll);
        }
    }
}
