using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IDesignationRepository
    {
        IEnumerable<DesignationModel> GetDesignationList();
        ResponseModel SaveDesignation(DesignationModel model);
    }
}
