namespace StickyNotes.DeviceHelper
{
    public interface INotification
    {
        void CreateNotification(int id, string titulo);
        void RemoveNotification(int id);
    }
}
