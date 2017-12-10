using System;
using MP.Models.Security;

namespace MP.Framework.Web.Security
{
    public interface ISecurityManager
    {
        bool IsValidToken(AuthenticationToken authToken, string userAgent, string clientIp);
        Guid GetUserIdFromToken(AuthenticationToken authToken);
        AuthenticationToken DecryptAuthenticationToken(string token);
    }
}
