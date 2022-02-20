using Note.Base;

namespace Note.Models
{
    public class DraggableItemModel : BaseModel
    {
        public bool isBeingDragged { get; set; }
        public bool isBeingDraggedOver { get; set; }
    }

    public class NoteModel : DraggableItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Archived { get; set; }
        public long Timestamp { get; set; }
    }
}
