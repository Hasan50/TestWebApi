using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class UserDueAndPaidViewModel
    {
        public decimal TotalDue { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
