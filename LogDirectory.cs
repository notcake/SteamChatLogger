using Newtonsoft.Json;
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
    public class LogDirectory
    {
        private struct MappingFile
        {
            [JsonProperty("subdirectory")]
            public string Subdirectory;
            [JsonProperty("users")]
            public Dictionary<string, string> UserDirectories;
        }

        public string Directory { get; }
        private string subdirectory = null;

        private Dictionary<CSteamID, string> userDirectories = new Dictionary<CSteamID, string>();
        private Dictionary<string, CSteamID> directoryUsers = new Dictionary<string, CSteamID>();

        public LogDirectory(string directory)
        {
            Debug.WriteLine("Log directory: " + directory);
            this.Directory = directory;

            try
            {
                string json = File.ReadAllText(this.MappingPath, Encoding.UTF8);
                MappingFile mapping = JsonConvert.DeserializeObject<MappingFile>(json);

                this.subdirectory = mapping.Subdirectory;
                foreach (KeyValuePair<string, string> entry in mapping.UserDirectories)
                {
                    CSteamID steamId = new CSteamID(ulong.Parse(entry.Key));
                    this.userDirectories[steamId] = entry.Value;
                    this.directoryUsers[entry.Value] = steamId;
                }
            }
            catch (FileNotFoundException)
            {
                this.SaveMapping();
            }
        }

        public string CreateUserDirectory(CSteamID steamId, string name, string nickname)
        {
            string userDirectory = this.MatchUserDirectory(steamId, name, nickname);
            if (userDirectory != null) { return userDirectory; }

            name     = this.SanitizeFileName(name);
            nickname = this.SanitizeFileName(nickname);
            if (name?.Length     == 0) { name = null;     }
            if (nickname?.Length == 0) { nickname = null; }

            string directory = nickname ?? name ?? steamId.UInt64.ToString();
            this.userDirectories[steamId] = directory;
            this.directoryUsers[directory] = steamId;
            this.SaveMapping();

            return this.Subdirectory == null ? Path.Combine(this.Directory, directory) : Path.Combine(this.Directory, directory, this.Subdirectory);
        }

        public string GetUserDirectory(CSteamID steamId)
        {
            if (!this.userDirectories.ContainsKey(steamId)) { return null; }

            string userDirectory = this.userDirectories[steamId];

            return this.Subdirectory == null ? Path.Combine(this.Directory, userDirectory) : Path.Combine(this.Directory, userDirectory, this.Subdirectory);
        }

        public string MatchUserDirectory(CSteamID steamId, string name, string nickname)
        {
            string userDirectory = this.GetUserDirectory(steamId);
            if (userDirectory != null) { return userDirectory; }

            name     = this.SanitizeFileName(name);
            nickname = this.SanitizeFileName(nickname);
            if (name?.Length     == 0) { name = null;     }
            if (nickname?.Length == 0) { nickname = null; }

            if (nickname != null &&
                System.IO.Directory.Exists(Path.Combine(this.Directory, nickname)))
            {
                this.userDirectories[steamId] = nickname;
                this.directoryUsers[nickname] = steamId;
                this.SaveMapping();
                
                return this.Subdirectory == null ? Path.Combine(this.Directory, nickname) : Path.Combine(this.Directory, nickname, this.Subdirectory);
            }
            else if (name != null &&
                     System.IO.Directory.Exists(Path.Combine(this.Directory, name)))
            {
                this.userDirectories[steamId] = name;
                this.directoryUsers[name] = steamId;
                this.SaveMapping();

                return this.Subdirectory == null ? Path.Combine(this.Directory, name) : Path.Combine(this.Directory, name, this.Subdirectory);
            }

            return null;
        }

        public void SaveMapping()
        {
            MappingFile mapping = new MappingFile();
            mapping.Subdirectory = this.subdirectory;
            mapping.UserDirectories = new Dictionary<string, string>();
            foreach (KeyValuePair<CSteamID, string> entry in this.userDirectories)
            {
                string steamId = entry.Key.UInt64.ToString();
                mapping.UserDirectories[steamId] = entry.Value;
            }
            
            File.WriteAllText(this.MappingPath, JsonConvert.SerializeObject(mapping, Formatting.Indented) + "\n");
        }

        public string Subdirectory
        {
            get
            {
                return this.subdirectory;
            }
            set
            {
                this.subdirectory = value;
                this.SaveMapping();
            }
        }

        private string MappingPath => Path.Combine(this.Directory, "mapping.json");

        private string SanitizeFileName(string name)
        {
            if (name == null) { return null; }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c.ToString(), "");
            }

            return name;
        }
    }
}
