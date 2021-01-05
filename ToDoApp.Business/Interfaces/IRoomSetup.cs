using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IRoomSetup
    {
        ResponseModel Save(RoomSetupModel model);
        List<RoomSetupModel> GetAll(int? id);
    }
}
