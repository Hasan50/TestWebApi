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
    public class PeriodTypeApiController : ApiController
    {
        private readonly IPeriodTypeRepository _periodTypeRepository;
        public PeriodTypeApiController()
        {
            _periodTypeRepository = FtUnityMapper.GetInstance<IPeriodTypeRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPeriodTypeList()
        {
            var data = _periodTypeRepository.GetPeriodTypeList(10).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetPeriodTypeCboList()
        {
            var data = _periodTypeRepository.GetPeriodTypeCboList().ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] PeriodType model)
        {
            return Ok(_periodTypeRepository.Save(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] PeriodType model)
        {
            return Ok(_periodTypeRepository.DeletePeriodType(model));
        }
    }
}