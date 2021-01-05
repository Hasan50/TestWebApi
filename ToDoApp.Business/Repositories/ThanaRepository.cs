using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    class ThanaRepository : Repository<Thana>, IThanaRepository
    {
        public ThanaRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Thana> GetThanaList(int count)
        {
            return DbContext.Thana.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SaveThana(Thana model)
        {
            //model.CreatedDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            DbContext.Thana.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
