using Autofac;
using StickyNotes.Settings;
using Module = Autofac.Module;

namespace StickyNotes.AppRegisterModules
{
    public class ServicesRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserSettings>().As<IUserSettings>();

            base.Load(builder);
        }

    }
}
