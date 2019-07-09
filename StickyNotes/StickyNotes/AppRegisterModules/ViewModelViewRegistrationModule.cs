using System.Reflection;
using Autofac;
using Rene.Xam.Extensions.Base;
using Rene.Xam.Extensions.Bootstrapping.ViewContracts;

namespace StickyNotes.AppRegisterModules
{
    public class ViewModelViewRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var asm = Assembly.GetExecutingAssembly();
            builder
                .RegisterAssemblyTypes(asm)
                .Where(t => typeof(ViewModelBase).IsAssignableFrom(t))
                .AssignableTo<IViewModelBase>();

            var commonassembly = typeof(StickyNotes.Model.Base.ModelBase<>).Assembly;
            builder
                .RegisterAssemblyTypes(commonassembly)
                .Where(t => typeof(ViewModelBase).IsAssignableFrom(t))
                .AssignableTo<IViewModelBase>();

        }
    }
}
