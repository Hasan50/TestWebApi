using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IFileMovement
    {
        ResponseModel Save(FileMovementModel model);
        List<FileMovementModel> GetMovements(int fileId);
    }
}
