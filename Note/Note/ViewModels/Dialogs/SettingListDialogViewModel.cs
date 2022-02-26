using Note.Bases;
using Note.Helpers;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Note.Helpers.Constants;
using Note.Models;
using Xamarin.CommunityToolkit.UI.Views;

namespace Note.ViewModels.Dialogs
{
    public class SettingListDialogViewModel :
        BaseViewModel,
        IDialogAware
    {
        private DialogParameters param = new DialogParameters();
        public ObservableCollection<LanguageModel> LanguageList { get; set; } = new ObservableCollection<LanguageModel>();    
        public LanguageModel SelectedLanguageList { get; set; }

        public ICommand ChangeSelectLanguageListCommand { get; set; }
        public ICommand OnSwitchThemeCommand { get; set; }
        public ICommand CloseDialogCommand { get; set; }

        public event Action<IDialogParameters> RequestClose;

        public SettingListDialogViewModel(INavigationService navigationService) : base(navigationService)
        {
            OnSwitchThemeCommand = new Command(ThemeHelper.SwitchTheme);

            CloseDialogCommand = new Command(() => 
            {
                try
                {
                    param = new DialogParameters()
                    {
                        { "selectedList", SelectedLanguageList.Value }
                    };

                    RequestClose(param);
                }
                catch { }
            });
        }

        private void OnChangeSelectLanguageList(bool open = true)
        {
            if (!open)
                LanguageHelper.SwitchLanguage(SelectedLanguageList.Value);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            LanguageList = LanguageConstant;

            SelectedLanguageList = LanguageList
                .Where(x => x.Value == parameters.GetValue<string>("lang"))
                .FirstOrDefault();

            ChangeSelectLanguageListCommand = new Command(() => OnChangeSelectLanguageList(false));
        }
    }
}
