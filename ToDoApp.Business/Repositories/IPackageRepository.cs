using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IPackageRepository
    {
        IEnumerable<Package> GetPackageList(int count);
        IEnumerable<PackageAdvanceViewModel> GetPackageAdvanceList(int count);
        IEnumerable<object> GetPackageTestList();
        IEnumerable<PackageAdvance> GetPackageWithAdvanceList(string packageId);
        IEnumerable<UserPackageViewModel> GetUserPackageDetailList(string userId);
        IEnumerable<object> GetPackageCboList();
        List<PackageTergetTotalCountViewModel> GetPackageWithProductionCountList(DateTime date);
        List<PackageTergetTotalCountViewModel> GetPackageWithTergetCountList(DateTime date);
        ResponseModel SavePackage(Package model);
        ResponseModel DeletePackage(Package model);
        ResponseModel SavePackageAdvance(PackageAdvance model);
        ResponseModel DeletePackageAdvance(PackageAdvance model);
        ResponseModel SavePackageAssign(List<DailyUserPackageDeliveryViewModel> models);
        List<PackageTergetViewModel> GetPackageTergetList(string customerId);
        List<PackageTergetViewModel> GetPackageDeliveryList(string customerId);
        ResponseModel SaveAutoExecPackageDeliveryTerget();
        ResponseModel UpdatePackageDeliveryTerget(PackageDeliveryTergetViewModel model);
        ResponseModel UpdatePackageDelivery(PackageDeliveryTergetViewModel model);
        IEnumerable<UserWithPackage> GetUserWithPackageList(string userId);
        IEnumerable<object> GetUserWithPackageDetailList(string userId);
        List<DailyUserPackageDelivery> GetDailyUserPackageDeliveriesWithUserId(string userId, DateTime date);
        List<UserDuePackageViewModel> GetDuePackageList(string userId);
        List<UserDueAndPaidViewModel> GetUserDueAndPaidList(string userId);
        List<UserDueAndPaidViewModel> GetUserDueAndPaidWithDateList(string userId, DateTime date);
    }
}
