using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IInputHelp
    {
        ResponseModel Save(InputHelpModel model);
        List<InputHelpModel> GetAll(int? id);
    }
}
