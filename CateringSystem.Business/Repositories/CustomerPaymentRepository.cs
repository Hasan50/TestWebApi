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
    class CustomerPaymentRepository : Repository<CustomerPayment>, ICustomerPaymentRepository
    {
        public CustomerPaymentRepository(CateringDbContext context)
            : base(context)
        {
        }
        public IEnumerable<object> GetPaymentList(string userId)
        {
            return (from a in DbContext.CustomerPayment
                    join b in DbContext.CustomerInvoice on a.CustomerInvoiceId equals b.Id
                    join c in DbContext.UserCredentials on a.CustomerId equals c.Id
                    where a.CustomerId==userId
                    orderby a.CreatedDate select new {a.Id,a.CustomerInvoiceId,a.PaymentMode,a.PaidAmount,a.RemainingBalance,a.PaymentDate,a.TotalAmount,b.InvoiceNo,b.InvoiceDate, CustomerName=c.FullName }).Take(10).ToList(); 
        }
        public ResponseModel SavePayment(CustomerPayment model )
        {

            var flag = false;
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.CreatedDate = DateTime.Now;
                var packageDelivery = (from a in DbContext.CustomerInvoice
                                       join b in DbContext.CustomerInvoiceDetail on a.Id equals b.CustomerInvoiceId
                                       join c in DbContext.DailyUserPackageDelivery on b.DailyUserPackageDeliveryId equals c.Id
                                       where a.Id == model.CustomerInvoiceId && a.CustomerId == model.CustomerId
                                       select c
                                      ).FirstOrDefault();
                DbContext.CustomerPayment.Add(model);
                packageDelivery.PaidAmmount = model.TotalAmount;
                packageDelivery.UpdatedDate = DateTime.Now;
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.Entry(packageDelivery).State = EntityState.Modified;
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
        public ResponseModel DeleteCustomerPayment(CustomerPayment model)
        {
            try
            {


                var commandText = @"DELETE FROM CustomerPayment where Id=@Id ";
                List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(new SqlParameter("@Id", model.Id));
                SqlParameter[] parameters = parameterList.ToArray();
                int result = DbContext.Database.ExecuteSqlCommand(commandText, parameters);
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Payment can not be delete. Payment is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
