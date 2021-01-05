using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class DayShiftWithPackage
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public int DayShiftId { get; set; }
    }
}
