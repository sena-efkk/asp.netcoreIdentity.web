using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace asp.netcoreIdentityApp.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrorList(this ModelStateDictionary modelstate, List<string> errors)
        {
            errors.ForEach(x =>
            {
                modelstate.AddModelError(string.Empty, x);
            });

        }
    }
}