using Note.Bases;
using Note.Extensions;
using Note.Helpers;
using Note.Models;
using Note.Services;
using Note.Views;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Note.ViewModels
{
    public class MainViewModel : 
        BaseViewModel,
        IInitialize
    {
        private readonly IRepositoryService _repository;
        private IDialogService _dialogService;
        private string _language;
        private int _tappedCount = 0;

        public LayoutState NotesListState { get; set; }
        public ObservableCollection<NoteModel> NotesList { get; set; } = new ObservableCollection<NoteModel>();
        public bool isEnableButton { get; set; } = true;

        public ICommand OnSwitchLanguageCommand { get; }

        public ICommand OnDeleteNoteCommand { get; }
        public ICommand OnEditNoteCommand { get; }
        public ICommand OnCreateNoteCommand { get; }

        public ICommand OnItemDragged { get; }
        public ICommand OnItemDraggedOver { get; }
        public ICommand OnItemDragLeave { get; }
        public ICommand OnItemDropped { get; }

        public MainViewModel(INavigationService navigationService, IDialogService dialogService, IRepositoryService repository) 
            : base(navigationService)
        {
            _repository = repository;
            _dialogService = dialogService;

            _language = LanguageHelper.GetCurrentLanguage();

            OnSwitchLanguageCommand = new Command(async() => await OpenSettingListDialog());

            OnDeleteNoteCommand = new Command<NoteModel>(DeleteNote);
            OnEditNoteCommand = new Command<NoteModel>(async(note) => await EditNote(note));
            OnCreateNoteCommand = new Command(async () => await CreateNote());

            OnItemDragged = new Command<NoteModel>(ItemDragged);
            OnItemDraggedOver = new Command<NoteModel>(ItemDraggedOver);
            OnItemDragLeave = new Command<NoteModel>(ItemDragLeave);
            OnItemDropped = new Command<NoteModel>(note => ItemDropped(note));
        }

        public void Initialize(INavigationParameters parameters)
        {
            GetNotesList();
        }

        private void GetNotesList()
        {
            NotesListState = LayoutState.Loading;
            NotesList = _repository.GetNotes();

            ChekLayoutState();
        }

        private async Task OpenSettingListDialog()
        {
            _tappedCount++;

            if (_tappedCount == 1)
            {
                var param = new DialogParameters()
                {
                    { "lang", _language }
                };

                _language = await _dialogService.ShowDialogAsync(_language);

                Device.BeginInvokeOnMainThread(() => _tappedCount = 0);
            }
        }

        private void ChekLayoutState()
        {
            NotesListState = !NotesList.Any() 
                ? LayoutState.Empty
                : LayoutState.None;
        }

        private void DeleteNote(NoteModel note)
        {
            _repository.RemoveNote(note.Id);
            NotesList.Remove(note);

            ChekLayoutState();
        }

        private async Task CreateNote()
        {
            isEnableButton = false;

            await _navigationService.NavigateAsync(nameof(AddNoteView), null,
                useModalNavigation: true,
                animated: true);

            isEnableButton = true;
        }

        private async Task EditNote(NoteModel note)
        {
            var parametr = new NavigationParameters() { { "note", note } };

            await _navigationService.NavigateAsync(nameof(EditNoteView), parametr,
                useModalNavigation: true,
                animated: true);
        }

        private void ItemDragged(NoteModel note)
        {
            NotesList.ForEach(i => i.isDragged = note == i);
        }

        private void ItemDraggedOver(NoteModel note)
        {
            var itemDragged = NotesList.FirstOrDefault(p => p.isDragged);
            NotesList.ForEach(i => i.isDraggedOver = note == i && note != itemDragged);
        }

        private void ItemDragLeave(NoteModel note)
        {
            NotesList.ForEach(i => i.isDraggedOver = false);
        }

        private Task ItemDropped(NoteModel note)
        {
            var itemToMove = NotesList
                .First(i => i.isDragged);

            var itemToInsertBefore = note;

            if (itemToMove == null
                || itemToInsertBefore == null
                || itemToMove == itemToInsertBefore)
                return Task.CompletedTask;

            var insertAtIndex = NotesList
                .IndexOf(itemToInsertBefore);

            NotesList.Remove(itemToMove);
            NotesList.Insert(insertAtIndex, itemToMove);

            itemToMove.isDragged = false;
            itemToInsertBefore.isDraggedOver = false;

            return Task.CompletedTask;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var note = parameters.GetValue<NoteModel>("note");

            if (note != null)
                NotesList.Add(note);

            ChekLayoutState();
        }
    }
}