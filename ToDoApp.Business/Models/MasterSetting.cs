using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class MasterSetting
    {
        public string Id { get; set; }
        public int WeekOffCanBeSelectForDays { get; set; }
        public string DailyPackageUpdateLimitTime { get; set; }
    }
}
