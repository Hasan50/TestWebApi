using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface ISectorRepository
    {
        IEnumerable<Sector> GetSectorList(int count);
        ResponseModel SaveSector(Sector model);
    }
}
