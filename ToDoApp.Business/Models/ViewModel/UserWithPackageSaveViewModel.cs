using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class UserWithPackageSaveViewModel
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public int? DayShiftId { get; set; }
        public decimal? Amount { get; set; }
        public int? PackageCount { get; set; }
        public string PeriodTypeId { get; set; }
        public string UserCrediantialId { get; set; }
        public DateTime? PackageStartDate { get; set; }
        public object PackageStartDateView { get; set; }
        public bool DayShiftActive { get; set; }

    }


}
