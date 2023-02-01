using Microsoft.AspNetCore.Identity;

namespace Marka_WebUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
