using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class MasterSetting
    {
        public string Id { get; set; }
        public int WeekOffCanBeSelectForDays { get; set; }
        public string DailyPackageUpdateLimitTime { get; set; }
    }
}
