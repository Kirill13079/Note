using Note.Bases;
using Note.Interfaces;
using Note.Services;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Note.ViewModels
{
    public class AddNoteViewModel :
        BaseViewModel,
        IInitialize
    {
        private readonly IRepositoryService _repository;

        public string TitleNote { get; set; }
        public string ContentNote { get; set; }

        public bool Test { get; set; } = false;

        public ICommand OnBackCommand { get; }
        public ICommand OnSaveCommand { get; }

        public AddNoteViewModel(INavigationService navigationService, IRepositoryService repository) 
            : base(navigationService)
        {
            _repository = repository;

            OnBackCommand = new Command(BackCommand);
            OnSaveCommand = new Command(SaveNote);
        }

        public void Initialize(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private void SaveNote()
        {
            if (!string.IsNullOrEmpty(TitleNote) || !string.IsNullOrEmpty(ContentNote))
            {
                var note = _repository.AddNote(TitleNote, ContentNote);
                var parametr = new NavigationParameters() { { "note", note } };
                BackCommand(parametr);
            }
            else
            {
                BackCommand();
            }

            DependencyService.Get<IKeyboard>().HideKeyboard();
        }
    }
}
