namespace GoldAndSilver.Utility
{
    //this detail file will contain all the constant for the website
    public static class StaticDetail
    {

        public const string DefaultGoldImage = "Default-img.jpg";
        public const string ManagerUser = "Manager";
        public const string CustomerEndUser = "Customer";

        public const string ssShoppingCartCount = "ssCartCount";


        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


    }
}
