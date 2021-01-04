using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class PeriodTypeRepository : Repository<PeriodType>, IPeriodTypeRepository
    {
        public PeriodTypeRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<PeriodType> GetPeriodTypeList(int count)
        {
            return DbContext.PeriodType.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public IEnumerable<object> GetPeriodTypeCboList()
        {
            var data = DbContext.PeriodType.Select(r => new { Value = r.Id.ToString(), Text = r.Count+" "+ r.Name }).ToList();
            return data;
        }

        public ResponseModel Save(PeriodType model)
        {
            try
            {
                if (model.Id == null)
                {
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.PeriodType.Add(model);
                    DbContext.SaveChanges();
                }
                else
                {
                    var local = DbContext.Set<PeriodType>().Local.FirstOrDefault(c => c.Id == model.Id);
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
        public ResponseModel DeletePeriodType(PeriodType model)
        {
            try
            {
                var local = DbContext.Set<PeriodType>().Local.FirstOrDefault(c => c.Id == model.Id);
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
