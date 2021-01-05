using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    class PackageProductRepository : Repository<PackageProduct>, IPackageProductRepository
    {
        public PackageProductRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<PackageProduct> GetPackageProductList(int count)
        {
            return DbContext.PackageProduct.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SavePackageProduct(PackageProduct model)
        {
            try
            {
                if (model.Id == null)
                {
                    model.Id = Guid.NewGuid().ToString();
                    model.CreatedDate = DateTime.Now;
                    DbContext.PackageProduct.Add(model);
                }
                else
                {
                    var local = DbContext.Set<PackageProduct>().Local.FirstOrDefault(c => c.Id == model.Id);
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
        public ResponseModel DeletePackageProduct(PackageProduct model)
        {
                var existingData = DbContext.PackageProduct.Where(r => r.Id == model.Id).FirstOrDefault();
            try
            {
                var local = DbContext.Set<PackageProduct>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(existingData).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContext.Entry(existingData).State = System.Data.Entity.EntityState.Detached;
                return new ResponseModel { Success = false, Message = "Can not be delete. Using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public IEnumerable<object> GetPackageWithProductList(int count, string packageid,DateTime date)
        {
            return (from a in DbContext.PackageWithProductMaster
                    join c in DbContext.PackageWithProductMasterDetail on a.Id equals c.PackageWithProductMasterId
                    join b in DbContext.PackageProduct on c.PackageProductId equals b.Id
                    where a.PackageId.ToString() == packageid && DbFunctions.TruncateTime(a.CreatedDate) == DbFunctions.TruncateTime(date)
                    orderby a.CreatedDate
                    select new
                    {
                        a.Id,
                        a.PackageId,
                        a.ProductionQuantity,
                        c.Quantity,
                        c.Weight,
                        c.UoM,
                        b.Name,
                    }).Take(10).ToList();
        }
        public ResponseModel SavePackageWithProduct(List<PackageWithProductMasterDetail> models,PackageWithProductMaster model)
        {
            var flag = false;
            try
            {
                DbContext.Database.BeginTransaction();
                flag = true;
                if (model.Id ==null)
                {
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.PackageWithProductMaster.Add(model);
                }
                else
                {
                    DbContext.Entry(model).State = EntityState.Modified;
                }

                foreach (PackageWithProductMasterDetail ob in models)
                {
                    ob.Id = Guid.NewGuid().ToString();
                    ob.CreatedDate = DateTime.Now;
                    ob.PackageWithProductMasterId = model.Id;
                    DbContext.PackageWithProductMasterDetail.Add(ob);
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
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
