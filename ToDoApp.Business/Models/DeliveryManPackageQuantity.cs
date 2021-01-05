using ToDoApp.Framework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class DeliveryManPackageQuantity
    {
        public string Id { get; set; }
        public string DeliveryManId { get; set; }
        public string PackageId { get; set; }
        public int DeliveryTergetCount { get; set; }
        public DateTime DeliveryTergetDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
