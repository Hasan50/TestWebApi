using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class PackageWithProductMaster
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public int ProductionQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
    }
}
