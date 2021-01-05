using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IAreaRepository
    {
        IEnumerable<Area> GetAreaList(int count);
        ResponseModel SaveArea(Area model);
    }
}
