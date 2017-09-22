using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("SteamClient017")]
    public interface ISteamClient017
    {
        HSteamPipe CreateSteamPipe();
        bool BReleaseSteamPipe(HSteamPipe hSteamPipe);
        HSteamUser ConnectToGlobalUser(HSteamPipe hSteamPipe);
        HSteamUser CreateLocalUser(out HSteamPipe hSteamPipe, EAccountType eAccountType);
        void ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser);
        
        IntPtr GetISteamUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        void SetLocalIPBinding(uint unIP, ushort usPort);
        IntPtr GetISteamFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamUtils(HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        void GetISteamGenericInterface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamGameServerStats(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamRemoteStorage(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamScreenshots(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        void RunFrame();
        uint GetIPCCallCount();
        void SetWarningMessageHook(/* Action<int, string> */ IntPtr pFunction);
        bool BShutdownIfAllPipesClosed();
        IntPtr GetISteamHTTP(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamUnifiedMessages(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamAppList(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamMusic(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamMusicRemote(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamHTMLSurface(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        void DEPRECATED_Set_SteamAPI_CPostAPIResultInProcess(Action f);
        void DEPRECATED_Remove_SteamAPI_CPostAPIResultInProcess(Action f);
        void Set_SteamAPI_CCheckCallbackRegisteredInProcess(/* Func<int, uint> */ IntPtr func);
        IntPtr GetISteamInventory(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamVideo(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetISteamParentalSettings(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion);
    }

    public static class ISteamClient017Extension
    {
        public static ISteamFriends015 GetISteamFriends(this ISteamClient017 steamClient, HSteamUser hSteamUser, HSteamPipe hSteamPipe)
        {
            return Interface.Bind<ISteamFriends015>(steamClient.GetISteamFriends(hSteamUser, hSteamPipe, Interface.GetName<ISteamFriends015>()));
        }
        
        public static ISteamAppList001 GetISteamAppList(this ISteamClient017 steamClient, HSteamUser hSteamUser, HSteamPipe hSteamPipe)
        {
            return Interface.Bind<ISteamAppList001>(steamClient.GetISteamFriends(hSteamUser, hSteamPipe, Interface.GetName<ISteamAppList001>()));
        }
        
        public static ISteamUser019 GetISteamUser(this ISteamClient017 steamClient, HSteamUser hSteamUser, HSteamPipe hSteamPipe)
        {
            return Interface.Bind<ISteamUser019>(steamClient.GetISteamUser(hSteamUser, hSteamPipe, Interface.GetName<ISteamUser019>()));
        }
    }
}
