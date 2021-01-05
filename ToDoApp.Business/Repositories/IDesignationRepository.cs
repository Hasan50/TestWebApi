using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IDesignationRepository
    {
        IEnumerable<DesignationModel> GetDesignationList();
        ResponseModel SaveDesignation(DesignationModel model);
    }
}
