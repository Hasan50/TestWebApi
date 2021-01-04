using CateringSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CateringSystem.Business.Models;
using CateringSystem.Framework;
using System.Data;
using CateringSystem.Business.Mappers;

namespace CateringSystem.Business.DataAccess
{
    public class UserDataAccess : BaseDatabaseHandler, IUserManager
    {
        public UserIdentityModel Get(string userName, string password)
        {
            const string sql = @"SELECT C.*
                                 FROM UserCredentials C 
                                WHERE c.LoginID=@userName AND C.Password=@password";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@userName", ParamValue = userName},
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@password", ParamValue = password}
                };
            var results = ExecuteDBQuery(sql, queryParamList, UserMapper.ToUserCredentialModel);
            return results.Any() ? results.FirstOrDefault() : null;
        }

        public UserIdentityModel GetUserDetails(string userKey)
        {
            const string sql = @"SELECT C.*,R.RoomNo,D.Name DesignationName,Department.Name DepartmentName
                             FROM UserCredentials C 
                            LEFT JOIN RoomSetup R ON C.RoomId=R.Id
                            LEFT JOIN InputHelp D ON C.DesignationId=d.Id
                            LEFT JOIN InputHelp Department ON C.DepartmentId=Department.Id
                                WHERE C.Id=@userKey";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@userKey", ParamValue = userKey}
                };
            var results = ExecuteDBQuery(sql, queryParamList, UserMapper.ToUserCredentialModel);
            return results.Any() ? results.FirstOrDefault() : null;
        }

        public ResponseModel Save(UserIdentityModel model)
        {
            var err = string.Empty;

            const string sql = @"INSERT INTO UserCredentials(Id,LoginID,Password,UserName,Email,UserTypeId,IsActive,DesignationId,DepartmentId,RoomId,PhoneNumber)
                                VALUES(@Id,@LoginID,@Password,@UserName,@Email,@UserTypeId,1,@DesignationId,@DepartmentId,@RoomId,@PhoneNumber)";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@Id", ParamValue =Guid.NewGuid().ToString()},
                    new QueryParamObj { ParamName = "@LoginID", ParamValue =model.LoginID},
                    new QueryParamObj { ParamName = "@Password", ParamValue =model.Password},
                    new QueryParamObj { ParamName = "@Email", ParamValue =model.Email},
                    new QueryParamObj { ParamName = "@UserTypeId", ParamValue =model.UserTypeId},
                    new QueryParamObj { ParamName = "@PhoneNumber", ParamValue =model.PhoneNumber}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }

        public ResponseModel ChangePassword(string userInitial, string newPassword)
        {
            var err = string.Empty;

            const string sql = @"UPDATE UserCredentials set Password=@newPassword where LoginID=@userInitial";

            var queryParamList = new QueryParamList
               {
                    new QueryParamObj { ParamName = "@userInitial", ParamValue =userInitial},
                    new QueryParamObj { ParamName = "@newPassword", ParamValue =newPassword}
                };

            DBExecCommandEx(sql, queryParamList, ref err);
            return new ResponseModel { Success = string.IsNullOrEmpty(err) };
        }
        public List<UserIdentityModel> GetAll(int userTypeId)
        {
            const string sql = @"SELECT C.*,R.RoomNo,D.Name DesignationName,Department.Name DepartmentName
                                FROM UserCredentials C 
                                LEFT JOIN RoomSetup R ON C.RoomId=R.Id
                                LEFT JOIN InputHelp D ON C.DesignationId=d.Id
                                LEFT JOIN InputHelp Department ON C.DepartmentId=Department.Id
								where UserTypeId=@userTypeId";
            var queryParamList = new QueryParamList
                {
                    new QueryParamObj { ParamDirection = ParameterDirection.Input, ParamName = "@userTypeId", ParamValue = userTypeId,DBType=DbType.Int32}
                };
            return ExecuteDBQuery(sql, queryParamList, UserMapper.ToUserCredentialModel);
        }
    }
}
