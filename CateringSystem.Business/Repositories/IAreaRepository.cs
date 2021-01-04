using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IAreaRepository
    {
        IEnumerable<Area> GetAreaList(int count);
        ResponseModel SaveArea(Area model);
    }
}
