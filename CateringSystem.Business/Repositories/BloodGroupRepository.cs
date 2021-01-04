using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class BloodGroupRepository : Repository<BloodGroup>, IBloodGroupRepository
    {
        public BloodGroupRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<BloodGroup> GetBloodGroupList(int count)
        {
            return DbContext.BloodGroup.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SaveBloodGroup(BloodGroup model)
        {
            model.CreatedDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            DbContext.BloodGroup.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
