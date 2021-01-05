using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IMasterSettingRepository
    {
        MasterSetting GetMasterSetting();
        ResponseModel SaveMasterSetting(MasterSetting model);
    }
}
