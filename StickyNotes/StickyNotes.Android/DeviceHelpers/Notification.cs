using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StickyNotes.Model;
using StickyNotes.Settings;
using StickyNotes.DeviceHelper;
using Xamarin.Forms;

[assembly: Dependency(typeof(StickyNotes.Droid.DeviceHelpers.Notification))]

namespace StickyNotes.Droid.DeviceHelpers
{
    [BroadcastReceiver]
    public class ButtonReceiver : BroadcastReceiver, IMessager
    {
        public override void OnReceive(Context context, Intent intent)
        {
            int notificationId = intent.GetIntExtra("notificationId", 0);
            NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            manager.Cancel(notificationId);
            var todos = JsonConvert.DeserializeObject<List<Todo>>((string)Xamarin.Forms.Application.Current.Properties["Todos"]);
            todos.Remove(todos.First(t => t.Id == notificationId));
            Xamarin.Forms.Application.Current.Properties["Todos"] = JsonConvert.SerializeObject(todos);
            //cada vez que establezcamos un valor lo hacemos persistir por si acaso
            Task.Run(() => Xamarin.Forms.Application.Current.SavePropertiesAsync());

            MessagingCenter.Send<IMessager, List<Todo>>(this, "todos", todos);
        }
    }

    public class Notification : INotification
    {
        public void CreateNotification(int id, string titulo)
        {
            var buttonReceiver = new ButtonReceiver();

            Intent buttonIntent = new Intent(Android.App.Application.Context, typeof(ButtonReceiver));
            buttonIntent.PutExtra("notificationId", id);
            PendingIntent btPendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, id, buttonIntent, PendingIntentFlags.OneShot);

            var builder = new NotificationCompat.Builder(Android.App.Application.Context, "location_notification")
                .SetContentTitle(titulo)
                .SetSmallIcon(Resource.Drawable.ic_today_white_24dp)
                .SetAutoCancel(true)
                .AddAction(new NotificationCompat.Action(Resource.Drawable.abc_btn_check_material, "DONETE :)", btPendingIntent));

            Android.App.Notification notification = builder.Build();
            notification.Flags |= NotificationFlags.NoClear;
            NotificationManager notificationManager = Android.App.Application.Context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            notificationManager.Notify(id, notification);
        }

        public void RemoveNotification(int id)
        {
            NotificationManager notificationManager = Android.App.Application.Context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            notificationManager.Cancel(id);
        }
    }
}
