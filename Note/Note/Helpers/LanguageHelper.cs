using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;

namespace Note.Helpers
{
    public class LanguageHelper
    {
        public static string GetCurrentLanguage()
        {
            string language = Preferences.Get("language", string.Empty);

            if (string.IsNullOrEmpty(language))
                Preferences.Set("language", LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            else
                LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(language);

            return "en";
        }

        public static void SwitchLanguage(string value)
        {
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(value);
            Preferences.Set("language", LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
        }
    }
}
