using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("STEAMAPPLIST_INTERFACE_VERSION001")]
    public interface ISteamAppList001
    {
        uint GetNumInstalledApps();
        uint GetInstalledApps(AppId[] pvecAppID, uint unMaxAppIDs);

        int GetAppName(AppId nAppID, byte[] pchName, int cchNameMax);
	    int GetAppInstallDir(AppId nAppID, byte[] pchDirectory, int cchNameMax);

	    int GetAppBuildId(AppId nAppID);
    }

    public static class ISteamAppList001Extension
    {
        private static ThreadLocal<byte[]> nameBuffer = new ThreadLocal<byte[]>(() => new byte[512]);
        public static string GetAppName(this ISteamAppList001 steamAppList, AppId nAppID)
        {
            byte[] name = nameBuffer.Value;
            int cchName = steamAppList.GetAppName(nAppID, name, name.Length);
            if (cchName == -1) { return null; }

            return Encoding.UTF8.GetString(name, 0, Math.Max(0, cchName - 1));
        }
    }
}
