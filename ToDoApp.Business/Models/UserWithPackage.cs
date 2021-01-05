using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class UserWithPackage
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string DayShiftWithPackageId { get; set; }
        public string PeriodTypeId { get; set; }
        public string UserCrediantialId { get; set; }
        public int DayShiftId { get; set; }
        public decimal Amount { get; set; }
        public int PackageCount { get; set; }
        public DateTime PackageStartDate { get; set; }
        public DateTime? PackageEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
