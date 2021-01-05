using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IUnitOfMeasurementRepository
    {
        IEnumerable<UnitOfMeasurement> GetUnitOfMeasurementList(int count);
        ResponseModel SaveUnitOfMeasurement(UnitOfMeasurement model);
    }
}
