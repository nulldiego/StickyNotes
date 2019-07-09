using StickyNotes.Model.Base;

namespace StickyNotes.Model
{
    public class Todo : ModelBase<int>
    {
        public string Title { get; set; }
    }
}
