using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class PackageDeliveryTergetAutoExcViewModel
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string UserCrediantialId { get; set; }
        public int? PackageCount { get; set; }
        public string WeekDayName { get; set; }
    }
}
