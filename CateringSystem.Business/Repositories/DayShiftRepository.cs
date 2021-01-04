using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class DayShiftRepository : Repository<DayShift>, IDayShiftRepository
    {
        public DayShiftRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<object> GetDayShiftCboList()
        {
            var data = DbContext.DayShift.Select(r => new { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return data;
        }
        public IEnumerable<DayShift> GetDayShiftList()
        {
            var data = DbContext.DayShift.ToList();
            return data;
        }
        public ResponseModel DeleteShift(DayShift model)
        {
            try
            {
                var local = DbContext.Set<DayShift>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Shift can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel Save(DayShift model)
        {
            try
            {
                if (model.Id == 0)
                {
                    DbContext.DayShift.Add(model);
                }
                else
                {
                    var local = DbContext.Set<DayShift>().Local.FirstOrDefault(c => c.Id == model.Id);
                    if (local != null)
                    {
                        DbContext.Entry(local).State = EntityState.Detached;
                    }
                    //DbContext.Set<DayShift>().AddOrUpdate(model);
                    DbContext.Entry(model).State = EntityState.Modified;
                }
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Failed" };
            }
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public ResponseModel SaveDayShiftPackage(List<DayShiftWithPackage> models)
        {
            bool IsFindMatch = false;
            var existingList = GetDayShiftPackageList(100,models.FirstOrDefault().DayShiftId);
            DayShiftPackageViewModel matchPackage = new DayShiftPackageViewModel();
            foreach (var item in models)
            {
                var findMatch = existingList.Where(r => r.DayShiftId == item.DayShiftId && r.PackageId == item.PackageId).FirstOrDefault();
                if (findMatch !=null)
                {
                    matchPackage = findMatch;
                    IsFindMatch = true;
                }
            }
            if (!IsFindMatch)
            {
                foreach (DayShiftWithPackage model in models)
                {
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.DayShiftWithPackage.Add(model);
                    DbContext.SaveChanges();
                }
                return new ResponseModel { Success = true, Message = "Success" };
            }
            return new ResponseModel { Success = false, Message = matchPackage.PackageName+" Has already added on list." };
        }
        public ResponseModel DeleteDayShiftPackage(DayShiftWithPackage model)
        {
            try
            {
                var local = DbContext.Set<DayShiftWithPackage>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Shift can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public IEnumerable<DayShiftPackageViewModel> GetDayShiftPackageList(int count, int dayShiftId)
        {
            return (from a in DbContext.DayShiftWithPackage
                    join b in DbContext.DayShift on a.DayShiftId equals b.Id
                    join p in DbContext.Package on a.PackageId equals p.Id
                    where a.DayShiftId == dayShiftId
                    select new DayShiftPackageViewModel
                    {
                        Id=a.Id,
                       PackageId= a.PackageId,
                        DayShiftId= a.DayShiftId,
                        Name= b.Name,
                        PackageName= p.Name,
                        PackageCode= p.PackageCode,
                        Price= p.Price
                    }).Take(10).ToList();
        }
        public IEnumerable<object> GetDayShiftPackageListWithUserId(int count, int dayShiftId,string userId)
        {
            return (from a in DbContext.DayShiftWithPackage
                    join b in DbContext.DayShift on a.DayShiftId equals b.Id
                    join p in DbContext.Package on a.PackageId equals p.Id
                    where a.DayShiftId == dayShiftId
                    select new
                    {
                        a.Id,
                        a.PackageId,
                        a.DayShiftId,
                        b.Name,
                        PackageName = p.Name,
                        p.PackageCode,
                        p.Price,
                    }).Take(10).ToList();
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
