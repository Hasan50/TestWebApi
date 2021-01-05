using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    class RawItemRepository : Repository<RawItem>, IRawItemRepository
    {
        public RawItemRepository(CateringDbContext context)
            : base(context)
        {
        }
      
        public List<RawItemViewModel> GetRawItemList(int count)
        {
            var commandText = @"SELECT a.Id,a.Name,a.Quantity,a.Weight,a.Price,a.QuantityUomId,a.WeightUomId,a.ImagePath,a.CreatedDate,a.UpdatedDate,b.Name QuantityUom,c.Name WeightUom FROM RawItem a
                                left join UnitOfMeasurement b on a.QuantityUomId = b.Id
                                left join UnitOfMeasurement c on a.WeightUomId = c.Id
                                order by a.Name ";
            var data = DbContext.Database.SqlQuery<RawItemViewModel>(commandText).ToList();
            return data;
        }
        public ResponseModel DeleteRawItem(RawItem model)
        {
            try
            {
                var local = DbContext.Set<RawItem>().Local.FirstOrDefault(c => c.Id == model.Id);
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
        public ResponseModel SaveRawItem(RawItem model)
        {
            try
            {
                if (model.Id == null)
                {
                    model.Id = Guid.NewGuid().ToString();
                    model.CreatedDate = DateTime.Now;
                    DbContext.RawItem.Add(model);
                }
                else
                {
                    var local = DbContext.Set<RawItem>().Local.FirstOrDefault(c => c.Id == model.Id);
                    if (local != null)
                    {
                        DbContext.Entry(local).State = EntityState.Detached;
                    }
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
        public IEnumerable<object> GetPackageRawItemList(int count, string packageid)
        {
            return (from a in DbContext.PackageRawItem
                    join b in DbContext.RawItem on a.RawItemId equals b.Id
                    join c in DbContext.UnitOfMeasurement on b.QuantityUomId equals c.Id into ca from x in ca.DefaultIfEmpty()
                    join d in DbContext.UnitOfMeasurement on b.WeightUomId equals d.Id into da from y in da.DefaultIfEmpty()
                    where a.PackageId.ToString() == packageid
                    orderby a.CreatedDate
                    select new
                    {
                        a.Id,
                        a.PackageId,
                        a.WeekDay,
                        b.Name,
                        b.Quantity,
                        b.QuantityUomId,
                        QuantityUoM= x.Name,
                        b.WeightUomId,
                        WeightUoM= x.Name,
                        b.Weight,
                        b.Price
                    }).Take(10).ToList();
        }
        public ResponseModel SavePackageRawItem(List<PackageRawItem> models)
        {
            var singleModel = new PackageRawItem();
            foreach (var item in models)
            {
                var existingList = DbContext.PackageRawItem.Where(r => r.RawItemId == item.RawItemId && r.PackageId == item.PackageId).ToList();
                if (existingList.Count > 0)
                {
                    singleModel = existingList.FirstOrDefault();
                    break;
                }
            }
            if (singleModel.Id == null)
            {
                foreach (PackageRawItem model in models)
                {
                    model.CreatedDate = DateTime.Now;
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.PackageRawItem.Add(model);
                    DbContext.SaveChanges();
                }
                return new ResponseModel { Success = true, Message = "Success" };
            }
            return new ResponseModel { Success = false, Message = DbContext.Package.Where(r => r.Id == singleModel.PackageId).FirstOrDefault().Name + " Has already added on list." };
        }
        public ResponseModel DeletePackageRawItem(PackageRawItem model)
        {
            try
            {
                var local = DbContext.Set<PackageRawItem>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = EntityState.Detached;
                }
                DbContext.Entry(model).State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Shift can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
