using CateringSystem.Business.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CateringSystem.Business
{
    public class CateringDbContext:DbContext
    {

        public CateringDbContext() : base("name=FtCon")
        {
           Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public static CateringDbContext Create()
        {
            return new CateringDbContext();
        }
        public virtual DbSet<UserCredentials> UserCredentials { get; set; }
        public virtual DbSet<UserCredientialDetail> UserCredientialDetail { get; set; }
        public virtual DbSet<RawItem> RawItem { get; set; }
        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<Thana> Thana { get; set; }
        public virtual DbSet<PostOffice> PostOffice { get; set; }
        public virtual DbSet<PeriodType> PeriodType { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<Condition> Condition { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<PackageRawItem> PackageRawItem { get; set; }
        public virtual DbSet<PackageAdvance> PackageAdvance { get; set; }
        public virtual DbSet<PackageCondition> PackageCondition { get; set; }
        public virtual DbSet<UserWithPackage> UserWithPackage { get; set; }
        public virtual DbSet<DailyUserPackageDelivery> DailyUserPackageDelivery { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<UserWeekDayOff> UserWeekDayOff { get; set; }
        public virtual DbSet<MasterSetting> MasterSetting { get; set; }
        public virtual DbSet<DayShift> DayShift { get; set; }
        public virtual DbSet<UserDuePaidAmmount> UserDuePaidAmmount { get; set; }
        public virtual DbSet<DesignationModel> Designation { get; set; }
        public virtual DbSet<DeliveryManCustomerTag> DeliveryManCustomerTag { get; set; }
        public virtual DbSet<DayShiftWithPackage> DayShiftWithPackage { get; set; }
        public virtual DbSet<PackageProduct> PackageProduct { get; set; }
        public virtual DbSet<PackageWithProductMaster> PackageWithProductMaster { get; set; }
        public virtual DbSet<PackageWithProductMasterDetail> PackageWithProductMasterDetail { get; set; }
        public virtual DbSet<UserActiveStatus> UserActiveStatus { get; set; }
        public virtual DbSet<CustomerInvoice> CustomerInvoice { get; set; }
        public virtual DbSet<CustomerInvoiceDetail> CustomerInvoiceDetail { get; set; }
        public virtual DbSet<CustomerPayment> CustomerPayment { get; set; }
        public virtual DbSet<DeliveryManPackageQuantity> DeliveryManPackageQuantity { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new CourseConfiguration());
        //}
    }
}
