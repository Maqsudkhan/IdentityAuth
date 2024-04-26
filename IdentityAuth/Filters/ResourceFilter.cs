using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityAuth.Filters
{
    public class ResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resurs boshlanmoqda");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resurs yakunlandi");
        }
    }
}
