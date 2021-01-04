using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface ICustomerInvoiceRepository
    {
        IEnumerable<CustomerInvoiceViewModel> GetInvoiceList(string userId);
        ResponseModel SaveInvoice(CustomerInvoice model, List<CustomerInvoiceDetail> invoiceDetails);
        ResponseModel UpdateInvoice(CustomerInvoice model, List<CustomerInvoiceDetail> invoiceDetails);
        ResponseModel DeleteInvoice(CustomerInvoiceViewModel model);
        IEnumerable<CustomerInvoiceDetailViewModel> GetInvoiceDetail(string invoiceId);
        ResponseModel DeleteInvoiceDetail(CustomerInvoiceDetail model);
    }
}
