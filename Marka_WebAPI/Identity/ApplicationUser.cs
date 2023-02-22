using Microsoft.AspNetCore.Identity;

namespace Marka_WebAPI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
