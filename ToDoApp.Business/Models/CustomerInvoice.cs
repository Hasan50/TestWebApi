using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class CustomerInvoice
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromPhoneNumber { get; set; }
        public string FromAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CratedById { get; set; }
    }
}
