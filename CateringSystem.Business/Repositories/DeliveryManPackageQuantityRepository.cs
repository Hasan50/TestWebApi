using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using CateringSystem.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class DeliveryManPackageQuantityRepository : Repository<DeliveryManPackageQuantity>, IDeliveryManPackageQuantityRepository
    {
        public DeliveryManPackageQuantityRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<object> GetDeliveryManPackageList(int count, string deliveryManId, DateTime date)
        {
            return (from u in DbContext.DeliveryManPackageQuantity
                    join d in DbContext.Package on u.PackageId equals d.Id
                    where u.DeliveryManId == deliveryManId && DbFunctions.TruncateTime(u.DeliveryTergetDate) == DbFunctions.TruncateTime(date)
                    select new
                    {
                        u.Id,
                        u.PackageId,
                        u.DeliveryManId,
                        u.DeliveryTergetCount,
                        u.DeliveryTergetDate,
                        PackageName = d.Name,
                        d.PackageCode
                    }).ToList();
        }
        public IEnumerable<object> GetExtraDeliveryStatusList(string deliveryManId, DateTime date)
        {
            return (from d in DbContext.DeliveryManPackageQuantity
                    join a in DbContext.Employee on d.DeliveryManId equals a.Id into am
                    from e in am.DefaultIfEmpty()
                    join b in DbContext.Package on d.PackageId equals b.Id into ba 
                    from p in ba.DefaultIfEmpty()
                    where e.LoginKey == deliveryManId && DbFunctions.TruncateTime(d.DeliveryTergetDate) == DbFunctions.TruncateTime(date)
                    select new
                    {
                        d.Id,
                        PackageName= p.Name,
                        d.DeliveryTergetCount,
                    }).ToList();
        }
        public ResponseModel SavePackageQuantity(List<DeliveryManPackageQuantity> model)
        {
            var singleModel =new DeliveryManPackageQuantity();
            foreach (var item in model)
            {
                var existingList = DbContext.DeliveryManPackageQuantity.Where(r => r.DeliveryManId == item.DeliveryManId && r.PackageId == item.PackageId && DbFunctions.TruncateTime(r.DeliveryTergetDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                if (existingList.Count > 0)
                {
                    singleModel = existingList.FirstOrDefault();
                    break;
                }
            }
            if (singleModel.Id==null)
            {
                foreach (var item in model)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.CreatedAt = DateTime.Now;
                    DbContext.DeliveryManPackageQuantity.Add(item);
                    DbContext.SaveChanges();
                }
                return new ResponseModel { Success = true, Message = "Success" };
            }
            return new ResponseModel { Success = false, Message =DbContext.Package.Where(r=> r.Id==singleModel.PackageId).FirstOrDefault().Name+ " Has already added on list." };
        }
        public ResponseModel Delete(DeliveryManPackageQuantity model)
        {
            try
            {
                var local = DbContext.Set<DeliveryManPackageQuantity>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Can not be delete. Is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public List<DeliveryManPackageDeliveryStatusViewModel> GetDeliveryStatusList(string deliveryManId, DateTime date)
        {
            var conDate = date.ToString("yyyy/MM/dd");
            var commandText = @"SELECT dm.DeliveryManId,u.PackageDeliveryCount,u.PackageTergetCount,p.Name PackageName,uc.FullName CustomerName from [DailyUserPackageDelivery] u
            left join Package p on u.PackageId = p.Id
            left join DeliveryManCustomerTag dm on u.UserCrediantialId=dm.CustomerId
            left join Employee e on dm.DeliveryManId=e.Id
            left join UserCredentials uc on dm.CustomerId=uc.Id
            where e.LoginKey='" + deliveryManId+@"' and CONVERT(VARCHAR(10), u.DeliveryAssignDate, 111) = '"+ conDate + @"' 
            and CONVERT(VARCHAR(10), dm.CreatedAt, 111) = '" + conDate + @"'";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamName = "@deliveryManId", ParamValue =deliveryManId},
                    new QueryParamObj { ParamName = "@adate", ParamValue =conDate},
                    new QueryParamObj { ParamName = "@bdate", ParamValue =conDate}
                };
            var data = DbContext.Database.SqlQuery<DeliveryManPackageDeliveryStatusViewModel>(commandText).ToList();
            return data;
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
