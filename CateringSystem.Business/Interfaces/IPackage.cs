using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IPackage
    {
        ResponseModel Save(RoomSetupModel model);
        List<RoomSetupModel> GetAll(int? id);
    }
}
