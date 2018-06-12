using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("CLIENTENGINE_INTERFACE_VERSION005")]
    public interface IClientEngine005
    {
        HSteamPipe CreateSteamPipe();
        bool BReleaseSteamPipe(HSteamPipe hSteamPipe);

        HSteamUser CreateGlobalUser(out HSteamPipe phSteamPipe);
        HSteamUser ConnectToGlobalUser(HSteamPipe hSteamPipe);

        HSteamUser CreateLocalUser(out HSteamPipe phSteamPipe, EAccountType eAccountType);
        void CreatePipeToLocalUser(HSteamUser hSteamUser, out HSteamPipe phSteamPipe);

        void ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser);

        bool IsValidHSteamUserPipe(HSteamPipe hSteamPipe, HSteamUser hUser);

        IntPtr GetIClientUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void SetLocalIPBinding(uint unIP, ushort usPort);
        string GetUniverseName(EUniverse eUniverse);

        IntPtr GetIClientFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientUtils(HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientBilling(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientGameSearch(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void RunFrame();
        uint GetIPCCallCount();

        IntPtr GetIClientUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientGameServerStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientRemoteStorage(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientScreenshots(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void SetWarningMessageHook(/* Action<int, string> */ IntPtr pFunction);

        IntPtr GetIClientGameCoordinator(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition);
        void SetOverlayNotificationInsert(int x, int y);
        bool HookScreenshots(bool bHook);
        bool IsOverlayEnabled();

        bool GetAPICallResult(HSteamPipe hSteamPipe, SteamAPICall hSteamAPICall, byte[] pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed);

        IntPtr GetIClientProductBuilder(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientDepotBuilder(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientNetworkDeviceManager(HSteamPipe hSteamPipe, string pchVersion);

        void ConCommandInit(IntPtr pAccessor);

        IntPtr GetIClientAppManager(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientConfigStore(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        bool BOverlayNeedsPresent();

        IntPtr GetIClientGameStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientHTTP(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        bool BShutdownIfAllPipesClosed();

        IntPtr GetIClientAudio(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientMusic(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientUnifiedMessages(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientParentalSettings(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientStreamLauncher(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientDeviceAuth(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        IntPtr GetIClientRemoteClientManager(HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientStreamClient(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientShortcuts(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientInventory(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientVR(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientGameNotifications(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientHTMLSurface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientVideo(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientControllerSerialized(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientAppDisableUpdate(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void Set_Client_API_CCheckCallbackRegisteredInProcess(/* Func<int, int> */ IntPtr pFunction);

        IntPtr GetIClientBluetoothManager(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientSharedConnection(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);
        IntPtr GetIClientNetworkingSocketsSerialized(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion);

        void Unknown1();
        void Unknown2();

        IntPtr GetIPCServerMap();
        void OnDebugTextArrived(string pchDebugText);
        void OnThreadLocalRegistration();
        void OnThreadBuffersOverLimit();
    }

    public static class IClientEngine005Extension
    {
        public static IClientFriends001 GetIClientFriends(this IClientEngine005 clientEngine, HSteamUser hSteamUser, HSteamPipe hSteamPipe)
        {
            return Interface.Bind<IClientFriends001>(clientEngine.GetIClientFriends(hSteamUser, hSteamPipe, Interface.GetName<IClientFriends001>()));
        }

        public static IClientApps001 GetIClientApps(this IClientEngine005 clientEngine, HSteamUser hSteamUser, HSteamPipe hSteamPipe)
        {
            return Interface.Bind<IClientApps001>(clientEngine.GetIClientApps(hSteamUser, hSteamPipe, Interface.GetName<IClientApps001>()));
        }
    }
}
