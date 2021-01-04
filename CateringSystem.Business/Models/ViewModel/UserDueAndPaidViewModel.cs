using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class UserDueAndPaidViewModel
    {
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
