using Note.Helpers;
using Note.Resources.Language;
using Note.Services;
using Note.ViewModels;
using Note.ViewModels.Dialogs;
using Note.Views;
using Note.Views.Dialogs;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]

[assembly: ExportFont("Mulish-Black.ttf", Alias = "Mulish_Black")]
[assembly: ExportFont("Mulish-Bold.ttf", Alias = "Mulish_Bold")]
[assembly: ExportFont("Mulish-ExtraBold.ttf", Alias = "Mulish_ExtraBold")]
[assembly: ExportFont("Mulish-ExtraLight.ttf", Alias = "Mulish_ExtraLight")]
[assembly: ExportFont("Mulish-Light.ttf", Alias = "Mulish_Light")]
[assembly: ExportFont("Mulish-Medium.ttf", Alias = "Mulish_Medium")]
[assembly: ExportFont("Mulish-Regular.ttf", Alias = "Mulish_Regular")]
[assembly: ExportFont("Mulish-SemiBold.ttf", Alias = "Mulish_SemiBold")]

namespace Note
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer){ }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            LocalizationResourceManager.Current.PropertyChanged += Current_PropertyChanged;
            LocalizationResourceManager.Current.Init(AppResource.ResourceManager);

            ThemeHelper.SetTheme();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterRegionServices();

            containerRegistry.Register<IRepositoryService, RepositoryService>();

            containerRegistry.RegisterForNavigation<NavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>("MainView");
            containerRegistry.RegisterForNavigation<AddNoteView, AddNoteViewModel>("AddNoteView");
            containerRegistry.RegisterForNavigation<EditNoteView, EditNoteViewModel>("EditNoteView");

            containerRegistry.RegisterDialog<SettingListDialog, SettingListDialogViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void Current_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AppResource.Culture = LocalizationResourceManager.Current.CurrentCulture;
        }
    }
}
