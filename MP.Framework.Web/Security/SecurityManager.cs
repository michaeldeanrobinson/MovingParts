using System;
using MP.Framework.Security.Encryption;
using MP.Framework.Utility;
using MP.Models.Security;

namespace MP.Framework.Web.Security
{
    public class SecurityManager : ISecurityManager
    {
        public static readonly TimeSpan TokenValidTimeSpan = new TimeSpan(Settings.TokenValidTimeSpan, 0, 0, 0);
        private static readonly AesHelper _Aes = new AesHelper();

        public bool IsValidToken(AuthenticationToken authToken, string userAgent, string clientIp)
        {
            DateTime validWindow = DateTime.Now.Add(-TokenValidTimeSpan);
            bool userAgentResult = authToken.UserAgent.Equals(userAgent, StringComparison.OrdinalIgnoreCase);
            bool ipAddressResult = authToken.IPAddress.Equals(clientIp);
            // DateTime.Parse will convert the UTC time to local, so we compare against local time
            bool timestampResult = DateTime.Parse(authToken.UTCTimestamp) > validWindow;

            Factory.LogManager.Logger.LogInfo($"Token:Actual - '{authToken.UserAgent}' == '{userAgent}' = {userAgentResult}");
            Factory.LogManager.Logger.LogInfo($"Token:Actual - '{authToken.IPAddress}' == '{clientIp}' = {ipAddressResult}");
            Factory.LogManager.Logger.LogInfo($"Token:Actual - '{DateTime.Parse(authToken.UTCTimestamp)}' > '{validWindow}' = {timestampResult}");

            return userAgentResult && ipAddressResult && timestampResult;
        }

        public bool IsValidTokenEnvironmentIndependent(AuthenticationToken authToken)
        {
            if (authToken == null)
            {
                return false;
            }

            DateTime validWindow = DateTime.Now.Add(-TokenValidTimeSpan);
            // DateTime.Parse will convert the UTC time to local, so we compare against local time
            bool timestampResult = DateTime.Parse(authToken.UTCTimestamp) > validWindow;

            Factory.LogManager.Logger.LogInfo($"Token:Actual - '{DateTime.Parse(authToken.UTCTimestamp)}' > '{validWindow}' = {timestampResult}");

            return timestampResult;
        }

        public Guid GetUserIdFromToken(AuthenticationToken authToken)
        {
            return authToken != null ? authToken.UserId : Guid.Empty;
        }

        public AuthenticationToken DecryptAuthenticationToken(string token)
        {
            try
            {
                return ObjectHelper.ToObject<AuthenticationToken>(_Aes.Decrypt(token));
            }
            catch
            {
                return null;
            }
        }
    }
}
