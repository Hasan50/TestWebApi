using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
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
