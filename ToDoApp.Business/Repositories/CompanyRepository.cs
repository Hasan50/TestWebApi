using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(CateringDbContext context)
            : base(context)
        {
        }
    
        public List<CompanyViewModel> GetCompanyList(int count)
        {
            var commandText = @"SELECT c.Id,c.UserCredentialId,c.Name,c.PhoneNo,c.ContactPersonOneName,c.ContactPersonOneNumber
                                ,c.ContactPersonOneDesignation,c.ContactPersonOneSection,c.ContactPersonTwoName,c.ContactPersonTwoNumber
                                ,c.ContactPersonTwoDesignation,c.ContactPersonOneSection,c.Address,c.IsActive
                                ,u.LoginID,u.FullName,u.Email,s.Status
                                from Company c
                                left join UserCredentials u on c.UserCredentialId=u.Id
                                left join UserActiveStatus s on u.Id=s.UserCredentialId
                                order by c.Name";
            var data = DbContext.Database.SqlQuery<CompanyViewModel>(commandText).ToList();
            return data;
        }
        public ResponseModel SaveCompany(Company model)
        {

            try
            {
                if (model.Id == null)
                {
                    model.CreatedDate = DateTime.Now;
                    model.Id = Guid.NewGuid().ToString();
                    DbContext.Company.Add(model);
                    DbContext.SaveChanges();
                }
                else
                {
                    var local = DbContext.Set<Company>().Local.FirstOrDefault(c => c.Id == model.Id);
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
        public ResponseModel DeleteCompany(Company model)
        {
            try
            {
                var local = DbContext.Set<Company>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = EntityState.Detached;
                }
                DbContext.Entry(model).State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Company can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
