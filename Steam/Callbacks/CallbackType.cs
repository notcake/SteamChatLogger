using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Callbacks
{
    public enum CallbackType : int
    {
        k_iSteamUserCallbacks                 =  100,
        [Callback(typeof(SteamServersConnected))]
        SteamServersConnected                 = k_iSteamUserCallbacks +  1,
        [Callback(typeof(IPCFailure))]
        IPCFailure                            = k_iSteamUserCallbacks + 17,
        [Callback(typeof(AppLifetimeNotice))]
        AppLifetimeNotice                     = k_iSteamUserCallbacks + 30,

        k_iSteamFriendsCallbacks              =  300,
        [Callback(typeof(PersonaStateChange))]
        PersonaStateChange                    = k_iSteamFriendsCallbacks +  4,
        [Callback(typeof(FriendRichPresenceUpdate))]
        FriendRichPresenceUpdate              = k_iSteamFriendsCallbacks + 36,

        k_iSteamMatchmakingCallbacks          =  500,
        [Callback(typeof(FavoritesListChangedOld))]
        FavoritesListChangedOld               = k_iSteamMatchmakingCallbacks +  1,
        [Callback(typeof(FavoritesListChanged))]
        FavoritesListChanged                  = k_iSteamMatchmakingCallbacks +  2,

        k_iSteamUtilsCallbacks                =  700,
        [Callback(typeof(SteamConfigStoreChanged))]
        SteamConfigStoreChanged               = k_iSteamUtilsCallbacks + 11,

        k_iClientFriendsCallbacks             =  800,
        [Callback(typeof(FriendAdded))]
        FriendAdded                           = k_iClientFriendsCallbacks +  3,
        [Callback(typeof(UserRequestingFriendship))]
        UserRequestingFriendship              = k_iClientFriendsCallbacks +  4,
        [Callback(typeof(FriendChatMsg))]
        FriendChatMsg                         = k_iClientFriendsCallbacks +  5,
        [Callback(typeof(ChatRoomInvite))]
        ChatRoomInvite                        = k_iClientFriendsCallbacks +  7,
        [Callback(typeof(ChatRoomEnter))]
        ChatRoomEnter                         = k_iClientFriendsCallbacks +  8,
        [Callback(typeof(ChatMemberStateChange))]
        ChatMemberStateChange                 = k_iClientFriendsCallbacks +  9,
        [Callback(typeof(ChatRoomMsg))]
        ChatRoomMsg                           = k_iClientFriendsCallbacks + 10,
        [Callback(typeof(ChatRoomDlgClose))]
        ChatRoomDlgClose                      = k_iClientFriendsCallbacks + 11,
        [Callback(typeof(ChatRoomKicking))]
        ChatRoomKicking                       = k_iClientFriendsCallbacks + 13,
        [Callback(typeof(ChatRoomBanning))]
        ChatRoomBanning                       = k_iClientFriendsCallbacks + 14,
        [Callback(typeof(ChatRoomCreate))]
        ChatRoomCreate                        = k_iClientFriendsCallbacks + 15,
        [Callback(typeof(OpenChatDialog))]
        OpenChatDialog                        = k_iClientFriendsCallbacks + 16,
        [Callback(typeof(ChatRoomActionResult))]
        ChatRoomActionResult                  = k_iClientFriendsCallbacks + 17,
        [Callback(typeof(ClanInfoChanged))]
        ClanInfoChanged                       = k_iClientFriendsCallbacks + 19,
        [Callback(typeof(ChatRoomInfoChanged))]
        ChatRoomInfoChanged                   = k_iClientFriendsCallbacks + 21,
        [Callback(typeof(NotifyChatRoomVoiceStateChanged))]
        NotifyChatRoomVoiceStateChanged       = k_iClientFriendsCallbacks + 27,

        k_iClientUserCallbacks                =  900,
        [Callback(typeof(SteamServersDisconnected))]
        SteamServersDisconnected              = k_iClientUserCallbacks +  3,
        [Callback(typeof(AccountInformationUpdated))]
        AccountInformationUpdated             = k_iClientUserCallbacks + 15,
        [Callback(typeof(UpdateGuestPasses))]
        UpdateGuestPasses                     = k_iClientUserCallbacks + 19,
        [Callback(typeof(AppOwnershipTicketReceived))]
        AppOwnershipTicketReceived            = k_iClientUserCallbacks + 28,
        [Callback(typeof(ClientMarketingMessageUpdate))]
        ClientMarketingMessageUpdate          = k_iClientUserCallbacks + 37,
        [Callback(typeof(WebAuthRequestCallback))]
        WebAuthRequestCallback                = k_iClientUserCallbacks + 42,
        [Callback(typeof(AppMinutesPlayedDataNotice))]
        AppMinutesPlayedDataNotice            = k_iClientUserCallbacks + 46,
        [Callback(typeof(WalletBalanceUpdated))]
        WalletBalanceUpdated                  = k_iClientUserCallbacks + 48,
        [Callback(typeof(LoginInformationChanged))]
        LoginInformationChanged               = k_iClientUserCallbacks + 55,
        [Callback(typeof(UpdateOfflineMessageNotification))]
        UpdateOfflineMessageNotification      = k_iClientUserCallbacks + 62,
        [Callback(typeof(FriendMessageHistoryChatLog))]
        FriendMessageHistoryChatLog           = k_iClientUserCallbacks + 63,
        [Callback(typeof(GetSteamGuardDetailsResponse))]
        GetSteamGuardDetailsResponse          = k_iClientUserCallbacks + 66,

        k_iSteamAppsCallbacks                 = 1000,
        [Callback(typeof(AppDataChanged))]
        AppDataChanged                        = k_iSteamAppsCallbacks + 1,
        [Callback(typeof(AppInfoUpdateComplete))]
        AppInfoUpdateComplete                 = k_iSteamAppsCallbacks + 3,
        [Callback(typeof(AppEventStateChange))]
        AppEventStateChange                   = k_iSteamAppsCallbacks + 6,
        [Callback(typeof(DownloadScheduleChanged))]
        DownloadScheduleChanged               = k_iSteamAppsCallbacks + 9,

        k_iSteamUserStatsCallbacks            = 1100,
        [Callback(typeof(UserAchievementIconFetched))]
        UserAchievementIconFetched            = k_iSteamUserStatsCallbacks + 9,

        k_iClientRemoteStorageCallbacks       = 1300,
        [Callback(typeof(RemoteStorageAppSyncedClient))]
        RemoteStorageAppSyncedClient          = k_iClientRemoteStorageCallbacks + 1,
        [Callback(typeof(RemoteStorageAppInfoLoaded))]
        RemoteStorageAppInfoLoaded            = k_iClientRemoteStorageCallbacks + 4,
        [Callback(typeof(RemoteStorageAppSyncStatusCheck))]
        RemoteStorageAppSyncStatusCheck       = k_iClientRemoteStorageCallbacks + 5,

        k_iClientMusicCallbacks               = 3200,
        k_iClientRemoteClientManagerCallbacks = 3300,

        k_iClientVideoCallbacks               = 4600,
    }
}
