using Marka_WebAPI.Identity;
using Marka_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            if (_userManager.Users != null)
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
                if (user != null)
                {
                    return user;
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser(UserModel model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
            };
            await _userManager.CreateAsync(user, model.Password);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            var user = await _userManager.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;
            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_userManager.Users == null)
            {
                return NotFound();
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(i=> i.Id == id);
            if (user==null)
            {
                return NotFound();
            }
            _userManager.DeleteAsync(user);
            return NoContent();
        }
        private bool UserExists(string id)
        {
            return (_userManager.Users?.Any(u => u.Id == id)).GetValueOrDefault();
        }
    }
}
