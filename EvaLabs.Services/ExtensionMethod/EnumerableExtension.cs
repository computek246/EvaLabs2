using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaLabs.Services.ExtensionMethod
{
    public static class EnumerableExtension
    {
        public static SelectList AsSelectList(this IEnumerable enumerable,
            string dataValueField,
            string dataTextField,
            object selectedValue = null)
        {
            return new(enumerable, dataValueField, dataTextField, selectedValue);
        }


        public static SelectList AsSelectList(this IEnumerable enumerable,
            string dataValueField,
            string dataTextField,
            string dataGroupField,
            object selectedValue = null)
        {
            return new(enumerable, dataValueField, dataTextField, selectedValue, dataGroupField);
        }
    }
}
