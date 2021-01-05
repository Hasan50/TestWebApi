using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class AutoExicuteApiController : ApiController
    {
        private readonly IPackageRepository _packageRepository;
        public AutoExicuteApiController()
        {
            _packageRepository = FtUnityMapper.GetInstance<IPackageRepository>();
        }
        [HttpGet]
        [AllowAnonymous]
        public string GetDailyDeliveryPackageList()
        {
            _packageRepository.SaveAutoExecPackageDeliveryTerget();
            return "Success";
        }
     
    }
}