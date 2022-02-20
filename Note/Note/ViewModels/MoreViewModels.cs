using Note.Base;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Note.ViewModels
{
    public class MoreViewModels : BaseViewModel
    {
        public ICommand BackCommand { get; set; }

        public MoreViewModels(INavigationService navigationService) 
            : base(navigationService)
        {
            BackCommand = new Command(BackCommandH);
        }
    }
}
