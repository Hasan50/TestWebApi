using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IFileInformation
    {
        ResponseModel Save(FileInformation model);
        List<FileInformation> GetAll(int? id);

    }
}
