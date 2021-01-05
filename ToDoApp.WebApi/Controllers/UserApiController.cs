using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using ToDoApp.Framework;
using System.Collections.Generic;
using System.Collections;
using System;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class UserApiController : ApiController
    {
        private readonly IUserManager _iUserManager;
        private readonly IUserCredientialRepository _userCredientialRepository;
        private readonly IPackageRepository _packageRepository;
        public UserApiController()
        {
            _iUserManager = FtUnityMapper.GetInstance<IUserManager>();
            _userCredientialRepository = FtUnityMapper.GetInstance<IUserCredientialRepository>();
            _packageRepository = FtUnityMapper.GetInstance<IPackageRepository>();
        }



        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var data = _iUserManager.GetAll(id).ToList();
            return Ok(data.Any() ? data.FirstOrDefault() : null);
        }

        [HttpGet]
        public IHttpActionResult GetUserList()
        {
            var data = _userCredientialRepository.GetUserList((int)UserType.User).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetUserAndCompanyList()
        {
            var data = _userCredientialRepository.GetUserAndCompanyList().ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetEmployeeList()
        {
            var data = _userCredientialRepository.GetEmployeeList((int)UserType.Employee).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetEmployeeWithDesignationList(int designationId)
        {
            var data = _userCredientialRepository.GetEmployeeList((int)UserType.Employee).Where(r=> r.DesignationId==designationId).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetDeliveryManList()
        {
            var data = _userCredientialRepository.GetDeliveryManList().ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPackageDeliveryManList(string date)
        {
            var deliveryManList = _userCredientialRepository.GetDeliveryManList().ToList();
            List<object> datalist = new List<object>();
            foreach (var item in deliveryManList)
            {
                var ob = new
                {
                    DeliveryMan = item.Name,
                    OrganizationList= GetOrgList(item, Convert.ToDateTime(date))
                };
               
                datalist.Add(ob);
            }
            return Ok(datalist);
        }
        [HttpGet]
        public IHttpActionResult GetEmployeePackageDeliveryManList(string id,string date)
        {
            var deliveryManList = _userCredientialRepository.GetDeliveryManListWithId(id).ToList();
            List<object> datalist = new List<object>();
            foreach (var item in deliveryManList)
            {
                var ob = new
                {
                    DeliveryMan = item.Name,
                    OrganizationList = GetOrgList(item, Convert.ToDateTime(date))
                };

                datalist.Add(ob);
            }
            return Ok(datalist);
        }
        private List<object> GetOrgList(Employee item,DateTime date)
        {
            List<object> orgObList = new List<object>();
            var orgList = _userCredientialRepository.GetDeliveryManCustomerTagList(item.Id,date);
            foreach (var orgi in orgList)
            {
                var orgOb = new
                {
                    Id=orgi.CustomerId,
                    Name = orgi.CustomerName,
                    PackageList = _packageRepository.GetPackageTergetList(orgi.CustomerId),
                    DeliveryPackageList = _packageRepository.GetPackageDeliveryList(orgi.CustomerId),
                    Remarks = ""
                };
                orgObList.Add(orgOb);
            }
            return orgObList;
        }

        [HttpPost]
        public IHttpActionResult SaveDeliverymanCustomerTag([FromBody] List<DeliveryManCustomerTag> model)
        {
            return Ok(_userCredientialRepository.SaveDeliverymanCustomerTag(model));
        }
        [HttpGet]
        public IHttpActionResult GetDeliveryManCustomerTagList(string deliveryManId,string date)
        {
            var data = _userCredientialRepository.GetDeliveryManCustomerTagList(deliveryManId,Convert.ToDateTime(date)).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetCustomerWithDetail(string id)
        {
            var userData = _userCredientialRepository.GetUerProfileDetail(id);
            var userPackage = _packageRepository.GetUserWithPackageDetailList(id);
            var userWeekDayOffList = _userCredientialRepository.GetUserWeekDayOffList(id);
            return Ok(new { userData,userPackage,userWeekDayOffList});
        }
        [HttpPost]
        public IHttpActionResult DeleteDeliveryManCustomerTag([FromBody] DeliveryManCustomerTagViewModel model)
        {
            return Ok(_userCredientialRepository.DeleteeliveryManCustomerTag(model));
        }
        [HttpGet]
        public IHttpActionResult GetUerProfileDetail(string id)
        {
            var userData = _userCredientialRepository.GetUerProfileDetail(id);
            var userWeekDayOffList = _userCredientialRepository.GetUserWeekDayOffList(id);
            return Ok(new { userData, userWeekDayOffList });
        }
    }
}
