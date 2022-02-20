using Note.Base;
using Note.Models;
using Note.Services;
using Note.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Note.ViewModels
{
    public class MainViewModels : 
        BaseViewModel,
        IInitialize
    {
        private readonly IRepository _repository;

        public LayoutState NotesListState { get; set; }
        public ObservableCollection<NoteModel> NotesList { get; set; } = new ObservableCollection<NoteModel>();
        public ICommand MoreCommand { get; set; }
        public ICommand CheckTaskCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        public ICommand ItemDragged { get; }
        public ICommand ItemDraggedOver { get; }
        public ICommand ItemDragLeave { get; }
        public ICommand ItemDropped { get; }

        public MainViewModels(INavigationService navigationService, IRepository repository) 
            : base(navigationService)
        {
            _repository = repository;

            CheckTaskCommand = new Command<NoteModel>(CheckTaskCommandHandler);
            MoreCommand = new Command(() => _navigationService.NavigateAsync(nameof(More)));
            DeleteNoteCommand = new Command<NoteModel>((NoteModel item) => NotesList.Remove(item));
            ItemDragged = new Command<NoteModel>(OnItemDragged);
            ItemDraggedOver = new Command<NoteModel>(OnItemDraggedOver);
            ItemDragLeave = new Command<NoteModel>(OnItemDragLeave);
            ItemDropped = new Command<NoteModel>(i => OnItemDropped(i));
        }

        public void Initialize(INavigationParameters parameters)
        {
            NotesListState = LayoutState.Loading;

            NotesList.Add(new NoteModel { Id = 1, Title = "Test1", Content = "abadasd" });
            NotesList.Add(new NoteModel { Id = 2, Title = "Test2", Content = "abadasd" });
            NotesList.Add(new NoteModel { Id = 3, Title = "Test3", Content = "abadasd" });
            NotesList.Add(new NoteModel { Id = 4, Title = "Test4", Content = "abadasd" });

            if (NotesList.Count == 0)
                NotesListState = LayoutState.Empty;

            NotesListState = LayoutState.None;
        }


        private void OnItemDragged(NoteModel item)
        {
            NotesList.ForEach(i => i.isBeingDragged = item == i);
        }

        private void OnItemDraggedOver(NoteModel item)
        {
            var itemBeingDragged = NotesList.FirstOrDefault(i => i.isBeingDragged);
            NotesList.ForEach(i => i.isBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        private void OnItemDragLeave(NoteModel item)
        {
            NotesList.ForEach(i => i.isBeingDraggedOver = false);
        }

        private void CheckTaskCommandHandler(NoteModel item)
        {
            item.Archived = !item.Archived;
        }

        private Task OnItemDropped(NoteModel item)
        {
            var itemToMove = NotesList.First(i => i.isBeingDragged);
            var itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return Task.CompletedTask;

            var insertAtIndex = NotesList.IndexOf(itemToInsertBefore);
            NotesList.Remove(itemToMove);
            NotesList.Insert(insertAtIndex, itemToMove);
            itemToMove.isBeingDragged = false;
            itemToInsertBefore.isBeingDraggedOver = false;
            return Task.CompletedTask;
        }
    }
}
