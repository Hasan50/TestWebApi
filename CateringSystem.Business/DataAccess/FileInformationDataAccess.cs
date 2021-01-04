using CateringSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using CateringSystem.Business.Models;
using CateringSystem.Framework;
using System.Data;
using CateringSystem.Business.Mappers;

namespace CateringSystem.Business.DataAccess
{
    public class FileInformationDataAccess : BaseDatabaseHandler, IFileInformation
    {
        public ResponseModel Save(FileInformation model)
        {
            var err = string.Empty;

            const string sql = @"IF NOT EXISTS(SELECT TOP 1 * FROM FileInformation WHERE Id=@Id)
                                BEGIN
                                INSERT INTO FileInformation(FileName,FileNo,BarcodeNo,Description,CreatedAt,CreatedById)
                                                     VALUES(@FileName,@FileNo,@BarcodeNo,@Description,@CreatedAt,@CreatedById)
                                END
                                ELSE
                                BEGIN
                                 UPDATE FileInformation SET FileName=@FileName,FileNo=@FileNo,Description=@Description,UpdatedAt=@CreatedAt,UpdatedById=@CreatedById 
                                    WHERE Id=@Id
                                END";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@Id", ParamValue =model.Id},
                    new QueryParamObj { ParamName = "@FileNo", ParamValue =string.IsNullOrEmpty(model.FileNo)?null:model.FileNo},
                    new QueryParamObj { ParamName = "@FileName", ParamValue =string.IsNullOrEmpty(model.FileName)?null:model.FileName},
                    new QueryParamObj { ParamName = "@BarcodeNo", ParamValue =string.IsNullOrEmpty(model.BarcodeNo)?null:model.BarcodeNo},
                    new QueryParamObj { ParamName = "@Description", ParamValue =string.IsNullOrEmpty(model.Description)?null:model.Description},
                    new QueryParamObj { ParamName = "@CreatedAt", ParamValue =DateTime.UtcNow,DBType=DbType.DateTime},
                    new QueryParamObj { ParamName = "@CreatedById", ParamValue =model.CreatedById}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }

        public List<FileInformation> GetAll(int? id)
        {
            const string sql = @"SELECT C.*,cc.UserName CreatedByName,F.RoomNo FromRoomName
                                ,T.RoomNo ToRoomName
                                 FROM FileInformation C
                                LEFT JOIN UserCredentials CC ON C.CreatedById=CC.Id
                                LEFT JOIN RoomSetup F ON C.FromRoomId=F.Id
                                LEFT JOIN RoomSetup T ON C.ToRoomId=T.Id
                                WHERE (@Id IS NULL OR c.Id=@Id)";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@Id", ParamValue = id,DBType=DbType.Int32}
                };
            return ExecuteDBQuery(sql, queryParamList, FileInformationMapper.ToFile);
        }


    }
}
