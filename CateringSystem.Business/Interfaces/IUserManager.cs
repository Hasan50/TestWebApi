using CateringSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringSystem.Business.Interfaces
{
   public interface IUserManager
    {
        UserIdentityModel Get(string userName,string password);
        UserIdentityModel GetUserDetails(string userKey);
        ResponseModel Save(UserIdentityModel model);
        ResponseModel ChangePassword(string userName, string password);
        List<UserIdentityModel> GetAll(int userTypeId);
    }
}
