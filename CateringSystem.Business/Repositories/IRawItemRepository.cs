using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface IRawItemRepository
    {
        List<RawItemViewModel> GetRawItemList(int count);
        ResponseModel SaveRawItem(RawItem model);
        IEnumerable<object> GetPackageRawItemList(int count, string packageid);
        ResponseModel SavePackageRawItem(List<PackageRawItem> models);
        ResponseModel DeletePackageRawItem(PackageRawItem model);
        ResponseModel DeleteRawItem(RawItem model);
    }
}
