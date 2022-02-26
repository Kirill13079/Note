using Note.Bases;

namespace Note.Models
{
    public class DraggableItemModel : BaseModel
    {
        public bool isDragged { get; set; }
        public bool isDraggedOver { get; set; }
    }

    public class NoteModel : DraggableItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string SubContent { get; set; }
        public bool Archived { get; set; }
        public string Timestamp { get; set; }
    }
}
