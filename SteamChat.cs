using Steam;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamChatLogger
{
    public class SteamChat : IDisposable
    {
        public event Action Committed;

        public CSteamID SteamId { get; }
        public string Directory { get; }

        public SteamChat(CSteamID steamId, string directory)
        {
            this.SteamId = steamId;
            this.Directory = directory;
        }

        #region IDisposable
        public void Dispose()
        {
            this.CloseLogFile();
        }
        #endregion

        #region SteamChat
        public void OnMessage(RTime32 timestamp, CSteamID senderId, string senderName, string message)
        {
            this.AppendLine(timestamp, senderName + ": " + message);
        }

        public void OnLostConnection(RTime32 timestamp)
        {
            this.AppendLine(timestamp, "Your state is set to Offline.");
        }

        public void OnRegainedConnection(RTime32 timestamp)
        {
            this.AppendLine(timestamp, "Online now and rejoined chat.");
        }

        public void OnStatusChanged(RTime32 timestamp, CSteamID userId, string userName, EPersonaState status)
        {
            this.AppendLine(timestamp, userName + " is now " + status.ToDisplayString() + ".");
        }

        public void OnGameChanged(RTime32 timestamp, CSteamID userId, string userName, FriendGameInfo friendGameInfo, string gameName)
        {
            if (friendGameInfo.m_gameID.m_nAppID.Value == 0) { return; }
            if (friendGameInfo.m_unGameIP == 0) { return; }

            this.AppendLine(timestamp, userName + " is now playing " + gameName + " (" + friendGameInfo.m_GameIP.ToString() + ":" + friendGameInfo.m_usGamePort.ToString() + ").");
        }

        public void OnUserInvited(RTime32 timestamp, CSteamID userId, string userName)
        {
            this.AppendLine(timestamp, userName + " has been invited to chat.");
        }

        public void OnUserEntered(RTime32 timestamp, CSteamID userId, string userName)
        {
            this.AppendLine(timestamp, userName + " entered chat.");
        }

        public void OnUserLeft(RTime32 timestamp, CSteamID userId, string userName)
        {
            this.AppendLine(timestamp, userName + " left chat.");
        }

        public void OnUserDisconnected(RTime32 timestamp, CSteamID userId, string userName)
        {
            this.AppendLine(timestamp, userName + " disconnected.");
        }

        public void OnUserKicked(RTime32 timestamp, CSteamID userId, string userName, CSteamID kickerId, string kickerName)
        {
            this.AppendLine(timestamp, userName + " was kicked by " + kickerName + ".");
        }

        public void OnUserBanned(RTime32 timestamp, CSteamID userId, string userName, CSteamID bannerId, string bannerName)
        {
            this.AppendLine(timestamp, userName + " was banned by " + bannerName + ".");
        }

        public void OnSelfKicked(RTime32 timestamp, CSteamID kickerId, string kickerName)
        {
            this.AppendLine(timestamp, "You have been kicked from the chat by " + kickerName + ".");
        }

        public void OnSelfBanned(RTime32 timestamp, CSteamID bannerId, string bannerName)
        {
            this.AppendLine(timestamp, "You have been banned from the chat by " + bannerName + ".");
        }

        private bool locked = false;
        private bool officersOnly = false;
        public void OnGroupChatStateChanged(RTime32 timestamp, CSteamID userId, string userName, bool locked, bool officersOnly)
        {
            if (this.locked != locked)
            {
                if (locked)
                {
                    this.AppendLine(timestamp, "The chat room has been locked by " + userName + ".");
                }
                else
                {
                    this.AppendLine(timestamp, "The chat room has been unlocked by " + userName + ".");
                }
            }
            if (this.officersOnly != officersOnly)
            {
                if (officersOnly)
                {
                    this.AppendLine(timestamp, "The chat room has been set to officers-only chat by " + userName + ".");
                }
                else
                {
                    this.AppendLine(timestamp, "The chat room has been set to all-users chat by " + userName + ".");
                }
            }

            this.locked = locked;
            this.officersOnly = officersOnly;
        }

        public void SetGroupChatState(bool locked, bool officersOnly)
        {
            this.locked = locked;
            this.officersOnly = officersOnly;
        }

        public void OnOpen()
        {
        }

        public void OnEscalated(RTime32 timestamp, CSteamID friendId, string friendName)
        {
            this.AppendLine(timestamp, "Your chat with " + friendName + " is now a multi-user chat.");
        }

        public void OnClose()
        {
            this.Dispose();
        }
        #endregion

        #region Internal
        private int logFileYear  = -1;
        private int logFileMonth = -1;
        private int logFileDay   = -1;
        private FileStream logFileStream = null;
        private string monthDirectory = null;
        private void AdvanceTime(RTime32 timestamp)
        {
            this.AdvanceTime(DateTimeOffset.FromUnixTimeSeconds(timestamp.Value));
        }

        private void AdvanceTime(DateTimeOffset dateTimeOffset)
        {
            DateTime dateTime = dateTimeOffset.LocalDateTime;
            if (dateTime.Year  != this.logFileYear ||
                dateTime.Month != this.logFileMonth ||
                dateTime.Day   != this.logFileDay)
            {
                this.logFileYear  = dateTime.Year;
                this.logFileMonth = dateTime.Month;
                this.logFileDay   = dateTime.Day;

                this.monthDirectory = Path.Combine(this.Directory, dateTime.ToString("yyyyMM MMMMM"));
                string logFilePath = Path.Combine(this.monthDirectory, dateTime.ToString("dd dddd") + ".txt");
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                this.CloseLogFile();

                bool logFileAlreadyExisted = File.Exists(logFilePath);
                this.logFileStream = File.Open(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete);

                if (logFileAlreadyExisted)
                {
                    this.AppendLine("");
                }
                else
                {
                    this.AppendLine("\uFEFF" + dateTime.ToString("dd MMMM yyyy"));
                }
            }
        }

        private void AppendLine(RTime32 timestamp, string line)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp.Value);
            this.AdvanceTime(dateTimeOffset);
            this.AppendLine(dateTimeOffset.ToString("HH:mm") + " - " + line);
        }

        private void AppendLine(string line)
        {
            byte[] data = Encoding.UTF8.GetBytes(line + "\r\n");
            this.logFileStream.Write(data, 0, data.Length);
            this.logFileStream.Flush(true);

            Debug.WriteLine(line);

            this.Committed?.Invoke();
        }

        private void CloseLogFile()
        {
            if (this.logFileStream == null) { return; }

            this.logFileStream.Close();
            this.logFileStream.Dispose();
            this.logFileStream = null;

            this.Committed?.Invoke();
        }
        #endregion
    }
}
