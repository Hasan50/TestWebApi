using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;

namespace ToDoApp.Business.Interfaces
{
    public interface IUserCredientialRepository
    {
        List<UserWithPackageAdvanceViewModel> GetUserList(int userTypeId);
        List<UserWithPackageAdvanceViewModel> GetUserAndCompanyList();
        List<UserWithEmployeeViewModel> GetEmployeeList(int userTypeId);
        List<Employee> GetDeliveryManList();
        List<Employee> GetDeliveryManListWithId(string id);
        ResponseModel SaveUser(UserCredientialViewModel model, List<UserWithPackage> advanceOb, List<UserWeekDayOff> userWeekDayOffs);
        ResponseModel UpdateUser(UserCredientialViewModel model, List<UserWithPackage> userWithPackage
    , List<UserWeekDayOff> userWeekDayOffs);
        ResponseModel DeleteUser(UserWithPackageAdvanceViewModel model);
        ResponseModel SaveEmployee(UserCredientialViewModel model, Employee employeeOb);
        ResponseModel UpdateEmployee(UserCredientialViewModel model, Employee employeeOb);
        ResponseModel DeleteEmployee(Employee model);
        ResponseModel SaveCompany(UserCredientialViewModel model, Company companyOb);
        ResponseModel UpdateCompany(UserCredientialViewModel model, Company companyOb);
        List<Employee> CheckEmployeeCode(string employeeCode);
        IEnumerable<UserCredentials> GetExecution();
        ResponseModel SaveDeliverymanCustomerTag(List<DeliveryManCustomerTag> models);
        List<DeliveryManCustomerTagViewModel> GetDeliveryManCustomerTagList(string deliveryManId, DateTime date);
        ResponseModel DeleteeliveryManCustomerTag(DeliveryManCustomerTagViewModel model);
        List<object> GetCustomerWithDetail(string id);
        IEnumerable<UserWeekDayOff> GetUserWeekDayOffList(string userId);
        List<UserCredientialViewModel> GetUerProfileDetail(string userId);
        ResponseModel EditProfile(UserCredientialViewModel model, List<UserWeekDayOff> userWeekDayOffs);
    }
}
