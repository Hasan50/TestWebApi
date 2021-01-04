using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CateringSystem.Business.DataAccess
{
    class CustomerInvoiceRepository : Repository<CustomerInvoice>, ICustomerInvoiceRepository
    {
        public CustomerInvoiceRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<CustomerInvoiceViewModel> GetInvoiceList(string userId)
        {
            var commandText = @"SELECT a.Id,a.CustomerId,a.InvoiceNo,a.Title,a.TotalAmount,a.InvoiceDate,
                            a.Description,a.CreatedDate,a.CratedById,a.FromName,a.FromEmail,a.FromPhoneNumber,
                                a.FromAddress,CustomerName=b.FullName,CustomerEmail=b.Email,CustomerPhoneNumber=b.PhoneNumber,CustomerAddress=c.Address,b.FileId,b.FileName from  CustomerInvoice a
                                left join UserCredentials b on a.CustomerId=b.Id
                                left join UserCredientialDetail c on a.CustomerId=c.UserCredentialId
                                where a.CustomerId=@userId  order by a.CreatedDate";
            var name = new SqlParameter("@userId", userId);
            var data = DbContext.Database.SqlQuery<CustomerInvoiceViewModel>(commandText, name).ToList();
            return data;
        }
        public IEnumerable<CustomerInvoiceDetailViewModel> GetInvoiceDetail(string invoiceId)
        {
            return (from a in DbContext.CustomerInvoiceDetail
                    join b in DbContext.Package on a.PackageId equals b.Id
                    where a.CustomerInvoiceId ==invoiceId
                    select new CustomerInvoiceDetailViewModel
                    {
                        Id = a.Id,
                        DailyUserPackageDeliveryId = a.DailyUserPackageDeliveryId,
                        CustomerInvoiceId = a.CustomerInvoiceId,
                        PackageId = a.PackageId,
                        PackageName = b.Name,
                        DeliveryQty = a.DeliveryQty,
                        PackagePrice = a.PackagePrice,
                        CreatedDate = a.CreatedDate
                    }
                    ).ToList();
        }
        public ResponseModel SaveInvoice(CustomerInvoice model, List<CustomerInvoiceDetail> invoiceDetails)
        {
            var flag = false;
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.CreatedDate = DateTime.Now;
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.CustomerInvoice.Add(model);
                foreach (var item in invoiceDetails)
                {
                    item.CustomerInvoiceId = model.Id;
                    item.CreatedDate = DateTime.Now;
                    DbContext.CustomerInvoiceDetail.Add(item);
                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Detached;
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }

            return new ResponseModel { Success = true, Message = "Success" };
        }
        public ResponseModel UpdateInvoice(CustomerInvoice model, List<CustomerInvoiceDetail> invoiceDetails)
        {
            var flag = false;
            try
            {

                DbContext.Database.BeginTransaction();
                flag = true;
                var local = DbContext.Set<CustomerInvoice>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                foreach (var item in invoiceDetails)
                {
                    if (item.Id == 0)
                    {
                        item.CustomerInvoiceId = model.Id;
                        item.CreatedDate = DateTime.Now;
                        DbContext.CustomerInvoiceDetail.Add(item);
                    }
                    else
                    {
                        var itemlocal = DbContext.Set<CustomerInvoiceDetail>().Local.FirstOrDefault(c => c.Id == item.Id);
                        if (itemlocal != null)
                        {
                            DbContext.Entry(itemlocal).State = System.Data.Entity.EntityState.Detached;
                        }
                        DbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }

            return new ResponseModel { Success = true, Message = "Success" };
        }
        public ResponseModel DeleteInvoice(CustomerInvoiceViewModel model)
        {
            try
            {

                var commandText = @"DELETE FROM CustomerInvoiceDetail where CustomerInvoiceId=@Id
                                    DELETE FROM CustomerInvoice where Id=@Id
                                    ";
                var name = new SqlParameter("@Id", model.Id);
                var data = DbContext.Database.SqlQuery<CustomerInvoiceViewModel>(commandText, name).ToList();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Package can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel DeleteInvoiceDetail(CustomerInvoiceDetail model)
        {
            try
            {

                var commandText = @"DELETE FROM CustomerInvoiceDetail where Id=@Id ";
                List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(new SqlParameter("@Id", model.Id));
                SqlParameter[] parameters = parameterList.ToArray();
                int result = DbContext.Database.ExecuteSqlCommand(commandText, parameters);
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "can not be delete. Package is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }

}
