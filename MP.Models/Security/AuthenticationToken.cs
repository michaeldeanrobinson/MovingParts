using System;

namespace MP.Models.Security
{
    public class AuthenticationToken
    {
        public string UTCTimestamp { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }
    }
}
