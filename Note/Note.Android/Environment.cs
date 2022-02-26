using Android.OS;
using Note.Interfaces;
using Xamarin.Essentials;

namespace Note.Droid
{
    public class Environment : IEnvironment
    {
        [System.Obsolete]
        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt > BuildVersionCodes.Lollipop)
            {
                var window = Platform.CurrentActivity.Window;
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                window.SetStatusBarColor(color.ToPlatformColor());

                if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                {
                    var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                    window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
                }
            }
        }
    }
}