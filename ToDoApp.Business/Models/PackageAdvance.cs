using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class PackageAdvance
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public decimal Amount { get; set; }
        public string PeriodTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
