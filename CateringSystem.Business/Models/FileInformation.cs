using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class FileInformation
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileNo { get; set; }
        public string BarcodeNo { get; set; }
        public string Description { get; set; }
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

        public int? FromRoomId { get; set; }
        public int? ToRoomId { get; set; }
        [ScriptIgnore]
        public DateTime? RoomInDateTime { get; set; }
        [ScriptIgnore]
        public DateTime? RoomOutDateTime { get; set; }
        public bool? IsMoving { get; set; }

        public string FileStatus
        {
            get { return IsMoving.HasValue && IsMoving.Value ? "Moving" : "Idle"; }
        }

        public string RoomInDate
        {
            get { return RoomInDateTime.HasValue ? RoomInDateTime.Value.ToZoneTimeBD().ToString(Constants.DateLongFormat) : string.Empty; }
        }
        public string RoomOutDate
        {
            get { return RoomOutDateTime.HasValue ? RoomOutDateTime.Value.ToZoneTimeBD().ToString(Constants.DateLongFormat) : string.Empty; }
        }

        public string RoomInTime
        {
            get { return RoomInDateTime.HasValue ? RoomInDateTime.Value.ToZoneTimeBD().ToString(Constants.TimeFormat) : string.Empty; }
        }
        public string RoomOutTime
        {
            get { return RoomOutDateTime.HasValue ? RoomOutDateTime.Value.ToZoneTimeBD().ToString(Constants.TimeFormat) : string.Empty; }
        }

        public string FromRoomName { get; set; }
        public string ToRoomName { get; set; }

        public int StayInCurrentRoom { get; }
        public int StayInPreviousRoom { get; }
    }
}
