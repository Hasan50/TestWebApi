using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class PackageAdvanceViewModel
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageCode { get; set; }
        public string PeriodType { get; set; }
        public int? PeriodCount { get; set; }
        public decimal Amount { get; set; }
        public string PeriodTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
