using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Servicing.Account;
using Servicing.Clients;
using Servicing.Data;
using Servicing.Requests;
using Servicing.Roles;

namespace Site.PresentationLogic.Container
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<DbContext>().ImplementedBy<AnykeyDbCntext>());
            container.Register(Component.For<IRoleService>().ImplementedBy<RoleService>());
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>().LifestyleTransient());
            container.Register(Component.For<IRequestService>().ImplementedBy<RequestService>());
            container.Register(Component.For<IClientService>().ImplementedBy<ClientService>());
        }
    }
}