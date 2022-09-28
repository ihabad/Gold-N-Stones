using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GoldAndSilver.Extensions
{
    public static class IEnumerableExtension
    { //Based on the object's id and property the method converts the object and returns a select list item

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, int selectedValue)
        {

            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),

                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                   };
        }

    }

}
