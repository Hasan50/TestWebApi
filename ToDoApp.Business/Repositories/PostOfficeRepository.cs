using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    class PostOfficeRepository : Repository<PostOffice>, IPostOfficeRepository
    {
        public PostOfficeRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<PostOffice> GetPostOfficeList(int count)
        {
            return DbContext.PostOffice.OrderByDescending(c => c.Name).Take(count).ToList();
        }
        public ResponseModel SavePostOffice(PostOffice model)
        {
            //model.CreatedDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            DbContext.PostOffice.Add(model);
            DbContext.SaveChanges();
            return new ResponseModel { Success = true, Message = "Success" };
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
