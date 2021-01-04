using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class CustomerInvoiceViewModel
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string InvoiceNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromPhoneNumber { get; set; }
        public string FromAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CratedById { get; set; }
    }
}
