using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using ToDoApp.Business.Repositories;
using ToDoApp.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace ToDoApp.Business.DataAccess
{
    public class UserCredientialRepository : Repository<Package>, IUserCredientialRepository
    {
        public UserCredientialRepository(CateringDbContext context)
            : base(context)
        {
        }
        public List<UserWithPackageAdvanceViewModel> GetUserList(int userTypeId)
        {
            var commandText = @"select  p.Id,p.FullName,p.FileId,p.FileName,p.PhoneNumber,p.ImagePath,p.Email,co.Name CompanyName,co.Address CompanyAddress,c.CompanyId,c.PaidAmount,C.DueAmount,Package= e.Name,PackagePrice=d.Amount,pe.Name PeriodType,pe.Count PeriodCount,p.IsActive from UserCredentials p
                                left join UserCredientialDetail c on p.Id= c.UserCredentialId
                                left join UserWithPackage d on p.Id = d.UserCrediantialId
                                left join Package e on d.PackageId = e.Id
								left join PeriodType pe on d.PeriodTypeId=pe.Id
                                left join Company co on c.CompanyId=co.Id
                                where p.UserTypeId=@userTypeId order by c.CreatedDate";
            var name = new SqlParameter("@userTypeId", userTypeId);
            var data = DbContext.Database.SqlQuery<UserWithPackageAdvanceViewModel>(commandText, name).ToList();
            return data;
        }
        public List<UserWithPackageAdvanceViewModel> GetUserAndCompanyList()
        {
            var commandText = @"select  p.Id,p.FullName,p.FileId,p.FileName,p.PhoneNumber,p.ImagePath,p.Email,co.Name CompanyName,co.Address CompanyAddress,c.CompanyId,c.PaidAmount,C.DueAmount,Package= e.Name,PackagePrice=d.Amount,p.IsActive from UserCredentials p
                                left join UserCredientialDetail c on p.Id= c.UserCredentialId
                                left join UserWithPackage d on p.Id = d.UserCrediantialId
                                left join Package e on d.PackageId = e.Id
                                left join Company co on c.CompanyId=co.Id
                                where p.UserTypeId in(2,4) and c.AccountType in ('personal','company')";
            var name = new SqlParameter("@userTypeId", "");
            var data = DbContext.Database.SqlQuery<UserWithPackageAdvanceViewModel>(commandText, name).ToList();
            return data;
        }
        public List<UserWithEmployeeViewModel> GetEmployeeList(int userTypeId)
        {
            var commandText = @"select  d.Id,d.LoginKey,p.LoginID UserName,c.DathOfBirth,c.NationalId,c.Department,c.Section,c.BloodGroup,c.Gender,d.EmployeeCode,d.DesignationId,p.FullName,p.PhoneNumber,p.ImagePath,p.Email,c.Address,p.IsActive from UserCredentials p
                                left join UserCredientialDetail c on p.Id= c.UserCredentialId
                                left join Employee d on p.Id = d.LoginKey
                                where p.UserTypeId=@userTypeId order by c.CreatedDate";
            var name = new SqlParameter("userTypeId", userTypeId);
            var data = DbContext.Database.SqlQuery<UserWithEmployeeViewModel>(commandText, name).ToList();
            return data;
        }
        public List<Employee> GetDeliveryManList()
        {
            var commandText = @"SELECT * FROM Employee where DesignationId=1";
            var name = new SqlParameter("userTypeId", "");
            var data = DbContext.Database.SqlQuery<Employee>(commandText, name).ToList();
            return data;
        }
        public List<Employee> GetDeliveryManListWithId(string id)
        {
            var commandText = @"SELECT * FROM Employee where LoginKey=@id and DesignationId=1";
            var parameter = new SqlParameter("id", id);
            var data = DbContext.Database.SqlQuery<Employee>(commandText, parameter).ToList();
            return data;
        }
        public void GetUserList2(int userTypeId)
        {
            var commandText = @"SELECT u.Id,u.FullName FROM UserCredentials u
                                LEFT JOIN UserCredientialDetail ud on u.Id = ud.UserCredentialId
                                where UserTypeId = @userTypeId
                                order by u.CreatedDate";
            var name = new SqlParameter("@userTypeId", userTypeId);
            var data = DbContext.Database.ExecuteSqlCommand(commandText, name);
        }
        public ResponseModel SaveUser(UserCredientialViewModel model, List<UserWithPackage> userWithPackage
            , List<UserWeekDayOff> userWeekDayOffs)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = new UserCredentials
                {
                    Id = Guid.NewGuid().ToString(),
                    LoginID = model.LoginID,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    UserTypeId = model.UserTypeId,
                    IsActive = model.IsActive,
                    PhoneNumber = model.PhoneNumber,
                    ImagePath = model.ImagePath,
                    FileId = model.FileId,
                    FileName = model.FileName,
                    CreatedDate = DateTime.Now,
                    CreatedById = model.CreatedById,
                    UpdatedDate = null
                };
                UserCredientialDetail userCredientialDetail = new UserCredientialDetail
                {
                    UserCredentialId = userCredentials.Id,
                    Gender = model.Gender,
                    Age = CommonHelper.CalculateYourAge(model.DathOfBirth).Years,
                    DathOfBirth = model.DathOfBirth,
                    BloodGroup = model.BloodGroup,
                    Weight = model.Weight,
                    Height = model.Height,
                    Diabetes = model.Diabetes,
                    Allergy = model.Allergy,
                    AllergyDetail = model.AllergyDetail,
                    BP = model.BP,
                    NationalId = model.NationalId,
                    CompanyId = model.CompanyId,
                    CompanyName = model.CompanyName,
                    CompanyAddress = model.Address,
                    Address = model.Address,
                    Department = model.Department,
                    Section = model.Section,
                    Designation = model.Designation,
                    ReferencePersonName = model.ReferencePersonName,
                    ReferencePersonCellNumber = model.ReferencePersonCellNumber,
                    ReplacementMenu = model.ReplacementMenu,
                    SpecialMenu = model.SpecialMenu,
                    WeekDayOff = model.WeekDayOff,
                    SalesExecutiveId = model.SalesExecutiveId,
                    DeliveryManId = model.DeliveryManId,
                    PaidAmount = model.PaidAmount,
                    DueAmount = model.DueAmount,
                    RegistrationAmount = model.RegistrationAmount,
                    Status = model.Status,
                    AccountType = model.AccountType,
                    CreatedDate = DateTime.Now
                };
                UserActiveStatus userActiveStatus = new UserActiveStatus
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = model.Status,
                    UserCredentialId = userCredentials.Id,
                    CreatedDate = DateTime.Now,
                    CreatedById = model.CreatedById,

                };

                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.UserCredentials.Add(userCredentials);
                SaveUserDetail(userCredientialDetail);
                DbContext.SaveChanges();
                DbContext.UserActiveStatus.Add(userActiveStatus);

                foreach (var item in userWithPackage)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.UserCrediantialId = userCredentials.Id;
                    item.CreatedDate = DateTime.Now;
                    SaveUserAdvance(item);
                }
                if (userWeekDayOffs != null)
                {
                    foreach (var item in userWeekDayOffs)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.UserId = userCredentials.Id;
                        item.CreatedDate = DateTime.Now;
                        SaveUserWeekDayOff(item);
                    }
                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel UpdateUser(UserCredientialViewModel model, List<UserWithPackage> userWithPackage
    , List<UserWeekDayOff> userWeekDayOffs)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = DbContext.UserCredentials.Where(r => r.Id == model.Id).FirstOrDefault();
                userCredentials.LoginID = model.LoginID;
                userCredentials.FullName = model.FullName;
                userCredentials.Email = model.Email;
                userCredentials.IsActive = model.IsActive;
                userCredentials.PhoneNumber = model.PhoneNumber;
                userCredentials.ImagePath = model.ImagePath;
                userCredentials.FileId = model.FileId;
                userCredentials.FileName = model.FileName;
                userCredentials.UpdatedDate = DateTime.Now;

                UserCredientialDetail userCredientialDetail = DbContext.UserCredientialDetail.Where(r => r.UserCredentialId == model.Id).FirstOrDefault();
                userCredientialDetail.UserCredentialId = userCredentials.Id;
                userCredientialDetail.Gender = model.Gender;
                userCredientialDetail.Age = CommonHelper.CalculateYourAge(model.DathOfBirth).Years;
                userCredientialDetail.DathOfBirth = model.DathOfBirth;
                userCredientialDetail.BloodGroup = model.BloodGroup;
                userCredientialDetail.Weight = model.Weight;
                userCredientialDetail.Height = model.Height;
                userCredientialDetail.Diabetes = model.Diabetes;
                userCredientialDetail.Allergy = model.Allergy;
                userCredientialDetail.AllergyDetail = model.AllergyDetail;
                userCredientialDetail.BP = model.BP;
                userCredientialDetail.NationalId = model.NationalId;
                userCredientialDetail.CompanyId = model.CompanyId;
                userCredientialDetail.CompanyName = model.CompanyName;
                userCredientialDetail.CompanyAddress = model.Address;
                userCredientialDetail.Address = model.Address;
                userCredientialDetail.Department = model.Department;
                userCredientialDetail.Section = model.Section;
                userCredientialDetail.Designation = model.Designation;
                userCredientialDetail.ReferencePersonName = model.ReferencePersonName;
                userCredientialDetail.ReferencePersonCellNumber = model.ReferencePersonCellNumber;
                userCredientialDetail.ReplacementMenu = model.ReplacementMenu;
                userCredientialDetail.SpecialMenu = model.SpecialMenu;
                userCredientialDetail.WeekDayOff = model.WeekDayOff;
                userCredientialDetail.SalesExecutiveId = model.SalesExecutiveId;
                userCredientialDetail.DeliveryManId = model.DeliveryManId;
                userCredientialDetail.PaidAmount = model.PaidAmount;
                userCredientialDetail.DueAmount = model.DueAmount;
                userCredientialDetail.RegistrationAmount = model.RegistrationAmount;
                userCredientialDetail.Status = model.Status;
                userCredientialDetail.AccountType = model.AccountType;
                userCredientialDetail.LunchReceiveAddress = model.LunchReceiveAddress;
                userCredientialDetail.UpdatedDate = DateTime.Now;

                UserActiveStatus userActiveStatus = DbContext.UserActiveStatus.Where(r => r.UserCredentialId == userCredentials.Id).OrderByDescending(a => a.CreatedDate).FirstOrDefault();
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.Entry(userCredentials).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                DbContext.Entry(userCredientialDetail).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                if (userActiveStatus.Status != model.Status)
                {
                    userActiveStatus.Status = model.Status;
                    DbContext.Entry(userActiveStatus).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var item in userWithPackage)
                {
                    if (item.Id == null)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.UserCrediantialId = userCredentials.Id;
                        item.CreatedDate = DateTime.Now;
                        SaveUserAdvance(item);
                    }
                    else
                    {
                        UserWithPackage up = DbContext.UserWithPackage.Where(r => r.Id == item.Id).FirstOrDefault();
                        up.UpdatedDate = DateTime.Now;
                        DbContext.Entry(up).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                if (userWeekDayOffs != null)
                {
                    foreach (var item in userWeekDayOffs)
                    {
                        if (item.Id == null)
                        {
                            item.Id = Guid.NewGuid().ToString();
                            item.UserId = userCredentials.Id;
                            item.CreatedDate = DateTime.Now;
                            SaveUserWeekDayOff(item);
                        }
                        else
                        {
                            UserWeekDayOff uw = DbContext.UserWeekDayOff.Where(r => r.Id == item.Id).FirstOrDefault();
                            uw.UpdatedDate = DateTime.Now;
                            DbContext.Entry(uw).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel EditProfile(UserCredientialViewModel model, List<UserWeekDayOff> userWeekDayOffs)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = DbContext.UserCredentials.Where(r => r.Id == model.Id).FirstOrDefault();
                userCredentials.FullName = model.FullName;
                userCredentials.Email = model.Email;
                userCredentials.IsActive = model.IsActive;
                userCredentials.PhoneNumber = model.PhoneNumber;
                userCredentials.ImagePath = model.ImagePath;
                userCredentials.FileId = model.FileId;
                userCredentials.FileName = model.FileName;
                userCredentials.UpdatedDate = DateTime.Now;

                UserCredientialDetail userCredientialDetail = DbContext.UserCredientialDetail.Where(r => r.UserCredentialId == model.Id).FirstOrDefault();
                userCredientialDetail.UserCredentialId = userCredentials.Id;
                userCredientialDetail.Gender = model.Gender;
                userCredientialDetail.Age = CommonHelper.CalculateYourAge(model.DathOfBirth).Years;
                userCredientialDetail.DathOfBirth = model.DathOfBirth;
                userCredientialDetail.BloodGroup = model.BloodGroup;
                userCredientialDetail.Weight = model.Weight;
                userCredientialDetail.Height = model.Height;
                userCredientialDetail.Diabetes = model.Diabetes;
                userCredientialDetail.Allergy = model.Allergy;
                userCredientialDetail.AllergyDetail = model.AllergyDetail;
                userCredientialDetail.BP = model.BP;
                userCredientialDetail.NationalId = model.NationalId;
                userCredientialDetail.CompanyId = model.CompanyId;
                userCredientialDetail.CompanyName = model.CompanyName;
                userCredientialDetail.CompanyAddress = model.Address;
                userCredientialDetail.Address = model.Address;
                userCredientialDetail.Department = model.Department;
                userCredientialDetail.Section = model.Section;
                userCredientialDetail.Designation = model.Designation;
                userCredientialDetail.ReferencePersonName = model.ReferencePersonName;
                userCredientialDetail.ReferencePersonCellNumber = model.ReferencePersonCellNumber;
                userCredientialDetail.ReplacementMenu = model.ReplacementMenu;
                userCredientialDetail.SpecialMenu = model.SpecialMenu;
                userCredientialDetail.WeekDayOff = model.WeekDayOff;
                userCredientialDetail.LunchReceiveAddress = model.LunchReceiveAddress;
                userCredientialDetail.UpdatedDate = DateTime.Now;

                UserActiveStatus userActiveStatus = DbContext.UserActiveStatus.Where(r => r.UserCredentialId == userCredentials.Id).OrderByDescending(a => a.CreatedDate).FirstOrDefault();
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.Entry(userCredentials).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                DbContext.Entry(userCredientialDetail).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                if (userWeekDayOffs != null)
                {
                    foreach (var item in userWeekDayOffs)
                    {
                        if (item.Id == null)
                        {
                            item.Id = Guid.NewGuid().ToString();
                            item.UserId = userCredentials.Id;
                            item.CreatedDate = DateTime.Now;
                            SaveUserWeekDayOff(item);
                        }
                        else
                        {
                            UserWeekDayOff uw = DbContext.UserWeekDayOff.Where(r => r.Id == item.Id).FirstOrDefault();
                            uw.UpdatedDate = DateTime.Now;
                            DbContext.Entry(uw).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                }
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel SaveUserDetail(UserCredientialDetail model)
        {
            try
            {
                DbContext.UserCredientialDetail.Add(model);
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ResponseModel SaveUserAdvance(UserWithPackage model)
        {
            try
            {
                DbContext.UserWithPackage.Add(model);
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ResponseModel SaveUserWeekDayOff(UserWeekDayOff model)
        {
            try
            {
                DbContext.UserWeekDayOff.Add(model);
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ResponseModel SaveEmployee(UserCredientialViewModel model, Employee employeeOb)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = new UserCredentials
                {
                    Id = Guid.NewGuid().ToString(),
                    LoginID = model.LoginID,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    UserTypeId = model.UserTypeId,
                    IsActive = model.IsActive,
                    PhoneNumber = model.PhoneNumber,
                    ImagePath = model.ImagePath,
                    CreatedDate = DateTime.Now,
                    CreatedById = model.CreatedById,
                    UpdatedDate = null
                };
                UserCredientialDetail userCredientialDetail = new UserCredientialDetail
                {
                    UserCredentialId = userCredentials.Id,
                    Gender = model.Gender,
                    Age = CommonHelper.CalculateYourAge(model.DathOfBirth).Years,
                    DathOfBirth = model.DathOfBirth,
                    BloodGroup = model.BloodGroup,
                    Weight = model.Weight,
                    Height = model.Height,
                    Diabetes = model.Diabetes,
                    Allergy = model.Allergy,
                    AllergyDetail = model.AllergyDetail,
                    BP = model.BP,
                    NationalId = model.NationalId,
                    CompanyName = model.CompanyName,
                    CompanyAddress = model.Address,
                    Address = model.Address,
                    Department = model.Department,
                    Section = model.Section,
                    Designation = model.Designation,
                    ReferencePersonName = model.ReferencePersonName,
                    ReferencePersonCellNumber = model.ReferencePersonCellNumber,
                    ReplacementMenu = model.ReplacementMenu,
                    SpecialMenu = model.SpecialMenu,
                    WeekDayOff = model.WeekDayOff,
                    SalesExecutiveId = model.SalesExecutiveId,
                    DeliveryManId = model.DeliveryManId,
                    LunchStartDate = null,
                    PaidAmount = null,
                    DueAmount = model.DueAmount,
                    CreatedDate = DateTime.Now
                };
                Employee employee = new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    LoginKey = userCredentials.Id,
                    Name = employeeOb.Name,
                    EmployeeCode = employeeOb.EmployeeCode,
                    DesignationId = employeeOb.DesignationId,
                    ContactNo = employeeOb.ContactNo,
                    CreatedAt = DateTime.Now,
                    CreatedById = employeeOb.CreatedById
                };
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.UserCredentials.Add(userCredentials);
                SaveUserDetail(userCredientialDetail);
                SaveUserEmployee(employee);
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel UpdateEmployee(UserCredientialViewModel model, Employee employeeOb)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = DbContext.UserCredentials.Where(r => r.Id == model.Id).FirstOrDefault();
                userCredentials.IsActive = model.IsActive;
                userCredentials.PhoneNumber = model.PhoneNumber;
                userCredentials.Email = model.Email;
                UserCredientialDetail userCredientialDetail = DbContext.UserCredientialDetail.Where(r => r.UserCredentialId == model.Id).FirstOrDefault();
                if (userCredientialDetail != null)
                {
                    userCredientialDetail.DathOfBirth = model.DathOfBirth;
                }
                Employee employee = DbContext.Employee.Where(r => r.Id == employeeOb.Id).FirstOrDefault();
                employee.DesignationId = employeeOb.DesignationId;
                employee.ContactNo = employeeOb.ContactNo;
                employee.Name = employeeOb.Name;
                DbContext.Database.BeginTransaction();
                flag = true;

                DbContext.Entry(userCredentials).State = System.Data.Entity.EntityState.Modified;
                DbContext.Entry(userCredientialDetail).State = System.Data.Entity.EntityState.Modified;
                DbContext.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel DeleteEmployee(Employee model)
        {
            try
            {

                var userCrediantialDetail = DbContext.UserCredientialDetail.Where(r => r.UserCredentialId == model.LoginKey).FirstOrDefault();
                if (userCrediantialDetail != null)
                {
                    DbContext.Entry(userCrediantialDetail).State = System.Data.Entity.EntityState.Deleted;
                }
                DbContext.SaveChanges();
                var userCrediantial = DbContext.UserCredentials.Where(r => r.Id == model.LoginKey).FirstOrDefault();
                DbContext.Entry(userCrediantial).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
                var local = DbContext.Set<Employee>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Employee can not be delete. Employee is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel DeleteUser(UserWithPackageAdvanceViewModel model)
        {
            try
            {
                var localActiveStatus = DbContext.Set<UserActiveStatus>().Local.FirstOrDefault(c => c.UserCredentialId == model.Id);
                if (localActiveStatus != null)
                {
                    DbContext.Entry(localActiveStatus).State = System.Data.Entity.EntityState.Detached;
                }
                var userActiveStatus = DbContext.UserActiveStatus.Where(r => r.UserCredentialId == model.Id).FirstOrDefault();
                if (userActiveStatus != null)
                    DbContext.Entry(userActiveStatus).State = System.Data.Entity.EntityState.Deleted;

                var localWeekDayOff = DbContext.Set<UserWeekDayOff>().Local.FirstOrDefault(c => c.UserId == model.Id);
                if (localWeekDayOff != null)
                {
                    DbContext.Entry(localWeekDayOff).State = System.Data.Entity.EntityState.Detached;
                }
                var userWeekDayOff = DbContext.UserWeekDayOff.Where(r => r.UserId == model.Id).FirstOrDefault();
                if (userWeekDayOff != null)
                    DbContext.Entry(userWeekDayOff).State = System.Data.Entity.EntityState.Deleted;
                var local = DbContext.Set<UserWithPackage>().Local.FirstOrDefault(c => c.UserCrediantialId == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                var userPackage = DbContext.UserWithPackage.Where(r => r.UserCrediantialId == model.Id).FirstOrDefault();
                if (userPackage != null)
                    DbContext.Entry(userPackage).State = System.Data.Entity.EntityState.Deleted;
                var userCrediantialDetail = DbContext.UserCredientialDetail.Where(r => r.UserCredentialId == model.Id).FirstOrDefault();
                if (userCrediantialDetail != null)
                    DbContext.Entry(userCrediantialDetail).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
                var userCrediantial = DbContext.UserCredentials.Where(r => r.Id == model.Id).FirstOrDefault();
                DbContext.Entry(userCrediantial).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();

                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "User can not be delete. Employee is using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel SaveCompany(UserCredientialViewModel model, Company companyOb)
        {
            var flag = false;
            try
            {
                UserCredentials userCredentials = new UserCredentials
                {
                    Id = Guid.NewGuid().ToString(),
                    LoginID = model.LoginID,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    UserTypeId = model.UserTypeId,
                    IsActive = model.IsActive,
                    PhoneNumber = model.PhoneNumber,
                    ImagePath = model.ImagePath,
                    FileId = model.FileId,
                    FileName = model.FileName,
                    CreatedDate = DateTime.Now,
                    CreatedById = model.CreatedById,
                    UpdatedDate = null
                };
                companyOb.Id = Guid.NewGuid().ToString();
                companyOb.UserCredentialId = userCredentials.Id;
                companyOb.CreatedDate = DateTime.Now;
                UserActiveStatus userActiveStatus = new UserActiveStatus
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = model.Status,
                    UserCredentialId = userCredentials.Id,
                    CreatedDate = DateTime.Now,
                    CreatedById = model.CreatedById,

                };
                DbContext.Database.BeginTransaction();
                flag = true;
                DbContext.UserCredentials.Add(userCredentials);
                DbContext.SaveChanges();
                DbContext.Company.Add(companyOb);
                DbContext.UserActiveStatus.Add(userActiveStatus);
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel UpdateCompany(UserCredientialViewModel model, Company companyOb)
        {
            var flag = false;
            try
            {
                var exsitingUser = DbContext.UserCredentials.Where(r => r.Id == model.UserCredentialId).FirstOrDefault();
                UserCredentials userCredentials = exsitingUser;
                userCredentials.LoginID = model.LoginID;
                userCredentials.FullName = model.FullName;
                userCredentials.Email = model.Email;
                userCredentials.IsActive = model.IsActive;
                userCredentials.PhoneNumber = model.PhoneNumber;
                userCredentials.UpdatedDate = DateTime.Now;

                var existingStatus = DbContext.UserActiveStatus.Where(r => r.UserCredentialId == model.UserCredentialId).OrderByDescending(r => r.CreatedDate).First();
                UserActiveStatus userActiveStatus = existingStatus;
                userActiveStatus.Status = model.Status;

                var existingCompany = DbContext.Company.Where(r => r.Id == companyOb.Id).FirstOrDefault();
                Company company = existingCompany;

                company.IsActive = companyOb.IsActive;
                company.Name = companyOb.Name;
                company.PhoneNo = companyOb.PhoneNo;
                company.Address = companyOb.Address;
                company.ContactPersonOneName = companyOb.ContactPersonOneName;
                company.ContactPersonOneNumber = companyOb.ContactPersonOneNumber;
                company.ContactPersonOneSection = companyOb.ContactPersonOneSection;
                company.ContactPersonOneDesignation = companyOb.ContactPersonOneDesignation;
                company.ContactPersonTwoName = companyOb.ContactPersonTwoName;
                company.ContactPersonTwoNumber = companyOb.ContactPersonTwoNumber;
                company.ContactPersonTwoSection = companyOb.ContactPersonTwoSection;

                DbContext.Database.BeginTransaction();
                flag = true;
                var local = DbContext.Set<UserCredentials>().Local.FirstOrDefault(c => c.Id == userCredentials.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(userCredentials).State = System.Data.Entity.EntityState.Modified;

                var statusLocal = DbContext.Set<UserActiveStatus>().Local.FirstOrDefault(c => c.Id == userActiveStatus.Id);
                if (statusLocal != null)
                {
                    DbContext.Entry(statusLocal).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(userActiveStatus).State = System.Data.Entity.EntityState.Modified;

                var companyLocal = DbContext.Set<Company>().Local.FirstOrDefault(c => c.Id == company.Id);
                if (companyLocal != null)
                {
                    DbContext.Entry(companyLocal).State = System.Data.Entity.EntityState.Detached;
                }
                DbContext.Entry(company).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                flag = false;
                DbContext.Database.CurrentTransaction.Commit();
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag)
                    DbContext.Database.CurrentTransaction.Rollback();
            }
        }
        public ResponseModel SaveDeliverymanCustomerTag(List<DeliveryManCustomerTag> models)
        {

            try
            {
                var singleModel = models.FirstOrDefault();
                var existingList = DbContext.DeliveryManCustomerTag.Where(r => r.DeliveryManId == singleModel.DeliveryManId && r.CustomerId == singleModel.CustomerId && DbFunctions.TruncateTime(r.AssignDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                if (existingList.Count < 1)
                {
                    foreach (DeliveryManCustomerTag model in models)
                    {
                        model.Id = Guid.NewGuid().ToString();
                        model.AssignDate = DateTime.Now;
                        model.CreatedAt = DateTime.Now;
                        DbContext.DeliveryManCustomerTag.Add(model);
                        DbContext.SaveChanges();
                    }
                    return new ResponseModel { Success = true, Message = "Success" };
                }
                return new ResponseModel { Success = false, Message = DbContext.UserCredentials.Where(r => r.Id == singleModel.CustomerId).FirstOrDefault().FullName + " Has already added on list." };

            }
            catch (Exception ex)
            {
                foreach (DeliveryManCustomerTag model in models)
                {
                DbContext.Entry(model).State = System.Data.Entity.EntityState.Detached;
                }
                return new ResponseModel { Success = false, Message = ex.Message };
            }
        }
        public List<DeliveryManCustomerTagViewModel> GetDeliveryManCustomerTagList(string deliveryManId, DateTime date)
        {
            return (from dc in DbContext.DeliveryManCustomerTag
                    join c in DbContext.UserCredentials on dc.CustomerId equals c.Id into l1
                    from left1 in l1.DefaultIfEmpty()
                    join ud in DbContext.UserCredientialDetail on left1.Id equals ud.UserCredentialId into l2
                    from left2 in l2.DefaultIfEmpty()
                    where dc.DeliveryManId == deliveryManId && DbFunctions.TruncateTime(dc.AssignDate) == DbFunctions.TruncateTime(date)
                    select new DeliveryManCustomerTagViewModel { Id = dc.Id, AssignDate = dc.AssignDate, CustomerId = dc.CustomerId, CustomerName = left1.FullName, PhoneNumber = left1.PhoneNumber, Address = left2.Address }).ToList();
        }
        public ResponseModel DeleteeliveryManCustomerTag(DeliveryManCustomerTagViewModel model)
        {
            try
            {
                var local = DbContext.Set<DeliveryManCustomerTag>().Local.FirstOrDefault(c => c.Id == model.Id);
                if (local != null)
                {
                    DbContext.Entry(local).State = System.Data.Entity.EntityState.Detached;
                }
                var existingData = DbContext.DeliveryManCustomerTag.Where(r => r.Id == model.Id).FirstOrDefault();
                DbContext.Entry(existingData).State = System.Data.Entity.EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = "Can not be delete. Using on transaction." };
            }
            return new ResponseModel { Success = true, Message = "Success" };

        }
        public ResponseModel SaveUserEmployee(Employee model)
        {
            try
            {
                DbContext.Employee.Add(model);
                return new ResponseModel { Success = true, Message = "Success" };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<UserCredentials> GetExecution()
        {
            return DbContext.UserCredentials.Where(r => r.Id.ToString() == "").ToList();
        }
        public IEnumerable<UserWeekDayOff> GetUserWeekDayOffList(string userId)
        {
            return DbContext.UserWeekDayOff.Where(r => r.UserId == userId).ToList();
        }
        public List<Employee> CheckEmployeeCode(string employeeCode)
        {
            return DbContext.Employee.Where(r => r.EmployeeCode == employeeCode).ToList();
        }
        public List<object> GetCustomerWithDetail(string id)
        {
            return (from a in DbContext.UserCredentials
                    join b in DbContext.UserCredientialDetail on a.Id equals b.UserCredentialId
                    join ud in DbContext.UserCredientialDetail on a.Id equals ud.UserCredentialId into l2
                    from left2 in l2.DefaultIfEmpty()
                    join ex in DbContext.Employee on b.SalesExecutiveId equals ex.Id into sale
                    from execleft in sale.DefaultIfEmpty()
                    join dm in DbContext.Employee on b.DeliveryManId equals dm.Id into del
                    from delleft in del.DefaultIfEmpty()
                    join co in DbContext.Company on b.CompanyId equals co.Id
                    from us in DbContext.UserActiveStatus.Where(r => a.Id == r.UserCredentialId).OrderByDescending(a => a.CreatedDate).Take(1)
                    where a.Id == id
                    select new
                    {
                        a.Id,
                        a.LoginID,
                        a.FullName,
                        a.Email,
                        a.UserTypeId,
                        a.IsActive,
                        a.PhoneNumber,
                        a.ImagePath,
                        a.FileId,
                        a.FileName,
                        b.Gender,
                        b.DathOfBirth,
                        b.BloodGroup,
                        b.Weight,
                        b.Height,
                        b.Diabetes,
                        b.Allergy,
                        b.AllergyDetail,
                        b.BP,
                        b.NationalId,
                        b.CompanyId,
                        CompanyName = co.Name,
                        CompanyAddress = co.Address,
                        b.LunchStartDate,
                        b.LunchReceiveAddress,
                        b.Address,
                        b.Department,
                        b.Section,
                        b.Designation,
                        b.ReferencePersonCellNumber,
                        b.ReplacementMenu,
                        b.SpecialMenu,
                        b.WeekDayOff,
                        b.SalesExecutiveId,
                        SalesExecutiveName = execleft.Name,
                        b.DeliveryManId,
                        DeliveryManName = delleft.Name,
                        b.PaidAmount,
                        b.DueAmount,
                        b.RegistrationAmount,
                        b.AccountType,
                        us.Status
                    }).ToList<object>();
        }
        public List<UserCredientialViewModel> GetUerProfileDetail(string userId)
        {
            var commandText = @"Select  a.Id,a.LoginID,
                                    a.FullName,
                                    a.Email,
                                    a.UserTypeId,
                                    a.IsActive,
                                    a.PhoneNumber,
                                    a.ImagePath,
                                    a.FileId,
                                    a.FileName,
                                    b.Gender,
                                    b.DathOfBirth,
                                    b.BloodGroup,
                                    b.Weight,
                                    b.Height,
                                    b.Diabetes,
                                    b.Allergy,
                                    b.AllergyDetail,
                                    b.BP,
                                    b.NationalId,
                                    b.CompanyId,
                                    CompanyName = e.Name,
                                    CompanyAddress = e.Address,
                                    b.LunchStartDate,
                                    b.LunchReceiveAddress,
                                    b.Address,
                                    b.Department,
                                    b.Section,
                                    b.Designation,
                                    b.ReferencePersonCellNumber,
                                    b.ReplacementMenu,
                                    b.SpecialMenu,
                                    b.WeekDayOff,
                                    b.SalesExecutiveId,
                                    SalesExecutiveName = c.Name,
                                    b.DeliveryManId,
                                    DeliveryManName = d.Name,
                                    b.PaidAmount,
                                    b.DueAmount,
                                    b.RegistrationAmount,
                                    b.AccountType,
                                    f.Status from UserCredentials a
                                    left join UserCredientialDetail b on a.Id=b.UserCredentialId
                                    left join Employee c on b.SalesExecutiveId=c.Id
                                    left join Employee d on b.DeliveryManId=d.Id
                                    left join Company e on b.CompanyId=e.Id
                                    left join (select top 1 Id,UserCredentialId,Status from UserActiveStatus where UserCredentialId=@userId order by CreatedDate desc) f on a.Id=f.UserCredentialId
                                    where a.Id=@userId";
            var name = new SqlParameter("@userId", userId);
            var data = DbContext.Database.SqlQuery<UserCredientialViewModel>(commandText, name).ToList();
            return data;
        }
        public CateringDbContext DbContext
        {
            get { return Context as CateringDbContext; }
        }
    }
}
