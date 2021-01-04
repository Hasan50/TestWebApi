using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface ISectorRepository
    {
        IEnumerable<Sector> GetSectorList(int count);
        ResponseModel SaveSector(Sector model);
    }
}
