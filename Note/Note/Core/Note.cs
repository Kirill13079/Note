using SQLite;

namespace Note.Core
{
    [SQLite.Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        [SQLite.Column("id")]
        public int Id { get; set; }

        [SQLite.Column("title")]
        public string Title { get; set; }

        [SQLite.Column("contnet")]
        public string Content { get; set; }

        [SQLite.Column("timestamp")]
        public long Timestamp { get; set; }
    }
}
