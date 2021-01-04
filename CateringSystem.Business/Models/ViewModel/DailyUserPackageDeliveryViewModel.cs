using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem.Business.Models
{
    public class DailyUserPackageDeliveryViewModel
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string UserCrediantialId { get; set; }
        public string UserWithPackageId { get; set; }
        public decimal PackagePrice { get; set; }
        public int? PackageTergetCount { get; set; }
        public int? PackageDeliveryCount { get; set; }
        public decimal ExtraPrice { get; set; }
        public bool IsSpecialMenu { get; set; }
        public bool IsProblem { get; set; }
        public bool IsCompleate { get; set; }
        public string ProblemDetail { get; set; }
        public string DeliveryDateTime { get; set; }
        public string DeliveryAssignDate { get; set; }

    }


}
