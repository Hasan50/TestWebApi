using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class FileMovementModel
    {
        public string Id { get; set; }
        public int FileId { get; set; }
        public int RoomId { get; set; }
        public bool? MoveIn { get; set; }
        public bool? MoveOut { get; set; }
        [ScriptIgnore]
        public DateTime? ActionAt { get; set; }
        public string ActionById { get; set; }


        public string ActionByName { get; set; }
        public string FileName { get; set; }
        public string FileNo { get; set; }
        public string RoomNo { get; set; }

        public string MovementStatus
        {
            get
            {
                return MoveIn.HasValue && MoveIn.Value ? "Move In" : "Move Out";
            }
        }

        public string MovementDate
        {
            get { return ActionAt.HasValue ? ActionAt.Value.ToZoneTimeBD().ToString(Constants.DateLongFormat) : string.Empty; }
        }

        public string RoomInTime
        {
            get { return ActionAt.HasValue ? ActionAt.Value.ToZoneTimeBD().ToString(Constants.TimeFormat) : string.Empty; }
        }
    }
}
