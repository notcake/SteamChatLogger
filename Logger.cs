using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using Steam;
using Steam.Callbacks;
using System.Runtime.InteropServices;

namespace SteamChatLogger
{
    public class Logger : IDisposable
    {
        public event Action<bool> EnabledChanged;
        public event Action<bool> ConnectedChanged;
        public event Action Committed;

        public bool Enabled { get; private set; }
        public bool Connected { get; private set; }

        private AutoResetEvent ConnectionThreadAbortionEvent = new AutoResetEvent(false);
        private Thread ConnectionThread = null;

        private AutoResetEvent ListenerThreadAbortionEvent = new AutoResetEvent(false);
        private Thread ListenerThread = null;

        private SteamClientDll SteamClientDll = null;
        private ISteamClient017 SteamClient = null;
        private ISteamFriends015 SteamFriends = null;
        private ISteamAppList001 SteamAppList = null;
        private ISteamUser019 SteamUser = null;
        private IClientEngine004 ClientEngine = null;
        private IClientFriends001 ClientFriends = null;
        private IClientApps001 ClientApps = null;
        private HSteamPipe HSteamPipe = HSteamPipe.Null;
        private HSteamUser HSteamUser = HSteamUser.Null;

        private CSteamID LocalSteamId = new CSteamID(0);

        private List<LogDirectory> logDirectories = new List<LogDirectory>();

        public Logger()
        {
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dropbox", "Logs");
            this.logDirectories.Add(new LogDirectory(logDirectory));
            this.logDirectories.Add(new LogDirectory(Path.Combine(logDirectory, "Anticheat Antisupport")));
            this.logDirectories.Add(new LogDirectory(Path.Combine(logDirectory, "Anticheat Leak Control")));
            this.logDirectories.Add(new LogDirectory(Path.Combine(logDirectory, "Anticheat Leak Support")));
            this.logDirectories.Add(new LogDirectory(Path.Combine(logDirectory, "Anticheat Miscellaneous")));
            this.logDirectories.Add(new LogDirectory(Path.Combine(logDirectory, "Anticheat Support")));
        }

        #region IDisposable
        public void Dispose()
        {
            this.Disable();
        }
        #endregion

        #region Logger
        public void Enable()
        {
            if (this.Enabled) { return; }

            this.Enabled = true;
            this.EnabledChanged?.Invoke(this.Enabled);
            
            this.ConnectionThread = new Thread(this.ConnectionThreadProc);
            this.ConnectionThread.Start();
        }

        public void Disable()
        {
            if (!this.Enabled) { return; }

            this.ConnectionThreadAbortionEvent.Set();
            this.ConnectionThread?.Join();
            this.ConnectionThread = null;
            this.ConnectionThreadAbortionEvent.Reset();

            this.Disconnect();

            this.Enabled = false;
            this.EnabledChanged?.Invoke(this.Enabled);
        }

        public IEnumerable<string> LogDirectories => this.logDirectories.Select(x => x.Directory);
        #endregion

        private bool Connect()
        {
            if (this.Connected) { return true; }

            this.SteamClientDll = this.SteamClientDll ?? SteamClientDll.Wrangle();
            if (this.SteamClientDll == null) { return false; }

            this.SteamClient  = this.SteamClient  ?? this.SteamClientDll.SteamClient;
            this.ClientEngine = this.ClientEngine ?? this.SteamClientDll.ClientEngine;

            this.HSteamPipe = this.SteamClient.CreateSteamPipe();
            if (this.HSteamPipe.Value == 0) { return false; }
            
            this.HSteamUser = this.SteamClient.ConnectToGlobalUser(this.HSteamPipe);
            this.SteamFriends = this.SteamClient.GetISteamFriends(this.HSteamUser, this.HSteamPipe);
            this.SteamAppList = this.SteamClient.GetISteamAppList(this.HSteamUser, this.HSteamPipe);
            this.SteamUser = this.SteamClient.GetISteamUser(this.HSteamUser, this.HSteamPipe);

            this.ClientFriends = this.ClientEngine.GetIClientFriends(this.HSteamUser, this.HSteamPipe);
            this.ClientApps = this.ClientEngine.GetIClientApps(this.HSteamUser, this.HSteamPipe);

            this.LocalSteamId = this.SteamUser.GetSteamID();
            Debug.WriteLine("Local SteamID is " + this.LocalSteamId.UInt64.ToString());

            this.ListenerThread = new Thread(this.ListenerThreadProc);
            this.ListenerThread.Start();

            this.Connected = true;
            this.ConnectedChanged?.Invoke(this.Connected);

            return true;
        }

