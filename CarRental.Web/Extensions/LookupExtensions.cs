using CarRental.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CarRental.Web.Extensions
{
    public static class LookupExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items) where T : ILookup
        {
            return items.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()

            });
        }
    }
}
