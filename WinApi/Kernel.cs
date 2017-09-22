using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamChatLogger.WinApi
{
    public static partial class Kernel
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(HMODULE hModule);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(HMODULE hModule, string procName);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern HMODULE LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern HMODULE LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryExFlags dwFlags);

        public static TDelegate GetProcAddress<TDelegate>(HMODULE hModule, string procName)
        {
            IntPtr pFunction = Kernel.GetProcAddress(hModule, procName);
            if (pFunction == IntPtr.Zero) { return default(TDelegate); }

            return Marshal.GetDelegateForFunctionPointer<TDelegate>(pFunction);
        }
    }
}
