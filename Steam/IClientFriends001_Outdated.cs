using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("CLIENTFRIENDS_INTERFACE_VERSION001")]
    public interface IClientFriends001_Outdated
    {
        string GetPersonaName();
        
        void SetPersonaName(string pchPersonaName);
        SteamAPICall SetPersonaNameEx(string pchPersonaName, bool bSendCallback);

        bool IsPersonaNameSet();
        
        EPersonaState GetPersonaState();
        void SetPersonaState(EPersonaState ePersonaState);

        bool NotifyUIOfMenuChange(bool bShowAvatars, bool bSortByName, bool bShowOnlineOnly, bool bShowUntaggedFriends);
        
        int GetFriendCount(EFriendFlags iFriendFlags);
        uint GetFriendArray(CSteamID[] pSteamIDFriends, byte[] unknown0, int unknown1, int unknown2);

        CSteamID GetFriendByIndex(int iFriend, EFriendFlags iFriendFlags);
        
        int GetOnlineFriendCount();
        
        EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend);
        EPersonaState GetFriendPersonaState(CSteamID steamIDFriend);
        string GetFriendPersonaName(CSteamID steamIDFriend);
        
        int GetSmallFriendAvatar(CSteamID steamIDFriend);
        int GetMediumFriendAvatar(CSteamID steamIDFriend);
        int GetLargeFriendAvatar(CSteamID steamIDFriend);

        void SetFriendRegValue(CSteamID steamIDFriend, string pchKey, string pchValue);
        string GetFriendRegValue(CSteamID steamIDFriend, string pchKey);

        bool DeleteFriendRegValue(CSteamID steamID, string pchKey);

        bool GetFriendGamePlayed(CSteamID steamID, out FriendGameInfo pGamePlayInfo);
        string GetFriendGamePlayedExtraInfo(CSteamID steamIDFriend);

        CSteamID GetFriendGameServer(CSteamID steamIDFriend);
        
        EPersonaStateFlag GetFriendPersonaStateFlags(CSteamID steamIDFriend);
        bool IsFriendGameOnConsole(CSteamID steamIDFriend);

        FriendSessionStateInfo GetFriendSessionStateInfo(CSteamID steamIDFriend);

        EUserRestriction GetFriendRestrictions(CSteamID steamIDFriend);
        
        string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName);
        uint GetFriendSteamLevel(CSteamID steamIDFriend);
        SteamAPICall RequestPersonaNameHistory(CSteamID steamIDFriend);
        string GetFriendPersonaNameHistoryAndDate(CSteamID steamIDFriend, int iPersonaName, out RTime32 puTime);

        bool AddFriend(CSteamID steamID);
        bool RemoveFriend(CSteamID steamID);
        bool HasFriend(CSteamID steamID, EFriendFlags iFriendFlags);
        
        HSteamCall AddFriendByName(string pchEmailOrAccountName);

        bool InviteFriendByEmail(string pchEmailAddress);

        bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly);

        bool SetIgnoreFriend(CSteamID steamIDFriend, bool bIgnore);

        bool ReportChatDeclined(CSteamID steamID);
        
        bool CreateFriendsGroup(string pchGroupName);
        bool DeleteFriendsGroup(short iGroupID);
        bool RenameFriendsGroup(string pchNewGroupName, short iGroupID);
        bool AddFriendToGroup(CSteamID steamID, short iGroupID);
        bool RemoveFriendFromGroup(CSteamID steamID, short iGroupID);
        bool IsFriendMemberOfFriendsGroup(CSteamID steamID, short iGroupID);
        short GetFriendsGroupCount();
        short GetFriendsGroupIDByIndex(short iGroupIndex);
        string GetFriendsGroupName(short iGroupID);
        short GetFriendsGroupMembershipCount(short iGroupID);

        CSteamID GetFirstFriendsGroupMember(short iGroupID);
        CSteamID GetNextFriendsGroupMember(short iGroupID);

        short GetGroupFriendsMembershipCount(CSteamID steamID);
        short GetFirstGroupFriendsMember(CSteamID steamID);
        short GetNextGroupFriendsMember(CSteamID steamID);

        string GetPlayerNickname(CSteamID playerSteamID);
        bool SetPlayerNickname(CSteamID playerSteamID, string cszNickname);

        uint GetFriendsSteamLevel(CSteamID steamIDFriend);

        int GetChatMessagesCount(CSteamID steamIDFriend);
        int GetChatMessage(CSteamID steamIDFriend, int iMessageID, byte[] pvData, int cubData, out EChatEntryType peChatEntryType, out CSteamID pSteamIDChatter, out RTime32 puTime);

        bool SendMsgToFriend(CSteamID steamIDFriend, EChatEntryType eChatEntryType, byte[] pvMsgBody, int cubMsgBody);
        
        void ClearChatHistory(CSteamID steamIDFriend);

        int GetKnownClanCount();
        CSteamID GetKnownClanByIndex(int iClan);

        int GetClanCount();
        CSteamID GetClanByIndex(int iClan);
        
        string GetClanName(CSteamID steamIDClan);
        string GetClanTag(CSteamID steamIDClan);

        bool GetFriendActivityCounts(out int pnOnline, out int pnInGame, bool bExcludeTaggedFriends);
        bool GetClanActivityCounts(CSteamID steamID, out int pnOnline, out int pnInGame, out int pnChatting);

        SteamAPICall DownloadClanActivityCounts(CSteamID[] groupIDs, int nIds);
        bool GetFriendsGroupActivityCounts(short iGroupID, out int pnOnline, out int pnInGame);

        bool IsClanPublic(CSteamID steamID);

        SteamAPICall JoinClanChatRoom(CSteamID groupID);
        bool LeaveClanChatRoom(CSteamID groupID);
        int GetClanChatMemberCount(CSteamID groupID);

        CSteamID GetChatMemberByIndex(CSteamID groupID, int iIndex);

        bool SendClanChatMessage(CSteamID groupID, string cszMessage);
        int GetClanChatMessage(CSteamID groupID, int iMessageID, byte[] pvData, int cubData, out EChatEntryType peChatEntryType, out CSteamID pSteamIDChatter);
        bool IsClanChatAdmin(CSteamID groupID, CSteamID userID);
        bool IsClanChatWindowOpenInSteam(CSteamID groupID);
        bool OpenClanChatWindowInSteam(CSteamID groupID);
        bool CloseClanChatWindowInSteam(CSteamID groupID);
        bool SetListenForFriendsMessages(bool bListen);
        bool ReplyToFriendMessage(CSteamID friendID, string cszMessage);
        int GetFriendMessage(CSteamID friendID, int iMessageID, byte[] pvData, int cubData, out EChatEntryType peChatEntryType);

        bool InviteFriendToClan(CSteamID steamIDfriend, CSteamID steamIDclan);
        bool AcknowledgeInviteToClan(CSteamID steamID, bool bAcceptOrDenyClanInvite);

        // iterators for any source
        int GetFriendCountFromSource(CSteamID steamIDSource);

        CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend);

        bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource);

        int GetCoplayFriendCount();

        CSteamID GetCoplayFriend(int iCoplayEvent);


        RTime32 GetFriendCoplayTime(CSteamID steamIDFriend);
        AppId GetFriendCoplayGame(CSteamID steamIDFriend);

        bool SetRichPresence(AppId nAppId, string pchKey, string pchValue);
        void ClearRichPresence(AppId nAppId);
        string GetFriendRichPresence(AppId nAppId, CSteamID steamIDFriend, string pchKey);
        int GetFriendRichPresenceKeyCount(AppId nAppId, CSteamID steamIDFriend);
        string GetFriendRichPresenceKeyByIndex(AppId nAppId, CSteamID steamIDFriend, int iIndex);

        void RequestFriendRichPresence(AppId nAppId, CSteamID steamIDFriend);

        bool JoinChatRoom(CSteamID steamIDChat);
        void LeaveChatRoom(CSteamID steamIDChat);

        bool InviteUserToChatRoom(CSteamID steamIDChat, CSteamID steamIDInvitee);

        bool SendChatMsg(CSteamID steamIDChat, EChatEntryType eChatEntryType, byte[] pvMsgBody, int cubMsgBody );

        int GetChatRoomMessagesCount(CSteamID steamIDChat);
        int GetChatRoomEntry(CSteamID steamIDChat, int iMessageID, out CSteamID steamIDuser, byte[] pvData, int cubData, out EChatEntryType peChatEntryType);

        void ClearChatRoomHistory(CSteamID steamID);

        bool SerializeChatRoomDlg(CSteamID steamIDChat, byte[] pvHistory, int cubHistory );
        int GetSizeOfSerializedChatRoomDlg(CSteamID steamIDChat);
        bool GetSerializedChatRoomDlg(CSteamID steamIDChat, byte[] pvHistory, int cubBuffer, out int pcubData);
        bool ClearSerializedChatRoomDlg(CSteamID steamIDChat);

        bool KickChatMember(CSteamID steamIDChat, CSteamID steamIDUserToActOn);
        bool BanChatMember(CSteamID steamIDChat, CSteamID steamIDUserToActOn);
        bool UnBanChatMember(CSteamID steamIDChat, CSteamID steamIDUserToActOn);

        bool SetChatRoomType(CSteamID steamIDChat, ELobbyType eLobbyType);
        bool GetChatRoomLockState(CSteamID steamIDChat, out bool pbLocked);
        bool GetChatRoomPermissions(CSteamID steamIDChat, out uint prgfChatRoomPermissions);

        bool SetChatRoomModerated(CSteamID steamIDChat, bool bModerated);
        bool BChatRoomModerated(CSteamID steamIDChat);

        bool NotifyChatRoomDlgsOfUIChange(CSteamID steamIDChat, bool bShowAvatars, bool bBeepOnNewMsg, bool bShowSteamIDs, bool bShowTimestampOnNewMsg);

        bool TerminateChatRoom(CSteamID steamIDChat);

        int GetChatRoomCount();
        CSteamID GetChatRoomByIndex(int iChatRoom);
        string GetChatRoomName(CSteamID steamIDChat);

        bool BGetChatRoomMemberDetails(CSteamID steamIDChat, CSteamID steamIDUser, out uint prgfChatMemberDetails, out uint prgfChatMemberDetailsLocal);

        void CreateChatRoom(EChatRoomType eType, ulong ulGameID, string pchName, ELobbyType eLobbyType, CSteamID steamIDClan, CSteamID steamIDFriendChat, CSteamID steamIDInvited, uint rgfChatPermissionOfficer, uint rgfChatPermissionMember, uint rgfChatPermissionAll);

        void VoiceCallNew(CSteamID steamIDLocalPeer, CSteamID steamIDRemotePeer);
        void VoiceCall(CSteamID steamIDLocalPeer, CSteamID steamIDRemotePeer);
        void VoiceHangUp(CSteamID steamIDLocalPeer, HVoiceCall hVoiceCall);

        void SetVoiceSpeakerVolume(float flVolume);
        void SetVoiceMicrophoneVolume(float flVolume);

        void SetAutoAnswer(bool bAutoAnswer);

        void VoiceAnswer(HVoiceCall hVoiceCall);
        void AcceptVoiceCall(CSteamID steamIDLocalPeer, CSteamID steamIDRemotePeer);

        void VoicePutOnHold(HVoiceCall HVoiceCall, bool bLocalHold);
        bool BVoiceIsLocalOnHold(HVoiceCall hVoiceCall);
        bool BVoiceIsRemoteOnHold(HVoiceCall hVoiceCall);

        void SetDoNotDisturb(bool bDoNotDisturb);

        void EnableVoiceNotificationSounds(bool bEnable);

        void SetPushToTalkEnabled(bool bEnable);
        bool IsPushToTalkEnabled();

        void SetPushToTalkKey(int nVirtualKey);
        int GetPushToTalkKey();

        bool IsPushToTalkKeyDown();

        void EnableVoiceCalibration(bool bState);
        bool IsVoiceCalibrating();
        float GetVoiceCalibrationSamplePeak();

        void SetMicBoost(bool bBoost);
        bool GetMicBoost();

        bool HasHardwareMicBoost();

        string GetMicDeviceName();

        void StartTalking(HVoiceCall hVoiceCall);
        void EndTalking(HVoiceCall hVoiceCall);

        bool VoiceIsValid(HVoiceCall hVoiceCall);

        void SetAutoReflectVoice(bool bState);

        ECallState GetCallState(HVoiceCall hVoiceCall);

        float GetVoiceMicrophoneVolume();
        float GetVoiceSpeakerVolume();

        float TimeSinceLastVoiceDataReceived(HVoiceCall hVoiceCall);
        float TimeSinceLastVoiceDataSend(HVoiceCall hVoiceCall);

        bool BCanSend(HVoiceCall hVoiceCall);
        bool BCanReceive(HVoiceCall hVoiceCall);

        float GetEstimatedBitsPerSecond(HVoiceCall hVoiceCall, bool bIncoming);
        float GetPeakSample(HVoiceCall hVoiceCall, bool bIncoming);

        void SendResumeRequest(HVoiceCall hVoiceCall);

        void OpenChatDialog(CSteamID steamID);

        void StartChatRoomVoiceSpeaking(CSteamID steamIDChat, CSteamID steamIDMember);
        void EndChatRoomVoiceSpeaking(CSteamID steamIDChat, CSteamID steamIDMember);

        RTime32 GetFriendLastLogonTime(CSteamID steamIDFriend);
        RTime32 GetFriendLastLogoffTime(CSteamID steamIDFriend);

        int GetChatRoomVoiceTotalSlotCount(CSteamID steamIDChat);
        int GetChatRoomVoiceUsedSlotCount(CSteamID steamIDChat);

        CSteamID GetChatRoomVoiceUsedSlot(CSteamID steamIDChat, int iSlot);

        EChatRoomVoiceStatus GetChatRoomVoiceStatus(CSteamID steamIDChat, CSteamID steamIDSpeaker);

        bool BChatRoomHasAvailableVoiceSlots(CSteamID steamIDChat);

        bool BIsChatRoomVoiceSpeaking(CSteamID steamIDChat, CSteamID steamIDSpeaker);

        float GetChatRoomPeakSample(CSteamID steamIDChat, CSteamID steamIDSpeaker, bool bIncoming);

        void ChatRoomVoiceRetryConnections(CSteamID steamIDChat);

        void SetPortTypes(uint unFlags);

        void ReinitAudio();

        void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking);

        bool IsInGameVoiceSpeaking();

        void ActivateGameOverlay(string pchDialog);
        void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID);
        void ActivateGameOverlayToWebPage(string pchURL);
        void ActivateGameOverlayToStore(AppId nAppId, EOverlayToStoreFlag eFlag);
        void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby);

        void NotifyGameOverlayStateChanged(bool bActive);
        void NotifyGameServerChangeRequested(string pchServerAddress, string pchPassword);
        bool NotifyLobbyJoinRequested(AppId nAppId, CSteamID steamIDLobby, CSteamID steamIDFriend);
        bool NotifyRichPresenceJoinRequested(AppId nAppId, CSteamID steamIDFriend, string pchConnectString);

        EClanRelationship GetClanRelationship(CSteamID steamIDclan);

        EClanRank GetFriendClanRank(CSteamID steamIDUser, CSteamID steamIDClan);

        bool VoiceIsAvailable();

        void TestVoiceDisconnect(HVoiceCall hVoiceCall);
        void TestChatRoomPeerDisconnect(CSteamID steamIDChat, CSteamID steamIDSpeaker);
        void TestVoicePacketLoss(float flFractionOfIncomingPacketsToDrop);

        HVoiceCall FindFriendVoiceChatHandle(CSteamID steamIDFriend);

        void RequestFriendsWhoPlayGame(CGameID gameID);
        uint GetCountFriendsWhoPlayGame(CGameID gameID);


        CSteamID GetFriendWhoPlaysGame(uint iIndex, CGameID gameID);

        void SetPlayedWith(CSteamID steamIDUserPlayedWith);

        SteamAPICall RequestClanOfficerList(CSteamID steamIDClan);

        CSteamID GetClanOwner(CSteamID steamIDClan);

        int GetClanOfficerCount(CSteamID steamIDClan);
        CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer);
        
        
        EUserRestriction GetUserRestrictions();

        SteamAPICall RequestFriendProfileInfo(CSteamID steamIDFriend);
        // Available keys: TimeCreated, RealName, CityName, StateName, CountryName, Headline, Playtime, Summary
        string GetFriendProfileInfo(CSteamID steamIDFriend, string pchKey);

        bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString);

        int GetOnlineConsoleFriendCount();

        SteamAPICall RequestTrade(CSteamID steamIDPartner);
        void TradeResponse(uint unTradeRequestID, bool bAccept);
        void CancelTradeRequest(CSteamID steamIDPartner);

        bool HideFriend(CSteamID steamIDFriend, bool bHide);
        string GetFriendFacebookName(CSteamID steamIDFriend);
        ulong GetFriendFacebookID(CSteamID steamIDFriend);

        SteamAPICall GetFollowerCount(CSteamID steamID);
        SteamAPICall IsFollowing(CSteamID steamID);
        SteamAPICall EnumerateFollowingList(uint uStartIndex);

        void RequestFriendMessageHistory(CSteamID steamIDFriend);
        void RequestFriendMessageHistoryForOfflineMessages();

        int GetCountFriendsWithOfflineMessages();
        uint GetFriendWithOfflineMessage(int iFriend);
        void ClearFriendHasOfflineMessage(uint uFriend);

        void RequestEmoticonList();
        int GetEmoticonCount();
        string GetEmoticonName(int iEmoticon);

        void ClientLinkFilterInit();
        uint LinkDisposition(string url);
    }
}
