using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Business.Mappers
{
   public class UserMapper
    {

        public static List<UserIdentityModel> ToUserCredentialModel(DbDataReader readers)
        {
            if (readers == null)
                return null;
            var models = new List<UserIdentityModel>();

            while (readers.Read())
            {
                var model = new UserIdentityModel
                {
                    Id = Convert.IsDBNull(readers["Id"]) ? string.Empty : Convert.ToString(readers["Id"]),
                    Email = Convert.IsDBNull(readers["Email"]) ? string.Empty : Convert.ToString(readers["Email"]),
                    PhoneNumber = Convert.IsDBNull(readers["PhoneNumber"]) ? string.Empty : Convert.ToString(readers["PhoneNumber"]),
                    FullName = Convert.IsDBNull(readers["FullName"]) ? string.Empty : Convert.ToString(readers["FullName"]),
                    UserTypeId = Convert.ToInt32(readers["UserTypeId"]),
                    IsActive = Convert.ToBoolean(readers["IsActive"]),
                    LoginID = Convert.IsDBNull(readers["LoginID"]) ? string.Empty : Convert.ToString(readers["LoginID"]),
                };

                models.Add(model);
            }

            return models;
        }
    }
}
