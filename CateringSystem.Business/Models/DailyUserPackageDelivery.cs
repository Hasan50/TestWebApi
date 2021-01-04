using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class DailyUserPackageDelivery
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string UserCrediantialId { get; set; }
        public string UserWithPackageId { get; set; }
        public decimal PackagePrice { get; set; }
        public int? PackageTergetCount { get; set; }
        public int? PackageDeliveryCount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal DueAmmount { get; set; }
        public decimal PaidAmmount { get; set; }
        public bool IsSpecialMenu { get; set; }
        public bool IsProblem { get; set; }
        public bool IsCompleate { get; set; }
        public string ProblemDetail { get; set; }
        public DateTime? DeliveryDateTime { get; set; }
        public DateTime? DeliveryAssignDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
