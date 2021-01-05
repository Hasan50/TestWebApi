using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ToDoApp.Business.Mappers
{
    public class InputHelpMapper
    {

        public static List<InputHelpModel> ToInputHelp(DbDataReader readers)
        {
            if (readers == null)
                return null;
            var models = new List<InputHelpModel>();

            while (readers.Read())
            {
                var model = new InputHelpModel
                {
                    Id =  Convert.ToInt32(readers["Id"]),
                    Name = Convert.IsDBNull(readers["Name"]) ? string.Empty : Convert.ToString(readers["Name"]),
                    InputHelpTypeId = Convert.ToInt32(readers["InputHelpTypeId"]),
                    CreatedAt = Convert.ToDateTime(readers["CreatedAt"]),
                    CreatedById = Convert.ToString(readers["CreatedById"]),
                    CreatedByName = Convert.IsDBNull(readers["CreatedByName"]) ? string.Empty : Convert.ToString(readers["CreatedByName"]),
                    UpdatedAt = Convert.IsDBNull(readers["UpdatedAt"]) ? (DateTime?)null : Convert.ToDateTime(readers["UpdatedAt"]),
                };

                models.Add(model);
            }

            return models;
        }

        public static List<RoomSetupModel> ToRoomSetup(DbDataReader readers)
        {
            if (readers == null)
                return null;
            var models = new List<RoomSetupModel>();

            while (readers.Read())
            {
                var model = new RoomSetupModel
                {
                    Id = Convert.ToInt32(readers["Id"]),
                    RoomNo = Convert.IsDBNull(readers["RoomNo"]) ? string.Empty : Convert.ToString(readers["RoomNo"]),
                    Description = Convert.IsDBNull(readers["Description"]) ? string.Empty : Convert.ToString(readers["Description"]),
                    BuildingName = Convert.IsDBNull(readers["BuildingName"]) ? string.Empty : Convert.ToString(readers["BuildingName"]),
                    BuildingId = Convert.ToInt32(readers["BuildingId"]),
                    IsRack = Convert.IsDBNull(readers["IsRack"]) ? (bool?)null : Convert.ToBoolean(readers["IsRack"])
                };

                models.Add(model);
            }

            return models;
        }
    }
}
