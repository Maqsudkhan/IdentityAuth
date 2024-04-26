using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace IdentityAuth.Filters
{

    public class AuthorizationFilter  : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Foydalanuvchi haqida ma'lumot olish
            var user = context.HttpContext.User;

            // Ruxsatni tekshirish
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