        private void Disconnect()
        {
            if (!this.Connected) { return; }

            this.ListenerThreadAbortionEvent.Set();
            this.ListenerThread.Join();
            this.ListenerThread = null;
            this.ListenerThreadAbortionEvent.Reset();

            this.Connected = false;
            this.ConnectedChanged?.Invoke(this.Connected);
        }

        private void ConnectionThreadProc()
        {
            do
            {
                if (this.Connect()) { break; }
            }
            while (!this.ConnectionThreadAbortionEvent.WaitOne(1000));
        }

        private void ListenerThreadProc()
        {
            // Scan existing group chats
            int chatCount = this.ClientFriends.GetChatRoomCount();
            for (int i = 0; i < chatCount; i++)
            {
                CSteamID chatId = this.ClientFriends.GetChatRoomByIndex(i);
                this.GetChat(chatId);
            }

            // Listen for events
            do
            {
                // Check if Steam is still running.
                this.SteamUser.BLoggedOn();

                while (this.SteamClientDll.Steam_BGetCallback(this.HSteamPipe, out CallbackMsg callbackMsg))
                {
                    switch (callbackMsg.m_iCallback)
                    {
                        case CallbackType.IPCFailure:
                            {
                                IPCFailure data = callbackMsg.GetCallbackData<IPCFailure>();
                                this.Disconnect();

                                if (this.Enabled)
                                {
                                    this.ConnectionThread = new Thread(this.ConnectionThreadProc);
                                    this.ConnectionThread.Start();
                                }
                            }
                            break;
                        case CallbackType.SteamServersConnected:
                            {
                                SteamServersConnected data = callbackMsg.GetCallbackData<SteamServersConnected>();

                                this.LocalSteamId = this.SteamUser.GetSteamID();
                                Debug.WriteLine("Local SteamID is " + this.LocalSteamId.UInt64.ToString());
                            }
                            break;
                        case CallbackType.PersonaStateChange:
                            {
                                PersonaStateChange data = callbackMsg.GetCallbackData<PersonaStateChange>();

                                // Connection lost and regained events
                                if (data.m_ulSteamID == this.LocalSteamId)
                                {
                                    if (data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeComeOnline))
                                    {
                                        foreach (SteamChat chat in this.chats.Values)
                                        {
                                            chat.OnRegainedConnection(RTime32.Now);
                                        }
                                    }
                                    else if (data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeGoneOffline))
                                    {
                                        foreach (SteamChat chat in this.chats.Values)
                                        {
                                            chat.OnLostConnection(RTime32.Now);
                                        }
                                    }
                                }

                                // Remote user status
                                if (!data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeComeOnline) &&
                                    !data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeGoneOffline))
                                {
                                    SteamChat chat = this.PeekChat(data.m_ulSteamID);
                                    if (data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeStatus))
                                    {
                                        chat?.OnStatusChanged(RTime32.Now, data.m_ulSteamID, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamID), this.SteamFriends.GetFriendPersonaState(data.m_ulSteamID));
                                    }
                                    else if (data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeGamePlayed) ||
                                             data.m_nChangeFlags.HasFlag(EPersonaChange.k_EPersonaChangeGameServer))
                                    {
                                        this.SteamFriends.GetFriendGamePlayed(data.m_ulSteamID, out FriendGameInfo friendGameInfo);
                                        string gameName = friendGameInfo.m_gameID.m_nAppID.Value != 0 ? this.ClientApps.GetAppData(friendGameInfo.m_gameID.m_nAppID, "name") : null;
                                        chat?.OnGameChanged(RTime32.Now, data.m_ulSteamID, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamID), friendGameInfo, gameName);
                                    }
                                }
                            }
                            break;
                        case CallbackType.FriendChatMsg:
                            {
                                FriendChatMsg data = callbackMsg.GetCallbackData<FriendChatMsg>();
                                SteamChat chat = this.GetChat(data.m_ulFriendID);

                                if (data.m_eChatEntryType == EChatEntryType.k_EChatEntryTypeChatMsg)
                                {
                                    (EChatEntryType type, RTime32 timestamp, CSteamID senderId, string message) = this.ClientFriends.GetChatMessage(data.m_ulFriendID, data.m_iChatID);

                                    // Race condition: FriendMessageHistoryChatLog will rewrite the message indices
                                    //                 if this is a newly opened chat.
                                    if (type == EChatEntryType.k_EChatEntryTypeChatMsg)
                                    {
                                        // We won the race.
                                        chat.OnMessage(timestamp, senderId, this.SteamFriends.GetFriendPersonaName(senderId), message);
                                    }
                                    else
                                    {
                                        // History is loaded, assume no more index shuffling
                                        Debug.WriteLine("FriendChatMsg indices rewritten!");

                                        // Message history can include messages that are marked k_EChatEntryTypeChatMsg!
                                        // Just take the last messages instead.
                                        // This is vulnerable to another race condition if multiple messages are received quickly.
                                        int messageCount = this.ClientFriends.GetChatMessagesCount(data.m_ulFriendID);
                                        (type, timestamp, senderId, message) = this.ClientFriends.GetChatMessage(data.m_ulFriendID, messageCount - 1);

                                        chat.OnMessage(timestamp, senderId, this.SteamFriends.GetFriendPersonaName(senderId), message);
                                    }
                                }
                            }
                            break;
                        case CallbackType.ChatRoomEnter:
                            {
                                ChatRoomEnter data = callbackMsg.GetCallbackData<ChatRoomEnter>();
                                if (data.m_EChatRoomEnterResponse == EChatRoomEnterResponse.k_EChatRoomEnterResponseSuccess)
                                {
                                    SteamChat chat = this.GetChat(data.m_ulSteamIDChat);

                                    // chat.OnOpen() has already been called by GetChat().
                                    if (data.m_ulSteamIDFriendChat.UInt64 != 0)
                                    {
                                        chat.OnEscalated(RTime32.Now, data.m_ulSteamIDFriendChat, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDFriendChat));
                                    }
                                }
                            }
                            break;
                        case CallbackType.ChatMemberStateChange:
                            {
                                ChatMemberStateChange data = callbackMsg.GetCallbackData<ChatMemberStateChange>();
                                SteamChat chat = this.GetChat(data.m_ulSteamIDChat);

                                CSteamID userId = data.m_ulSteamIDUserChanged;
                                string userName = this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDUserChanged);
                                switch (data.m_rgfChatMemberStateChange)
                                {
                                    case EChatMemberStateChange.k_EChatMemberStateChangeEntered:
                                        chat.OnUserEntered(RTime32.Now, userId, userName);
                                        break;
                                    case EChatMemberStateChange.k_EChatMemberStateChangeLeft:
                                        chat.OnUserLeft(RTime32.Now, userId, userName);
                                        break;
                                    case EChatMemberStateChange.k_EChatMemberStateChangeDisconnected:
                                        chat.OnUserDisconnected(RTime32.Now, userId, userName);
                                        break;
                                    case EChatMemberStateChange.k_EChatMemberStateChangeKicked:
                                        chat.OnUserKicked(RTime32.Now, userId, userName, data.m_ulSteamIDMakingChange, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDMakingChange));
                                        break;
                                    case EChatMemberStateChange.k_EChatMemberStateChangeBanned:
                                        chat.OnUserBanned(RTime32.Now, userId, userName, data.m_ulSteamIDMakingChange, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDMakingChange));
                                        break;
                                }
                            }
                            break;
                        case CallbackType.ChatRoomMsg:
                            {
                                ChatRoomMsg data = callbackMsg.GetCallbackData<ChatRoomMsg>();
                                SteamChat chat = this.GetChat(data.m_ulSteamIDChat);

                                CSteamID userId = data.m_ulSteamIDUser;
                                string userName = this.SteamFriends.GetFriendPersonaName(userId);

                                if (data.m_eChatEntryType == EChatEntryType.k_EChatEntryTypeChatMsg)
                                {
                                    (_, _, string message) = this.ClientFriends.GetChatRoomEntry(data.m_ulSteamIDChat, data.m_iChatID);
                                    chat.OnMessage(RTime32.Now, userId, userName, message);
                                }
                            }
                            break;
                        case CallbackType.ChatRoomDlgClose:
                            {
                                ChatRoomDlgClose data = callbackMsg.GetCallbackData<ChatRoomDlgClose>();
                                this.CloseChat(data.m_ulSteamIDChat);
                            }
                            break;
                        case CallbackType.ChatRoomKicking:
                            {
                                ChatRoomKicking data = callbackMsg.GetCallbackData<ChatRoomKicking>();
                                SteamChat chat = this.GetChat(data.m_ulSteamIDChat);
                                chat.OnSelfKicked(RTime32.Now, data.m_ulSteamIDAdmin, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDAdmin));
                            }
                            break;
                        case CallbackType.ChatRoomBanning:
                            {
                                ChatRoomKicking data = callbackMsg.GetCallbackData<ChatRoomKicking>();
                                SteamChat chat = this.GetChat(data.m_ulSteamIDChat);
                                chat.OnSelfBanned(RTime32.Now, data.m_ulSteamIDAdmin, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDAdmin));
                            }
                            break;
                        case CallbackType.ChatRoomCreate:
                            {
                                ChatRoomCreate data = callbackMsg.GetCallbackData<ChatRoomCreate>();
                                if (data.m_eResult == EResult.k_EResultOK)
                                {
                                    SteamChat chat = this.GetChat(data.m_ulSteamIDChat);
                                    chat.OnEscalated(RTime32.Now, data.m_ulSteamIDFriendChat, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDFriendChat));
                                }
                            }
                            break;
                        case CallbackType.ChatRoomActionResult:
                            {
                                ChatRoomActionResult data = callbackMsg.GetCallbackData<ChatRoomActionResult>();
                                if (data.m_EChatAction == EChatAction.k_EChatActionInviteChat &&
                                    data.m_EChatActionResult == EChatActionResult.k_EChatActionResultSuccess)
                                {
                                    SteamChat chat = this.GetChat(data.m_ulSteamIDChat);
                                    chat.OnUserInvited(RTime32.Now, data.m_ulSteamIDUserActedOn, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDUserActedOn));
                                }
                            }
                            break;
                        case CallbackType.ChatRoomInfoChanged:
                            {
                                ChatRoomInfoChanged data = callbackMsg.GetCallbackData<ChatRoomInfoChanged>();
                                SteamChat chat = this.GetChat(data.m_ulSteamIDChat);
                                this.ClientFriends.GetChatRoomLockState(data.m_ulSteamIDChat, out bool locked);
                                bool officersOnly = this.ClientFriends.BChatRoomModerated(data.m_ulSteamIDChat);
                                chat.OnGroupChatStateChanged(RTime32.Now, data.m_ulSteamIDMakingChange, this.SteamFriends.GetFriendPersonaName(data.m_ulSteamIDMakingChange), locked, officersOnly);
                            }
                            break;
                        case CallbackType.FriendMessageHistoryChatLog:
                            {
                                Debug.WriteLine("Received callback " + callbackMsg.m_iCallback.ToString());
                                FriendMessageHistoryChatLog data = callbackMsg.GetCallbackData<FriendMessageHistoryChatLog>();
                                this.GetChat(data.m_ulFriendID);
                            }
                            break;
                        default:
                            // Print unknown callbacks
                            Debug.WriteLine("Received callback " + callbackMsg.m_iCallback.ToString());
                            if (Enum.GetName(typeof(CallbackType), callbackMsg.m_iCallback) == null)
                            {
                                Debug.WriteLine("\t" + BitConverter.ToString(callbackMsg.GetRawData()).Replace('-', ' '));
                            }

                            // Emit warnings for mis-sized callback structs
                            callbackMsg.GetCallbackData();
                            break;
                    }
                    this.SteamClientDll.Steam_FreeLastCallback(this.HSteamPipe);
                }
            }
            while (!this.ListenerThreadAbortionEvent.WaitOne(1000));

            this.SteamClientDll.Steam_ReleaseThreadLocalMemory();
        }

        public int ChatCount => this.chats.Count;

        private Dictionary<CSteamID, SteamChat> chats = new Dictionary<CSteamID, SteamChat>();
        private void CloseChat(CSteamID steamId)
        {
            if (!this.chats.ContainsKey(steamId)) { return; }

            SteamChat chat = this.chats[steamId];
            chat.OnClose();
            chat.Dispose();
            this.chats.Remove(steamId);
        }

        private SteamChat GetChat(CSteamID steamId)
        {
            if (this.chats.ContainsKey(steamId)) { return this.chats[steamId]; }

            string name = null;
            string nickname = null;
            if (steamId.m_EAccountType == EAccountType.k_EAccountTypeIndividual)
            {
                name     = this.SteamFriends.GetFriendPersonaName(steamId);
                nickname = this.SteamFriends.GetPlayerNickname(steamId);
            }
            else if (steamId.m_EAccountType == EAccountType.k_EAccountTypeChat)
            {
                name = this.ClientFriends.GetChatRoomName(steamId);
            }

            string userDirectory = null;
            foreach (LogDirectory logDirectory in this.logDirectories)
            {
                userDirectory = logDirectory.GetUserDirectory(steamId);
                if (userDirectory != null) { break; }
            }

            if (userDirectory == null)
            {
                foreach (LogDirectory logDirectory in this.logDirectories)
                {
                    userDirectory = logDirectory.MatchUserDirectory(steamId, name, nickname);
                    if (userDirectory != null) { break; }
                }

                if (userDirectory == null)
                {
                    userDirectory = this.logDirectories[0].CreateUserDirectory(steamId, name, nickname);
                    Debug.WriteLine("Allocated new directory " + userDirectory + " for " + name);
                }
                else
                {
                    Debug.WriteLine("Matched directory " + userDirectory + " with " + name);
                }
            }
            else
            {
                Debug.WriteLine("Using existing directory " + userDirectory + " for " + name);
            }

            SteamChat chat = new SteamChat(steamId, userDirectory);
            chat.Committed += this.SteamChat_Committed;
            this.chats[chat.SteamId] = chat;
            chat.OnOpen();

            if (steamId.m_EAccountType == EAccountType.k_EAccountTypeChat)
            {
                this.ClientFriends.GetChatRoomLockState(steamId, out bool locked);
                bool officersOnly = this.ClientFriends.BChatRoomModerated(steamId);
                chat.SetGroupChatState(locked, officersOnly);
            }

            return chat;
        }

        private SteamChat PeekChat(CSteamID steamId)
        {
            if (!this.chats.ContainsKey(steamId)) { return null; }

            return this.chats[steamId];
        }

        private void SteamChat_Committed()
        {
            this.Committed?.Invoke();
        }
    }
}
