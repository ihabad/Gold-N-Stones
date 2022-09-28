namespace GoldAndSilver.Extensions
{
    public static class ReflectionExtension
    {
        //this extension method gets the value of whatever property we pass here
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
