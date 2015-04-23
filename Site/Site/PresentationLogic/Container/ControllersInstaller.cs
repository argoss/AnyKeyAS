using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Site.PresentationLogic.Container
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                       .Pick().If(t => t.Name.EndsWith("Controller"))
                                       .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                                       .LifestyleTransient());
        }
    }
}