using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class DeliveryManCustomerTag
    {
        public string Id { get; set; }
        public string DeliveryManId { get; set; }
        public string CustomerId { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
