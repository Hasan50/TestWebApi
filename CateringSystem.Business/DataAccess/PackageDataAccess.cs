using CateringSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using CateringSystem.Business.Models;
using CateringSystem.Framework;
using System.Data;
using CateringSystem.Business.Mappers;
using CateringSystem.Business.Repositories;

namespace CateringSystem.Business.DataAccess
{
   public class PackageDataAccess : BaseDatabaseHandler,IPackage
    {
        public ResponseModel Save(RoomSetupModel model)
        {
            var err = string.Empty;

            const string sql = @"IF NOT EXISTS(SELECT TOP 1 * FROM RoomSetup WHERE Id=@Id)
                                BEGIN
                                INSERT INTO RoomSetup(RoomNo,BuildingId,Description,IsRack)
                                                     VALUES(@RoomNo,@BuildingId,@Description,@IsRack)
                                END
                                ELSE
                                BEGIN
                                 UPDATE RoomSetup SET RoomNo=@RoomNo,BuildingId=@BuildingId,Description=@Description,IsRack=@IsRack 
                                    WHERE Id=@Id
                                END";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@Id", ParamValue =model.Id},
                    new QueryParamObj { ParamName = "@RoomNo", ParamValue =string.IsNullOrEmpty(model.RoomNo)?null:model.RoomNo},
                    new QueryParamObj { ParamName = "@BuildingId", ParamValue =model.BuildingId},
                    new QueryParamObj { ParamName = "@Description", ParamValue =string.IsNullOrEmpty(model.Description)?null:model.Description},
                    new QueryParamObj { ParamName = "@IsRack", ParamValue =model.IsRack}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }

        public List<RoomSetupModel> GetAll(int? id)
        {
            const string sql = @"SELECT R.Id,R.BuildingId,R.RoomNo,R.Description,CASE WHEN IsRack = 1 THEN 'True' WHEN IsRack IS NULL THEN 'False' ELSE 'False' END IsRack,I.Name BuildingName From RoomSetup R left join InputHelp I on R.BuildingId = I.Id WHERE (@Id IS NULL OR R.Id=@Id)";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@Id", ParamValue = id,DBType=DbType.Int32}
                };
            return ExecuteDBQuery(sql, queryParamList, InputHelpMapper.ToRoomSetup);
        }
    }
}
