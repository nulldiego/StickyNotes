using Autofac;
using Rene.Xam.Extensions.Bootstrapping;
using StickyNotes.AppRegisterModules;
using StickyNotes.ViewModels;
using StickyNotes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StickyNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (DesignMode.IsDesignModeEnabled)
            {
                MainPage = new NavigationPage(new MainPage());
                return;
            }

            this.Setup()
                .RegisterDependencies(builder =>
                {
                    builder.RegisterType<CommonViewModelViewRegistrationModule>();
                    builder.RegisterModule<ServicesRegistrationModule>();

                    builder.RegisterModule<ViewModelViewRegistrationModule>();


                })
                .RegisterViews(reg =>
                {
                    // Registar vistas custom (no convención)


                })
                .Configure(conf =>
                {
                    conf
                       //.UseMasterDetailMode<MenuViewModel, ItemsViewModel>()
                       .SetViewLocatorConvention((s) => s?.Replace(".ViewModels.", ".Views.").Replace("ViewModel", "Page"))
                       .SetStartupView<MainViewModel>()
                       ;
                    //  conf.SetStartupView<StartPageViewModel>();
                })
                .Build();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
