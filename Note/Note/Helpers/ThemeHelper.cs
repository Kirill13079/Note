using Note.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Note.Helpers
{
    public static class ThemeHelper
    {
        public static void SetTheme()
        {
            var theme = Preferences.Get("theme", string.Empty);
            var environment = DependencyService.Get<IEnvironment>();

            if (string.IsNullOrEmpty(theme) || theme == "light")
            {
                environment?.SetStatusBarColor(Color.FromHex("F2F2F2"), true);
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
            else
            {
                environment?.SetStatusBarColor(Color.FromHex("080808"), false);
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }

        public static void SwitchTheme()
        {
            bool isDark = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);
            var environment = DependencyService.Get<IEnvironment>();

            if (isDark)
            {
                environment?.SetStatusBarColor(Color.FromHex("F2F2F2"), true);
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
            else
            {
                environment?.SetStatusBarColor(Color.FromHex("080808"), false);
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
        }
    }
}
