using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IFileInformation
    {
        ResponseModel Save(FileInformation model);
        List<FileInformation> GetAll(int? id);

    }
}
