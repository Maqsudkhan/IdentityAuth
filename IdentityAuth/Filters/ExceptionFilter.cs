using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityAuth.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Xato yuz berdi: {context.Exception.Message}");
        }
    }
}
