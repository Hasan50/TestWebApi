using ToDoApp.Business.DataAccess;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Repositories;
using Microsoft.Practices.Unity;

namespace ToDoApp.Business
{
    public class FtUnityMapper
    {
        private static IUnityContainer _container;

        public static void RegisterComponents(IUnityContainer container)
        {
            _container = container;

            container.RegisterType<IUserManager, UserDataAccess>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFileInformation, FileInformationDataAccess>(new ContainerControlledLifetimeManager());
            container.RegisterType<IInputHelp, InputHelpDataAccess>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRoomSetup, RoomSetupDataAccess>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPackageRepository, PackageRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRawItemRepository, RawItemRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBloodGroupRepository, BloodGroupRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICompanyRepository, CompanyRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAreaRepository, AreaRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISectorRepository, SectorRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IThanaRepository, ThanaRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPostOfficeRepository, PostOfficeRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserCredientialRepository, UserCredientialRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFileMovement, FileMovementDataAccess>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPeriodTypeRepository, PeriodTypeRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMasterSettingRepository, MasterSettingRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDayShiftRepository, DayShiftRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDesignationRepository, DesignationRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPackageProductRepository, PackageProductRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomerInvoiceRepository, CustomerInvoiceRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICustomerPaymentRepository, CustomerPaymentRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeliveryManPackageQuantityRepository, DeliveryManPackageQuantityRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>(new ContainerControlledLifetimeManager());

        }

        public static T GetInstance<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (ResolutionFailedException exception)
            {
            }
            return default(T);
        }
    }
}
