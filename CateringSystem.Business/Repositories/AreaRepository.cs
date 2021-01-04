using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Area> GetAreaList(int count)
        {
            return DbContext.Area.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SaveArea(Area model)
        {
            //model.CreatedDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            DbContext.Area.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
