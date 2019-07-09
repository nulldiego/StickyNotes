
using Android.App;
using Android.Content.PM;
using Android.OS;
using StickyNotes.Droid;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace StickyNotes.Droid
{
    [Activity(Label = "Sticky Notes", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);

            CreateNotificationChannel();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }
            
            var channel = new NotificationChannel(CHANNEL_ID, "Sticky Notes", NotificationImportance.Default)
            {
                Description = "Sticky Notes"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}