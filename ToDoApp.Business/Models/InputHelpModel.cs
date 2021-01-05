using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class InputHelpModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InputHelpTypeId { get; set; }

        [ScriptIgnore]
        public DateTime? CreatedAt { get; set; }
        public string CreatedById { get; set; }
        [ScriptIgnore]
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedById { get; set; }

        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }

        public string CreatedDateTime
        {
            get { return CreatedAt.HasValue ? CreatedAt.Value.ToZoneTimeBD().ToString(Constants.DateTimeLongFormat) : string.Empty; }
        }


        public string UpdatedDateTime
        {
            get { return UpdatedAt.HasValue ? UpdatedAt.Value.ToZoneTimeBD().ToString(Constants.DateTimeLongFormat) : string.Empty; }
        }
    }
}
