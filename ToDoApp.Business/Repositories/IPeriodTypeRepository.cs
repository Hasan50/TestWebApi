using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IPeriodTypeRepository
    {
        IEnumerable<PeriodType> GetPeriodTypeList(int count);
        IEnumerable<object> GetPeriodTypeCboList();
        ResponseModel Save(PeriodType model);
        ResponseModel DeletePeriodType(PeriodType model);
    }
}
