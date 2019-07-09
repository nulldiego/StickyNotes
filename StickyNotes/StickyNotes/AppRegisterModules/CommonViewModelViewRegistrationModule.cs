using System.Reflection;
using Autofac;
using Rene.Xam.Extensions.Base;
using Rene.Xam.Extensions.Bootstrapping.ViewContracts;

namespace StickyNotes.AppRegisterModules
{
    public class CommonViewModelViewRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var asm = Assembly.GetExecutingAssembly();
            builder
                .RegisterAssemblyTypes(asm)
                .Where(t => typeof(ViewModelBase).IsAssignableFrom(t))
                .AssignableTo<IViewModelBase>();

        }
    }
}
