using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IPackageProductRepository
    {
        IEnumerable<PackageProduct> GetPackageProductList(int count);
        ResponseModel SavePackageProduct(PackageProduct model);
        ResponseModel DeletePackageProduct(PackageProduct model);
        IEnumerable<object> GetPackageWithProductList(int count, string packageid, DateTime date);
        ResponseModel SavePackageWithProduct(List<PackageWithProductMasterDetail> models, PackageWithProductMaster model);
    }
}
