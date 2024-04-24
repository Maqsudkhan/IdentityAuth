using IdentityAuth.DTOs;

namespace IdentityAuth.Services
{
    public interface IAuthService
    {
        public Task<AuthDTO> GenerateToken(AuthDTO user);
    }
}
