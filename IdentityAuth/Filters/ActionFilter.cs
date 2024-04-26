using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityAuth.Filters
{
    public class ActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action ishga tushishi");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action yakunlandi");
        }
    }
}
