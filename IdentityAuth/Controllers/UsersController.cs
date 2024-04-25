﻿using IdentityAuth.DTOs;
using IdentityAuth.Models;
using IdentityAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IdentityAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> Login(LoginDTO loginDTO)
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

            var token = await _authService.GenerateToken(user);

            return Ok(token);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser() 
        {

            var result = await _userManager.Users.ToListAsync();
                
            return Ok(result);

        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<string>> GetAllUsers1()
        {
            var result = await _userManager.Users.ToListAsync();

            return Ok("Student keldi");
        }

    }
}
