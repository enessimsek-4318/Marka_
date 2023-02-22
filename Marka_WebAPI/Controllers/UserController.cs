using Marka_WebAPI.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marka_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            if (_userManager.Users!=null)
            {
                return await _userManager.Users.ToListAsync();
            }
            return NotFound();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string Id)
        {
            if (_userManager.Users != null)
            {
                var user = await _userManager.Users.Where(i => i.Id == Id).FirstOrDefaultAsync();
                if (user!=null)
                {
                    return user;
                }
                return NotFound();
            }
            return NotFound();
        }
    }
}
