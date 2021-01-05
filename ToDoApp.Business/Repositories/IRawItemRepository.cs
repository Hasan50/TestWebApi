using ToDoApp.Business.Models;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
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
