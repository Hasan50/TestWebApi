using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class DesignationRepository : Repository<DesignationModel>, IDesignationRepository
    {
        public DesignationRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<DesignationModel> GetDesignationList()
        {
            return DbContext.Designation.OrderByDescending(c => c.Name).ToList();
        }
        public ResponseModel SaveDesignation(DesignationModel model)
        {
            //model.CreatedDate = DateTime.Now;
            DbContext.Designation.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
