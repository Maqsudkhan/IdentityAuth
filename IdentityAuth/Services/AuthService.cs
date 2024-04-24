using IdentityAuth.DTOs;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthDTO> GenerateToken(AppUser user)
        {
            if (user == null)
            {
                return new AuthDTO()
                {
                    Message = "User is null",
                    StatusCode = 404
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings").GetSection("SecurityKey").Value!);
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:SecurityKey"]!);

            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims =
                [
                    new(JwtRegisteredClaimNames.Email, user.Email!),
                    new(JwtRegisteredClaimNames.Name, user.FullName),
                    new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                    new(JwtRegisteredClaimNames.NameId, user.Id),
                    new(JwtRegisteredClaimNames.Aud, _configuration["JWTSettings:ValidAudience"]!),
                    new(JwtRegisteredClaimNames.Iss, _configuration["JWTSettings:ValidIssuer"]!),
                    new(JwtRegisteredClaimNames.Exp, _configuration["JWTSettings:ExpireDate"]!)
                ];

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWTSettings:ExpireDate"]!)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Message = "Successfully created token",
                StatusCode = 200,
                isSuccess = true
            };


        }
    }
}
