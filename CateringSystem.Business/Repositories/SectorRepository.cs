using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class SectorRepository : Repository<Sector>, ISectorRepository
    {
        public SectorRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Sector> GetSectorList(int count)
        {
            return DbContext.Sector.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SaveSector(Sector model)
        {
            //model.CreatedDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            DbContext.Sector.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
