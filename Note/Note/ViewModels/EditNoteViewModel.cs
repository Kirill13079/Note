using Note.Bases;
using Note.Interfaces;
using Note.Models;
using Note.Services;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Note.ViewModels
{
    public class EditNoteViewModel :
        BaseViewModel,
        IInitialize
    {
        private readonly IRepositoryService _repository;
        private NoteModel note;

        public string TitleNote { get; set; }
        public string ContentNote { get; set; }

        public ICommand OnBackCommand { get; }
        public ICommand OnEditCommand { get; }

        public EditNoteViewModel(INavigationService navigationService, IRepositoryService repository)
            : base(navigationService)
        {
            _repository = repository;
            OnBackCommand = new Command(BackCommand);
            OnEditCommand = new Command(EditNote);
        }

        public void Initialize(INavigationParameters parameters)
        {
            note = parameters.GetValue<NoteModel>("note");
            TitleNote = note.Title;
            ContentNote = note.Content;
        }

        private void EditNote()
        {
            note.Title = TitleNote;
            note.Content = ContentNote;

            note.SubTitle = _repository.Reduction(TitleNote);
            note.SubContent = _repository.Reduction(ContentNote);

            _repository.EditNote(note);
            BackCommand();

            DependencyService.Get<IKeyboard>().HideKeyboard();
        }
    }
}
