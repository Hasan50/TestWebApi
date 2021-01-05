using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class PackageTergetViewModel
    {
        public string Id { get; set; }
        public string DailyUserPackageDeliveryId { get; set; }
        public string PackageCode { get; set; }
        public int? PackageCount { get; set; }
      
    }


}
