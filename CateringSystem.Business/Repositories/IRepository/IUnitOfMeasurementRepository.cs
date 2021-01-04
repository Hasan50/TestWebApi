using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IUnitOfMeasurementRepository
    {
        IEnumerable<UnitOfMeasurement> GetUnitOfMeasurementList(int count);
        ResponseModel SaveUnitOfMeasurement(UnitOfMeasurement model);
    }
}
