#region Using statemetns

using System.Data.Entity;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;
using Pharmacy.Data;
using Pharmacy.Web.Core.ModelBuilders;

#endregion

namespace Pharmacy.IoC.CastleWindsor.Installers
{
    public class AdminInstaller : IWindsorInstaller
    {
        private const string WebAssembletyName = "Pharmacy.Web";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed(WebAssembletyName)
                .BasedOn<IController>()
                .LifestyleTransient()
                .Configure(x => x.Named(x.Implementation.FullName)));

            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<WindsorControllerFactory>());

            container.Register(Component.For<DbContext>()
                .ImplementedBy<DataContext>()
                .LifestyleSingleton());

            container.Register(Component.For(typeof(IRepository<>))
                .ImplementedBy(typeof(Repository<>))
                .LifestyleTransient());

            #region Validators

            container.Register(Component.For(typeof(IValidator<Core.Pharmacy>))
                .ImplementedBy<PharmacyValidator>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Medicament>))
                .ImplementedBy<MedicamentValidator>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<MedicamentPriceHistory>))
                .ImplementedBy<MedicamentPriceHistoryValidator>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Order>))
                .ImplementedBy<OrderValidator>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<OrderDetails>))
                .ImplementedBy<OrderDetailsValidator>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IValidator<Storage>))
                .ImplementedBy<StorageValidator>()
                .LifestylePerWebRequest());

            #endregion

            #region Managers

            container.Register(Component.For(typeof(IEntityManager<>))
                .ImplementedBy(typeof(EntityManager<>))
                .LifestylePerWebRequest());

            container.Register(Component.For(typeof(IMedicamentManager))
                .ImplementedBy(typeof(MedicamentManager))
                .LifestylePerWebRequest());

            #endregion

            #region Builders

            container.Register(Component.For(typeof(OrderViewModelBuilder))
                .ImplementedBy(typeof(OrderViewModelBuilder))
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof (OrderDetailsViewModelBuilder))
                .ImplementedBy(typeof (OrderDetailsViewModelBuilder))
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof (StorageViewModelBuilder))
                .ImplementedBy(typeof (StorageViewModelBuilder))
                .LifestylePerWebRequest());

            #endregion

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}