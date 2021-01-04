using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class DayShiftApiController : ApiController
    {
        private readonly IDayShiftRepository _dayShiftRepository;
        public DayShiftApiController()
        {
            _dayShiftRepository = FtUnityMapper.GetInstance<IDayShiftRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetDayShiftCboList()
        {
            var data = _dayShiftRepository.GetDayShiftCboList().ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetDayShiftList()
        {
            var data = _dayShiftRepository.GetDayShiftList().ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] DayShift model)
        {
            return Ok(_dayShiftRepository.Save(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] DayShift model)
        {
            return Ok(_dayShiftRepository.DeleteShift(model));
        }
    }
}