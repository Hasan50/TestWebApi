using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IPackage
    {
        ResponseModel Save(RoomSetupModel model);
        List<RoomSetupModel> GetAll(int? id);
    }
}
