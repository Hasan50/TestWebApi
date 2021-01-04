using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class PackageWithProductMasterDetail
    {
        public string Id { get; set; }
        public string PackageWithProductMasterId { get; set; }
        public string PackageProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public string UoM { get; set; }
        public int? WeightUomId { get; set; }
        public int? QuantityUomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
    }
}
