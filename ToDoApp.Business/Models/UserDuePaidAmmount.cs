using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class UserDuePaidAmmount
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string DailyUserPackageDeliveryId {get;set;}
        public decimal DueAmmount { get; set; }
        public decimal PaidAmmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
