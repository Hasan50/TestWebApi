using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface ICustomerPaymentRepository
    {
        IEnumerable<object> GetPaymentList(string userId);
        ResponseModel SavePayment(CustomerPayment model);
        ResponseModel DeleteCustomerPayment(CustomerPayment model);
    }
}
