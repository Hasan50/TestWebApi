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
    public class MasterSettingApiController : ApiController
    {
        private readonly IMasterSettingRepository _MasterSettingRepository;
        public MasterSettingApiController()
        {
            _MasterSettingRepository = FtUnityMapper.GetInstance<IMasterSettingRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetMasterSetting()
        {
            var data = _MasterSettingRepository.GetMasterSetting();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] MasterSetting model)
        {
            return Ok(_MasterSettingRepository.SaveMasterSetting(model));
        }
    }
}