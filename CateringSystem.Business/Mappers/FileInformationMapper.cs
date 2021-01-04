using CateringSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace CateringSystem.Business.Mappers
{
    public class FileInformationMapper
    {

        public static List<FileInformation> ToFile(DbDataReader readers)
        {
            if (readers == null)
                return null;
            var models = new List<FileInformation>();

            while (readers.Read())
            {
                var model = new FileInformation
                {
                    Id =  Convert.ToInt32(readers["Id"]),
                    FileName = Convert.IsDBNull(readers["FileName"]) ? string.Empty : Convert.ToString(readers["FileName"]),
                    FileNo = Convert.IsDBNull(readers["FileNo"]) ? string.Empty : Convert.ToString(readers["FileNo"]),
                    BarcodeNo = Convert.IsDBNull(readers["BarcodeNo"]) ? string.Empty : Convert.ToString(readers["BarcodeNo"]),
                    Description = Convert.IsDBNull(readers["Description"]) ? string.Empty : Convert.ToString(readers["Description"]),
                    CreatedAt = Convert.IsDBNull(readers["CreatedAt"]) ? (DateTime?)null : Convert.ToDateTime(readers["CreatedAt"]),
                    CreatedById = Convert.IsDBNull(readers["CreatedById"]) ? string.Empty : Convert.ToString(readers["CreatedById"]),
                    CreatedByName = Convert.IsDBNull(readers["CreatedByName"]) ? string.Empty : Convert.ToString(readers["CreatedByName"]),

                    FromRoomId = Convert.IsDBNull(readers["FromRoomId"]) ? (int?)null : Convert.ToInt32(readers["FromRoomId"]),
                    ToRoomId = Convert.IsDBNull(readers["ToRoomId"]) ? (int?)null : Convert.ToInt32(readers["ToRoomId"]),
                    RoomInDateTime = Convert.IsDBNull(readers["RoomInTime"]) ? (DateTime?)null : Convert.ToDateTime(readers["RoomInTime"]),
                    RoomOutDateTime = Convert.IsDBNull(readers["RoomOutTime"]) ? (DateTime?)null : Convert.ToDateTime(readers["RoomOutTime"]),
                    IsMoving = Convert.IsDBNull(readers["IsMoving"]) ? (bool?)null : Convert.ToBoolean(readers["IsMoving"]),
                    FromRoomName = Convert.IsDBNull(readers["FromRoomName"]) ? string.Empty : Convert.ToString(readers["FromRoomName"]),
                    ToRoomName = Convert.IsDBNull(readers["ToRoomName"]) ? string.Empty : Convert.ToString(readers["ToRoomName"]),
                };

                models.Add(model);
            }

            return models;
        }

        public static List<FileMovementModel> ToFileMovement(DbDataReader readers)
        {
            if (readers == null)
                return null;
            var models = new List<FileMovementModel>();

            while (readers.Read())
            {
                var model = new FileMovementModel
                {
                    Id = Convert.ToString(readers["Id"]),
                    FileName = Convert.IsDBNull(readers["FileName"]) ? string.Empty : Convert.ToString(readers["FileName"]),
                    FileNo = Convert.IsDBNull(readers["FileNo"]) ? string.Empty : Convert.ToString(readers["FileNo"]),
                    MoveIn = Convert.IsDBNull(readers["MoveIn"]) ? (bool?)null: Convert.ToBoolean(readers["MoveIn"]),
                    MoveOut = Convert.IsDBNull(readers["MoveOut"]) ? (bool?)null : Convert.ToBoolean(readers["MoveOut"]),
                    FileId = Convert.ToInt32(readers["FileId"]),
                    RoomId = Convert.ToInt32(readers["RoomId"]),
                    ActionAt = Convert.ToDateTime(readers["ActionAt"]),
                    ActionById = Convert.ToString(readers["ActionById"]),
                    ActionByName = Convert.IsDBNull(readers["ActionByName"]) ? string.Empty : Convert.ToString(readers["ActionByName"]),
                    RoomNo = Convert.IsDBNull(readers["RoomNo"]) ? string.Empty : Convert.ToString(readers["RoomNo"]),
                };

                models.Add(model);
            }

            return models;
        }
    }
}
