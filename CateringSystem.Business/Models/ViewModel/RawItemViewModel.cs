using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class RawItemViewModel
    {
        public string Id { get; set; }
        public int? WeightUomId { get; set; }
        public int? QuantityUomId { get; set; }
        public string WeightUom { get; set; }
        public string QuantityUom { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
