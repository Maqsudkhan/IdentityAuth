using IdentityAuth.DTOs;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IdentityAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDTO role)
        {
            var result = await _roleManager.FindByNameAsync(role.RoleName);
            if (result == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
                return Ok(new ResponseDTO
                {
                    Message = "Role created✅",
                    IsSuccess = true,
                    StatusCode = 201
                });
            }
            return Ok(new ResponseDTO
            {
                Message = "Role can not created❗",
                StatusCode = 403
            }); 
        }

        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRoleById(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
           
            var result = await _roleManager.DeleteAsync(role!);
            if (!result.Succeeded) 
            {
                return Ok("Error occured❗");
            }
            return Ok(result);
        }

    }
}
