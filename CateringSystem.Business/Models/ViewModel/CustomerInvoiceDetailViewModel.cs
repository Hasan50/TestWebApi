using CateringSystem.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class CustomerInvoiceDetailViewModel
    {
        public int Id { get; set; }
        public string DailyUserPackageDeliveryId { get; set; }
        public string CustomerInvoiceId { get; set; }
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public int DeliveryQty { get; set; }
        public decimal PackagePrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
