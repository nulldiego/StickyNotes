﻿using Autofac;
using Rene.Xam.Extensions.Bootstrapping.BootstrapperInterfaces;
using Rene.Xam.Extensions.Bootstrapping.Interfaces;
using Rene.Xam.Extensions.Bootstrapping.Services;
using Xamarin.Forms;
using DependencyService = Rene.Xam.Extensions.Bootstrapping.Services.DependencyService;

namespace Rene.Xam.Extensions.Bootstrapping.Modules
{
    public class DependencyRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<MessagingService>().As<IMessagingService>().SingleInstance();
            builder.Register<INavigation>(context =>
            {
                var mp = Application.Current.MainPage;

                if (mp.GetType() == typeof(MasterDetailPage))
                {
                    return ((MasterDetailPage)mp).Detail.Navigation;
                }

                return Application.Current.MainPage.Navigation;

            }).SingleInstance();

            builder.RegisterType<DependencyService>().As<IDependencyService>().SingleInstance();
        }
    }
}
