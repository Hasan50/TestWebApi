using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IInputHelp
    {
        ResponseModel Save(InputHelpModel model);
        List<InputHelpModel> GetAll(int? id);
    }
}
