using Note.Models;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Note.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly SQLiteConnection _database;
        private readonly string path = Path.Combine(System.Environment
            .GetFolderPath(System.Environment.SpecialFolder.Personal),
            "DataBase.db");

        public RepositoryService()
        {
            _database = new SQLiteConnection(path);
            _database.CreateTable<Core.Note>();
        }

        public NoteModel AddNote(string title, string content)
        {
            var note = new Core.Note
            {
                Title = title,
                Content = content,
                Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            };

            _database.Insert(note);

            return new NoteModel
            {
                Id = (int)SQLite3.LastInsertRowid(_database.Handle),
                Title = title,
                Content = content,
                SubTitle = Reduction(title),
                SubContent = Reduction(content),
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(note.Timestamp)
                .LocalDateTime
                .ToString("dd.MM.yyyy")
            };
        }

        public void EditNote(NoteModel item)
        {
            if (item != null)
            {
                var note = _database
                    .Query<Core.Note>($"select * from notes where id = {item.Id}")
                    .FirstOrDefault();

                if (note != null)
                {
                    note.Title = item.Title;
                    note.Content = item.Content;

                    _database.RunInTransaction(() => _database.Update(note));
                }
            }
        }

        public void RemoveNote(int id) 
        {
            _database.Delete<Core.Note>(id);
        }

        public ObservableCollection<NoteModel> GetNotes()
        {
            var notes = new ObservableCollection<NoteModel>();

            foreach (var item in _database
                .Query<Core.Note>("select * from notes"))
            {
                try
                {
                    var note = new NoteModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Content = item.Content,
                        SubTitle = Reduction(item.Title),
                        SubContent = Reduction(item.Content),
                        Timestamp = DateTimeOffset.FromUnixTimeSeconds(item.Timestamp)
                        .LocalDateTime
                        .ToString("dd.MM.yyyy")
                    };

                    notes.Add(note);
                }
                catch { }
            }

            return notes;
        }

        public string Reduction(string text)
        {
            return text?
                .Length >= 23
                ? $"{text?.Trim().Replace("\n", " ").Substring(0, 17)}..."
                : text;
        }
    }
}
