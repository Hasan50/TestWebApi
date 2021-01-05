using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class PackageDeliveryTergetViewModel
    {
        public string Id { get; set; }
        public string DailyUserPackageDeliveryId { get; set; }
        public string UserCrediantialId { get; set; }
        public int? PackageCount { get; set; }
      
    }


}
