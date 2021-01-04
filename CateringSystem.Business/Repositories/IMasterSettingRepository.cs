using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IMasterSettingRepository
    {
        MasterSetting GetMasterSetting();
        ResponseModel SaveMasterSetting(MasterSetting model);
    }
}
