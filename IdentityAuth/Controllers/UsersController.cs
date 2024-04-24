using IdentityAuth.DTOs;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IdentityAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                FullName = registerDto.FullName,
                UserName = registerDto.Email, 
                Email = registerDto.Email,
                Age = registerDto.Age,
                Status = registerDto.Status
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            foreach(var role in registerDto.Roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            
            if (user is null)
            {
                return Unauthorized("User not found with email❗");
            }
            var test = await _userManager.CheckPasswordAsync(user, loginDTO.Password);     
            if (!test)
            {
                return Unauthorized("Password invalid❗");
            }
            return Ok("Xush kelibsiz✅");
        }



        [HttpGet]
        public async Task<IActionResult> GetAllUser() 
        {

            var result = await _userManager.Users.ToListAsync();
                
            return Ok(result);

        }

    }
}
