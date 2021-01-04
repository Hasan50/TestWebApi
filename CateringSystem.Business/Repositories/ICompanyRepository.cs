using CateringSystem.Business.Models;
using System.Collections.Generic;

namespace CateringSystem.Business.Interfaces
{
    public interface ICompanyRepository
    {
        List<CompanyViewModel> GetCompanyList(int count);
        ResponseModel SaveCompany(Company model);
        ResponseModel DeleteCompany(Company model);
    }
}
