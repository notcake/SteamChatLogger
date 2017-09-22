using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("SteamFriends015")]
    public interface ISteamFriends015
    {
        string GetPersonaName();
        SteamAPICall SetPersonaName(string pchPersonaName);
        EPersonaState GetPersonaState();
        int GetFriendCount(int iFriendFlags);
        CSteamID GetFriendByIndex(int iFriend, int iFriendFlags);
        EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend);
        EPersonaState GetFriendPersonaState(CSteamID steamIDFriend);
        string GetFriendPersonaName(CSteamID steamIDFriend);
        bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo pFriendGameInfo);
        string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName);
        int GetFriendSteamLevel(CSteamID steamIDFriend);
        string GetPlayerNickname(CSteamID steamIDPlayer);
        int GetFriendsGroupCount();
        FriendsGroupID GetFriendsGroupIDByIndex(int iFG);
        string GetFriendsGroupName(FriendsGroupID friendsGroupID);
        int GetFriendsGroupMembersCount(FriendsGroupID friendsGroupID);
        void GetFriendsGroupMembersList(FriendsGroupID friendsGroupID, out CSteamID[] pOutSteamIDMembers, int nMembersCount);
        bool HasFriend(CSteamID steamIDFriend, int iFriendFlags);
        int GetClanCount();
        CSteamID GetClanByIndex(int iClan);
        string GetClanName(CSteamID steamIDClan);
        string GetClanTag(CSteamID steamIDClan);
        bool GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting);
        SteamAPICall DownloadClanActivityCounts(out CSteamID[] psteamIDClans, int cClansToRequest);
        int GetFriendCountFromSource(CSteamID steamIDSource);
        CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend);
        bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource);
        void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking);
        void ActivateGameOverlay(string pchDialog);
        void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID);
        void ActivateGameOverlayToWebPage(string pchURL);
        void ActivateGameOverlayToStore(AppId nAppID, EOverlayToStoreFlag eFlag);
        void SetPlayedWith(CSteamID steamIDUserPlayedWith);
        void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby);
        int GetSmallFriendAvatar(CSteamID steamIDFriend);
        int GetMediumFriendAvatar(CSteamID steamIDFriend);
        int GetLargeFriendAvatar(CSteamID steamIDFriend);
        bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly);
        SteamAPICall RequestClanOfficerList(CSteamID steamIDClan);
        CSteamID GetClanOwner(CSteamID steamIDClan);
        int GetClanOfficerCount(CSteamID steamIDClan);
        CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer);
        uint GetUserRestrictions();
        bool SetRichPresence(string pchKey, string pchValue);
        void ClearRichPresence();
        string GetFriendRichPresence(CSteamID steamIDFriend, string pchKey);
        int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend);
        string GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey);
        void RequestFriendRichPresence(CSteamID steamIDFriend);
        bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString);
        int GetCoplayFriendCount();
        CSteamID GetCoplayFriend(int iCoplayFriend);
        int GetFriendCoplayTime(CSteamID steamIDFriend);
        AppId GetFriendCoplayGame(CSteamID steamIDFriend);
        SteamAPICall JoinClanChatRoom(CSteamID steamIDClan);
        bool LeaveClanChatRoom(CSteamID steamIDClan);
        int GetClanChatMemberCount(CSteamID steamIDClan);
        CSteamID GetChatMemberByIndex(CSteamID steamIDClan, int iUser);
        bool SendClanChatMessage(CSteamID steamIDClanChat, string pchText);
        int GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, out string prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter);
        bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser);
        bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat);
        bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat);
        bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat);
        bool SetListenForFriendsMessages(bool bInterceptEnabled);
        bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend);
        int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, byte[] pvData, int cubData, out EChatEntryType peChatEntryType);
        SteamAPICall GetFollowerCount(CSteamID steamID);
        SteamAPICall IsFollowing(CSteamID steamID);
        SteamAPICall EnumerateFollowingList(uint unStartIndex);
    }

    public static class ISteamFriends015Extension
    {
        public static (EChatEntryType, string) GetFriendMessage(this ISteamFriends015 steamFriends, CSteamID steamIDFriend, int iMessageID)
        {
            int cubData = steamFriends.GetFriendMessage(steamIDFriend, iMessageID, null, 0, out _);
            byte[] message = new byte[cubData];
            steamFriends.GetFriendMessage(steamIDFriend, iMessageID, message, message.Length, out EChatEntryType eChatEntryType);

            return (eChatEntryType, Encoding.UTF8.GetString(message, 0, Math.Max(0, cubData - 1)));
        }
    }
}
