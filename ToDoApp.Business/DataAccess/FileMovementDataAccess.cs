using ToDoApp.Business.Interfaces;
using System;
using System.Collections.Generic;
using ToDoApp.Business.Models;
using ToDoApp.Framework;
using System.Data;
using ToDoApp.Business.Mappers;

namespace ToDoApp.Business.DataAccess
{
    public class FileMovementDataAccess : BaseDatabaseHandler, IFileMovement
    {
        public ResponseModel Save(FileMovementModel model)
        {
            var err = string.Empty;

            const string sql = @"INSERT INTO FileMovement(Id,FileId,RoomId,ActionAt,ActionById,MoveIn,MoveOut)
                                VALUES(@Id,@FileId,@RoomId,@ActionAt,@ActionById,@MoveIn,@MoveOut)";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@Id", ParamValue =Guid.NewGuid().ToString()},
                    new QueryParamObj { ParamName = "@FileId", ParamValue =model.FileId,DBType=DbType.Int32},
                    new QueryParamObj { ParamName = "@RoomId", ParamValue =model.RoomId,DBType=DbType.Int32},
                    new QueryParamObj { ParamName = "@ActionAt", ParamValue =DateTime.UtcNow,DBType=DbType.DateTime},
                    new QueryParamObj { ParamName = "@ActionById", ParamValue =model.ActionById},
                    new QueryParamObj { ParamName = "@MoveIn", ParamValue =model.MoveIn,DBType=DbType.Boolean},
                    new QueryParamObj { ParamName = "@MoveOut", ParamValue =model.MoveOut,DBType=DbType.Boolean}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }

        public List<FileMovementModel> GetMovements(int fileId)
        {
            const string sql = @"SELECT M.*,F.FileName,F.FileNo,c.UserName ActionByName,R.RoomNo  FROM FileMovement m 
                                INNER JOIN FileInformation F ON M.FileId=F.Id
                                LEFT JOIN UserCredentials C ON M.ActionById=C.Id
                                LEFT JOIN RoomSetup R ON M.RoomId=R.Id
                                WHERE m.FileId=@fileId";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@fileId", ParamValue = fileId,DBType=DbType.Int32}
                };
            return ExecuteDBQuery(sql, queryParamList, FileInformationMapper.ToFileMovement);
        }

       
    }
}
