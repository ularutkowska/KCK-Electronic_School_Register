using System.Globalization;
using System.Resources;


namespace KCK_Elektroniczny_Dziennik_Szkolny.Models
{
    public static class LanguageManager
    {
        private static ResourceManager resourceManager = new ResourceManager("KCK_Elektroniczny_Dziennik_Szkolny.Resources.Strings", typeof(LanguageManager).Assembly);
        private static CultureInfo currentCulture = new CultureInfo("pl"); // domyślnie angielski

        public static string GetString(string key)
        {
            var result = resourceManager.GetString(key, currentCulture);
            return result ?? $"Missing translation for: {key}";
        }

        public static void SetLanguage(string languageCode)
        {
            currentCulture = new CultureInfo(languageCode);
        }
    }
}
