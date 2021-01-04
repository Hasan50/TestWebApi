using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class UserDuePackageViewModel
    {
        public string PackageId { get; set; }
       public string  DailyUserPackageDeliveryId { get; set; }
        public int PackageQuantity { get; set; }
        public string Name { get;set;}
        public string PackageCode { get; set; }
        public decimal Price { get; set; }
    }
}
