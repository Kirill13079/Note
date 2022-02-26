using Note.Models;
using System.Collections.ObjectModel;

namespace Note.Services
{
    public interface IRepositoryService
    {
        NoteModel AddNote(string title, string content);
        void EditNote(NoteModel item);
        void RemoveNote(int id);
        ObservableCollection<NoteModel> GetNotes();
        string Reduction(string text);
    }
}
