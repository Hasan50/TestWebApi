using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class UserWeekDayOff
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string WeekDayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
