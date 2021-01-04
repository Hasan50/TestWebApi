using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class DayShiftWithPackage
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public int DayShiftId { get; set; }
    }
}
