using IdentityAuth.DTOs;

namespace IdentityAuth.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthDTO> GenerateToken(AuthDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
