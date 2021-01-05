using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class UserPackageViewModel
    {
        public string Id { get; set; }
        public string UserPackageId { get; set; }
        public string UserCrediantialId { get; set; }
        public string DailyUserPackageDeliveryId { get; set; }
        public string Package { get; set; }
        public string PackageCode { get; set; }
        public decimal? PackagePrice { get; set; }
        public decimal? DueAmmount { get; set; }
        public decimal? PaidAmmount { get; set; }
        public int TotalTergetCount { get; set; }
        public int? PackageCount { get; set; }
        public int? PackageTergetCount { get; set; }
        public DateTime? DeliveryAssignDate { get; set; }
        public DateTime? PackageStartDate { get; set; }
        public DateTime? PackageEndDate { get; set; }
    }


}
