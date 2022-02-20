using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Note.Droid.CustomRenders;

#pragma warning disable CS0612
[assembly: ExportRenderer(typeof(Entry), typeof(NoUnderlineEntry))]
[assembly: ExportRenderer(typeof(Editor), typeof(NoUnderlineEditor))]
#pragma warning restore CS0612

namespace Note.Droid
{
    public class CustomRenders
    {
        [System.Obsolete]
        public class NoUnderlineEntry : EntryRenderer
        {
            protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
            {
                base.OnElementChanged(e);
                Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }

        [System.Obsolete]
        public class NoUnderlineEditor : EditorRenderer
        {
            protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
            {
                base.OnElementChanged(e);
                Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}