using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class MasterSettingRepository : Repository<MasterSettingRepository>, IMasterSettingRepository
    {
        public MasterSettingRepository(CateringDbContext context)
            : base(context)
        {
        }
        public MasterSetting GetMasterSetting()
        {
            return DbContext.MasterSetting.FirstOrDefault();
        }
        public ResponseModel SaveMasterSetting(MasterSetting model)
        {
            //model.CreatedDate = DateTime.Now;
            if (model.Id != Guid.NewGuid().ToString())
            {
                DbContext.Entry(model).State = EntityState.Modified;
                DbContext.SaveChanges();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            else
            {
                model.Id = Guid.NewGuid().ToString();
                DbContext.MasterSetting.Add(model);
                DbContext.SaveChanges();
                return new ResponseModel { Success = true, Message = "Success" };
            }
          
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
