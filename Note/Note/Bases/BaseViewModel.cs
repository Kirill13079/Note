using Prism.AppModel;
using Prism.Navigation;
using System.ComponentModel;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.UI.Views;

namespace Note.Bases
{
    public class BaseViewModel :
        INavigationAware,
        IPageLifecycleAware,
        INotifyPropertyChanged,
        IDestructible
    {

        protected INavigationService _navigationService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public LayoutState MainState { get; set; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected async void BackCommand()
        {
            await _navigationService.GoBackAsync(null);
        }

        protected async void BackCommand(INavigationParameters parameters)
        {
            await _navigationService.GoBackAsync(parameters);
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        public virtual void Destroy() { }
    }
}
