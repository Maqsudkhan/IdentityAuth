using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityAuth.Filters
{
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Natija boshlanmoqda");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Natija yakunlandi");
        }
    }
}
