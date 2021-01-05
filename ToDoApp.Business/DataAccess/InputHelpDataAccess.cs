using ToDoApp.Business.Interfaces;
using System;
using System.Collections.Generic;
using ToDoApp.Business.Models;
using ToDoApp.Framework;
using System.Data;
using ToDoApp.Business.Mappers;

namespace ToDoApp.Business.DataAccess
{
   public class InputHelpDataAccess : BaseDatabaseHandler, IInputHelp
    {
        public ResponseModel Save(InputHelpModel model)
        {
            var err = string.Empty;

            const string sql = @"IF NOT EXISTS(SELECT TOP 1 * FROM InputHelp WHERE Id=@Id)
                                BEGIN
                                INSERT INTO InputHelp(Name,InputHelpTypeId,CreatedAt,CreatedById)
                                                     VALUES(@Name,@InputHelpTypeId,@CreatedAt,@CreatedById)
                                END
                                ELSE
                                BEGIN
                                 UPDATE InputHelp SET Name=@Name,InputHelpTypeId=@InputHelpTypeId,UpdatedAt=@CreatedAt,UpdatedById=@CreatedById 
                                    WHERE Id=@Id
                                END";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@Id", ParamValue =model.Id},
                    new QueryParamObj { ParamName = "@Name", ParamValue =string.IsNullOrEmpty(model.Name)?null:model.Name},
                    new QueryParamObj { ParamName = "@InputHelpTypeId", ParamValue =model.InputHelpTypeId},
                    new QueryParamObj { ParamName = "@CreatedAt", ParamValue =DateTime.UtcNow,DBType=DbType.DateTime},
                    new QueryParamObj { ParamName = "@CreatedById", ParamValue =model.CreatedById}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }

        public List<InputHelpModel> GetAll(int? id)
        {
            const string sql = @"SELECT C.*,cc.UserName CreatedByName FROM InputHelp C
                                LEFT JOIN UserCredentials CC ON C.CreatedById=CC.Id
                                WHERE (@Id IS NULL OR c.Id=@Id)";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@Id", ParamValue = id,DBType=DbType.Int32}
                };
            return ExecuteDBQuery(sql, queryParamList, InputHelpMapper.ToInputHelp);
        }
    }
}
