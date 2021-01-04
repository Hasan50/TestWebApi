using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IDayShiftRepository
    {
        IEnumerable<object> GetDayShiftCboList();
        IEnumerable<DayShift> GetDayShiftList();
        IEnumerable<DayShiftPackageViewModel> GetDayShiftPackageList(int count, int dayShiftId);
        IEnumerable<object> GetDayShiftPackageListWithUserId(int count, int dayShiftId, string userId);
        ResponseModel SaveDayShiftPackage(List<DayShiftWithPackage> models);
        ResponseModel DeleteDayShiftPackage(DayShiftWithPackage model);
        ResponseModel Save(DayShift model);
        ResponseModel DeleteShift(DayShift model);
    }
}
