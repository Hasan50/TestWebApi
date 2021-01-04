using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class UnitOfMeasurementRepository : Repository<UnitOfMeasurement>, IUnitOfMeasurementRepository
    {
        public UnitOfMeasurementRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<UnitOfMeasurement> GetUnitOfMeasurementList(int count)
        {
            return DbContext.UnitOfMeasurement.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SaveUnitOfMeasurement(UnitOfMeasurement model)
        {
            try
            {
                if (model.Id == 0)
                {
                    DbContext.UnitOfMeasurement.Add(model);
                    DbContext.SaveChanges();
                }
                else
                {
                    var local = DbContext.Set<UnitOfMeasurement>().Local.FirstOrDefault(c => c.Id == model.Id);
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
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
