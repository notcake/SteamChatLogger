using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    [InterfaceName("SteamUser019")]
    public interface ISteamUser019
    {
        HSteamUser GetHSteamUser();

        bool BLoggedOn();
        IntPtr GetSteamID(out CSteamID steamId);

        int InitiateGameConnection(byte[] pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, bool bSecure);
        void TerminateGameConnection(uint unIPServer, ushort usPortServer);

        void TrackAppUsageEvent(CGameID gameID, int eAppUsageEvent, string pchExtraInfo = "");

        bool GetUserDataFolder(byte[] pchBuffer, int cubBuffer);

        void StartVoiceRecording();
        void StopVoiceRecording();

        EVoiceResult GetAvailableVoice(out uint pcbCompressed, out uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated = 0);
        EVoiceResult GetVoice(bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, bool bWantUncompressed_Deprecated, byte[] pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, out uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated = 0);
        EVoiceResult DecompressVoice(byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate);

        uint GetVoiceOptimalSampleRate();

        HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket);
        EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, CSteamID steamID);
        void EndAuthSession(CSteamID steamID);
        void CancelAuthTicket(HAuthTicket hAuthTicket);

        EUserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId appID);

        bool BIsBehindNAT();

        void AdvertiseGame(CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer);

        SteamAPICall RequestEncryptedAppTicket(byte[] pDataToInclude, int cbDataToInclude);
        bool GetEncryptedAppTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket);

        int GetGameBadgeLevel(int nSeries, bool bFoil);
        int GetPlayerSteamLevel();

        SteamAPICall RequestStoreAuthURL(string pchRedirectURL);

        bool BIsPhoneVerified();
        bool BIsTwoFactorEnabled();
        bool BIsPhoneIdentifying();
        bool BIsPhoneRequiringVerification();
    }

    public static class ISteamUser019Extension
    {
        public static CSteamID GetSteamID(this ISteamUser019 steamUser)
        {
            steamUser.GetSteamID(out CSteamID steamId);
            return steamId;
        }
    }
}
