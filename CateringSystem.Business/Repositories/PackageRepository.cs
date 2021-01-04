using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using CateringSystem.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        public PackageRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Package> GetPackageList(int count)
        {
            return DbContext.Package.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public IEnumerable<UserWithPackage> GetUserWithPackageList(string userId)
        {
            return DbContext.UserWithPackage.Where(r => r.UserCrediantialId == userId).ToList();
        }
        public IEnumerable<object> GetUserWithPackageDetailList(string userId)
        {
            return (from u in DbContext.UserWithPackage
                    join d in DbContext.DayShift on u.DayShiftId equals d.Id
                    join pt in DbContext.PeriodType on u.PeriodTypeId equals pt.Id
                    select new
                    {
                        u.Id,
                        u.PackageId,
                        u.DayShiftId,
                        u.Amount,
                        u.PackageCount,
                        u.PeriodTypeId,
                        u.UserCrediantialId,
                        u.PackageStartDate,
                        u.PackageEndDate,
                        DayShiftName= d.Name,
                        PeriodTypeName=pt.Name
                    }).ToList();
        }
        public IEnumerable<UserPackageViewModel> GetUserPackageDetailList(string userId)
        {
            var commandText = @"SELECT p.Id,p.Name Package,p.PackageCode,p.Price PackagePrice,u.PackageStartDate,u.PackageEndDate,u.PackageId UserPackageId,ud.Id DailyUserPackageDeliveryId,ud.UserCrediantialId,ud.DeliveryAssignDate,ud.PackageTergetCount,ud.PaidAmmount,ud.DueAmmount from package p
                            left join (select PackageId,PackageStartDate,PackageEndDate from UserWithPackage
                            where UserCrediantialId=@userId)u on p.Id= u.PackageId
							    left join (select Id,PackageId,UserCrediantialId,DeliveryAssignDate,PackageTergetCount,PaidAmmount,DueAmmount from DailyUserPackageDelivery
                            where UserCrediantialId=@userId)ud on p.Id= ud.PackageId";
            var name = new SqlParameter("@userId", userId);
            var data = DbContext.Database.SqlQuery<UserPackageViewModel>(commandText, name).ToList();
            return data;
        }
        public IEnumerable<PackageAdvanceViewModel> GetPackageAdvanceList(int count)
        {
            try
            {
                var commandText = @"select a.Id,a.PackageId,a.Amount,a.PeriodTypeId,b.Name PackageName,b.PackageCode,c.Name PeriodType,c.Count PeriodCount from PackageAdvance a
                                    left join Package b on a.PackageId=b.Id
                                    left join PeriodType c on a.PeriodTypeId=c.Id
                                    order by a.CreatedDate";
                var data = DbContext.Database.SqlQuery<PackageAdvanceViewModel>(commandText).ToList();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<PackageAdvance> GetPackageWithAdvanceList(string packageId)
        {
            return DbContext.PackageAdvance.Where(r => r.PackageId.ToString() == packageId).OrderByDescending(c => c.CreatedDate).ToList();
        }
        public List<UserDuePackageViewModel> GetDuePackageList(string userId)
        {
            var commandText = @"SELECT b.Id DailyUserPackageDeliveryId, b.PackageId,ISNULL(b.PackageDeliveryCount,0) PackageQuantity,p.Name,p.PackageCode,p.Price from  DailyUserPackageDelivery b
                                left join Package p on b.PackageId=p.Id
                                where b.UserCrediantialId=@userId and b.PaidAmmount=0";
            var name = new SqlParameter("@userId", userId);
            var data = DbContext.Database.SqlQuery<UserDuePackageViewModel>(commandText, name).ToList();
            return data;
        }
        public IEnumerable<object> GetPackageCboList()
        {
            var data = DbContext.Package.Select(r => new { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return data;
        }
        public IEnumerable<object> GetPackageTestList()
        {
            var data = DbContext.Package.Select(r => new { Value = r.Id.ToString(), Text = r.Name, r.Price }).ToList();
            return data;
        }
        public ResponseModel DeletePackage(Package model)
        {
            try
            {
                var local = DbContext.Set<Package>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Package can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel SavePackage(Package model)
        {
            try
            {
                if (model.Id == null)
                {
                    model.CreatedDate = DateTime.Now;
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.Package.Add(model);
                }
                else
                {
                    var local = DbContext.Set<Package>().Local.FirstOrDefault(c => c.Id == model.Id);
                    if (local != null)
                    {
                        DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                    }
                    model.UpdatedDate = DateTime.Now;
                    DbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Failed" };
            }
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public ResponseModel DeletePackageAdvance(PackageAdvance model)
        {
            try
            {
                var local = DbContext.Set<PackageAdvance>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Package advance can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel SavePackageAdvance(PackageAdvance model)
        {
            bool IsFindMatch = false;
            var existingList = GetPackageAdvanceList(100);
            PackageAdvanceViewModel matchPackage = new PackageAdvanceViewModel();
            var findMatch = existingList.Where(r => r.PackageId == model.PackageId && r.PeriodTypeId == model.PeriodTypeId && r.Id !=model.Id).FirstOrDefault();
            if (findMatch != null)
            {
                matchPackage = findMatch;
                IsFindMatch = true;
            }
            if (!IsFindMatch)
            {
                try
                {
                    if (model.Id == null)
                    {
                        model.CreatedDate = DateTime.Now;
                        model.Id = Guid.NewGuid().ToString();
                        DbContext.PackageAdvance.Add(model);
                    }
                    else
                    {

                        var local = DbContext.Set<PackageAdvance>().Local.FirstOrDefault(c => c.Id == model.Id);
                        if (local != null)
                        {
                            DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                        }
                        var dbData = DbContext.PackageAdvance.Where(r => r.Id == model.Id).FirstOrDefault();
                        dbData.PackageId = model.PackageId;
                        dbData.Amount = model.Amount;
                        dbData.UpdatedDate = DateTime.Now;
                        DbContext.Entry(dbData).State = System.Data.Entity.EntityState.Modified;
                    }
                    DbContext.SaveChanges();
                    return new ResponseModel { Success = true, Message = "Success" };
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return new ResponseModel { Success = false, Message = matchPackage.PackageName+" and period type "+ matchPackage.PeriodType + " already saved." };
        }
        public ResponseModel SaveAutoExecPackageDeliveryTerget()
        {
            var flag = false;
            try
            {
                var uPackageList = (from up in DbContext.UserWithPackage
                                    join uc in DbContext.UserCredentials on up.UserCrediantialId equals uc.Id
                                    join w in DbContext.UserWeekDayOff on uc.Id equals w.UserId
                                    where uc.IsActive
                                    select new PackageDeliveryTergetAutoExcViewModel { Id = up.Id, UserCrediantialId = up.UserCrediantialId, PackageId = up.PackageId, PackageCount = up.PackageCount, WeekDayName = w.WeekDayName }).ToList();
                List<DailyUserPackageDelivery> s = new List<DailyUserPackageDelivery>();
                var currentDate = DateTime.Now;
                foreach (var item in uPackageList)
                {
                    if (item.WeekDayName != currentDate.DayOfWeek.ToString())
                    {
                        var d = new DailyUserPackageDelivery
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserWithPackageId = item.Id,
                            UserCrediantialId = item.UserCrediantialId,
                            PackageId = item.PackageId,
                            PackageTergetCount = item.PackageCount,
                            ExtraPrice = 0,
                            DeliveryAssignDate = DateTime.Now,
                            IsSpecialMenu = false,
                            CreatedDate = DateTime.Now,
                        };
                        s.Add(d);
                    }
                }
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.DailyUserPackageDelivery.AddRange(s);
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel UpdatePackageDeliveryTerget(PackageDeliveryTergetViewModel model)
        {
            try
            {

                if (model.DailyUserPackageDeliveryId != null)
                {
                    var package = DbContext.DailyUserPackageDelivery.Where(r => r.Id == model.DailyUserPackageDeliveryId && r.PackageId == model.Id).FirstOrDefault();
                    package.PackageTergetCount = model.PackageCount;
                    package.UpdatedDate = DateTime.Now;
                    DbContext.Entry(package).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    var packageOb = DbContext.Package.Where(r => r.Id == model.Id).FirstOrDefault();
                    var package = new DailyUserPackageDelivery
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserCrediantialId = model.UserCrediantialId,
                        PackageId = model.Id,
                        PackageTergetCount = model.PackageCount,
                        DueAmmount=(int)model.PackageCount* packageOb.Price,
                        DeliveryAssignDate = DateTime.Now,
                        CreatedDate=DateTime.Now
                    };
                    DbContext.DailyUserPackageDelivery.Add(package);
                }
                DbContext.SaveChanges();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public ResponseModel UpdatePackageDelivery(PackageDeliveryTergetViewModel model)
        {
            var flag = false;
            try
            {

                var package = DbContext.DailyUserPackageDelivery.Where(r => r.Id == model.DailyUserPackageDeliveryId && r.PackageId == model.Id).FirstOrDefault();
                package.PackageDeliveryCount = model.PackageCount;
                package.UpdatedDate = DateTime.Now;
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.Entry(package).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel SavePackageAssign(List<DailyUserPackageDeliveryViewModel> models)
        {
            var flag = false;
            try
            {
                DbContext.Database.BeginTransaction();
                flag = true;
                foreach (var item in models)
                {
                    var ss = DbContext.UserWithPackage.Where(r => r.UserCrediantialId == item.UserCrediantialId && r.PackageId == item.PackageId).ToList();//.FirstOrDefault(r => r.PackageId == item.PackageId && r.UserCrediantialId == item.UserCrediantialId).Id;
                    DailyUserPackageDelivery ob = new DailyUserPackageDelivery
                    {
                        Id= Guid.NewGuid().ToString(),
                        UserWithPackageId= ss.Count > 0 ? ss.First().Id.ToString() : null,
                        DeliveryAssignDate=Convert.ToDateTime(item.DeliveryAssignDate),
                        PackageTergetCount=item.PackageTergetCount,
                        UserCrediantialId=item.UserCrediantialId,
                        DueAmmount = Convert.ToDecimal(DbContext.Package.First(r => r.Id == item.PackageId).Price * item.PackageTergetCount),
                        PaidAmmount = 0,
                        PackageId = item.PackageId,
                        CreatedDate= DateTime.Now,
                    };
                    DbContext.DailyUserPackageDelivery.Add(ob);
                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public List<PackageTergetTotalCountViewModel> GetPackageWithProductionCountList(DateTime date)
        {
            var conDate = DateTime.Now.ToString("yyyy/MM/dd");
            var commandText = @"SELECT p.Id,p.Name,sum(pg.ProductionQuantity) TotalTergetCount from Package p
                                left join (select ProductionQuantity, PackageId from PackageWithProductMaster where CONVERT(VARCHAR(10), CreatedDate, 111) = @date) pg on p.Id=pg.PackageId
                                group by p.Id,p.Name";
            var name = new SqlParameter("@date", conDate);
            var data = DbContext.Database.SqlQuery<PackageTergetTotalCountViewModel>(commandText, name).ToList();
            return data;
        }
        public List<PackageTergetTotalCountViewModel> GetPackageWithTergetCountList(DateTime date)
        {
            var conDate = DateTime.Now.ToString("yyyy/MM/dd");
            var commandText = @"SELECT p.Id,p.Name,pd.TotalTergetCount from Package p
                                left join (select Sum(PackageTergetCount) TotalTergetCount, PackageId from DailyUserPackageDelivery where CONVERT(VARCHAR(10), DeliveryAssignDate, 111) = @date group by PackageId ) pd on p.Id=pd.PackageId";
            var name = new SqlParameter("@date", conDate);
            var data = DbContext.Database.SqlQuery<PackageTergetTotalCountViewModel>(commandText, name).ToList();
            return data;
        }
        public List<PackageTergetViewModel> GetPackageTergetList(string customerId)
        {
            var commandText = @"select p.Id,up.Id DailyUserPackageDeliveryId,p.PackageCode,up.PackageTergetCount PackageCount from Package p
                                left join DailyUserPackageDelivery up on p.Id=up.PackageId and up.UserCrediantialId=@customerId and CONVERT(VARCHAR(10), up.DeliveryAssignDate, 111) = CONVERT(VARCHAR(10), getdate(), 111)
                                order by p.PackageCode";
            var name = new SqlParameter("@customerId", customerId);
            var data = DbContext.Database.SqlQuery<PackageTergetViewModel>(commandText, name).ToList();
            return data;
        }
        public List<PackageTergetViewModel> GetPackageDeliveryList(string customerId)
        {
            var commandText = @"select p.Id,up.Id DailyUserPackageDeliveryId,p.PackageCode,up.PackageDeliveryCount PackageCount from Package p
                                left join DailyUserPackageDelivery up on p.Id=up.PackageId and up.UserCrediantialId=@customerId and CONVERT(VARCHAR(10), up.DeliveryAssignDate, 111) = CONVERT(VARCHAR(10), getdate(), 111)
                                order by p.PackageCode";
            var name = new SqlParameter("@customerId", customerId);
            var data = DbContext.Database.SqlQuery<PackageTergetViewModel>(commandText, name).ToList();
            return data;
        }
        public List<DailyUserPackageDelivery> GetDailyUserPackageDeliveriesWithUserId(string userId,DateTime date)
        {
            return DbContext.DailyUserPackageDelivery.Where(r => r.UserCrediantialId == userId && DbFunctions.TruncateTime(r.DeliveryAssignDate) == DbFunctions.TruncateTime(date)).ToList();
        }
        public List<UserDueAndPaidViewModel> GetUserDueAndPaidList(string userId)
        {
            var commandText = @"SELECT sum(DueAmmount) TotalDue,sum(PaidAmmount) TotalPaid
                              FROM DailyUserPackageDelivery
                              where UserCrediantialId=@userId";
            var name = new SqlParameter("@userId", userId);
            var data = DbContext.Database.SqlQuery<UserDueAndPaidViewModel>(commandText, name).ToList();
            return data;
        }
        public List<UserDueAndPaidViewModel> GetUserDueAndPaidWithDateList(string userId,DateTime date)
        {
            var conDate = date.ToString("yyyy/MM/dd");
            var commandText = @"SELECT sum(DueAmmount) TotalDue,sum(PaidAmmount) TotalPaid
                              FROM DailyUserPackageDelivery
                              where UserCrediantialId=@userId and CONVERT(VARCHAR(10),DeliveryAssignDate , 111) = @date";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamName = "@userId", ParamValue =userId},
                    new QueryParamObj { ParamName = "@date", ParamValue =date},
                };
            var data = DbContext.Database.SqlQuery<UserDueAndPaidViewModel>(commandText, queryParamList).ToList();
            return data;
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    
    }
}
