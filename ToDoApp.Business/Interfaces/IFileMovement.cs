using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IFileMovement
    {
        ResponseModel Save(FileMovementModel model);
        List<FileMovementModel> GetMovements(int fileId);
    }
}
