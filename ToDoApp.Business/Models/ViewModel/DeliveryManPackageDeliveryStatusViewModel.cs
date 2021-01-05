using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class DeliveryManPackageDeliveryStatusViewModel
    {
        public string DeliveryManId { get; set; }
        public int? PackageDeliveryCount { get; set; }
        public int? PackageTergetCount { get; set; }
        public string PackageName { get; set; }
        public string CustomerName { get; set; }
    }
}
