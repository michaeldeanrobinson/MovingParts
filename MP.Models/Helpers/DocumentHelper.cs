using System;
using System.Security.Principal;

namespace MP.Models.Helpers
{
    public static class DocumentHelper
    {
        public static void Initialize(IRequestModel document, IPrincipal principal)
        {
            document.UserId = Guid.Parse(principal.Identity.Name);
            document.ProcessTag = Guid.NewGuid();
        }
    }
}
