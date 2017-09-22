using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("CLIENTAPPS_INTERFACE_VERSION001")]
    public interface IClientApps001
    {
        int GetAppData(AppId unAppID, byte[] pchKey, byte[] pchValue, int cchValueMax);

        bool SetLocalAppConfig(AppId unAppID, byte[] pchBuffer, int cbBuffer);

        AppId GetInternalAppIDFromGameID(CGameID nGameID);

        int GetAllOwnedMultiplayerApps(AppId[] punAppIDs, int cAppIDsMax);

        int GetAppDataSection(AppId unAppID, EAppInfoSection eSection, byte[] pchBuffer, int cbBufferMax, bool bSharedKVSymbols);
        bool RequestAppInfoUpdate(AppId[] pAppIDs, int nNumAppIDs);

        void NotifyAppEventTriggered(AppId unAppID, EAppEvent eAppEvent);

        int GetDLCCount(AppId unAppID);
        bool BGetDLCDataByIndex(AppId unAppID, int iDLC, out AppId pDlcAppID, out bool pbAvailable, byte[] pchName, int cchNameBufferSize);
    }

    public static class IClientApps001Extension
    {
        private static ThreadLocal<byte[]> valueBuffer = new ThreadLocal<byte[]>(() => new byte[512]);
        public static string GetAppData(this IClientApps001 clientApps, AppId nAppID, string pchKey)
        {
            byte[] value = valueBuffer.Value;
            int cchValue = clientApps.GetAppData(nAppID, Encoding.UTF8.GetBytes(pchKey), value, value.Length);
            if (cchValue == -1) { return null; }

            return Encoding.UTF8.GetString(value, 0, Math.Max(0, cchValue - 1));
        }
    }
}
