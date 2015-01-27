#region Using statemetns
using System.Data.Entity;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using TOS.BusinessLogic.Managers;
using TOS.Contracts.Managers;
using TOS.Contracts.Repository;
using TOS.Core;
using TOS.Data.EF;
using TOS.Data.EF.Repository;

#endregion

namespace Solution.IoC.Castle.Installers
{
    public class AdminInstaller : IWindsorInstaller
    {
        private const string WebAssembletyName = "solution.Web";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed(WebAssembletyName)
                .BasedOn<IController>()
                .LifestyleTransient()
                .Configure(x => x.Named(x.Implementation.FullName)));

            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<WindsorControllerFactory>(),
                Component.For<IRepository<BaseEntity>>().ImplementedBy<Repository<BaseEntity>>());

            container.Register(Component.For<DbContext>().ImplementedBy<GameContext>().LifestyleSingleton());
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)).LifestyleTransient());
            container.Register(Component.For<IPlayerManager>().ImplementedBy<PlayerManager>().LifestylePerWebRequest());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}