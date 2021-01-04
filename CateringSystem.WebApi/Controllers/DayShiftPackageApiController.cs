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
    public class DayShiftPackageApiController : ApiController
    {
        private readonly IDayShiftRepository _dayShiftRepository;
        public DayShiftPackageApiController()
        {
            _dayShiftRepository = FtUnityMapper.GetInstance<IDayShiftRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetDayShiftPackageList(int dayShiftId)
        {
            var data = _dayShiftRepository.GetDayShiftPackageList(10, dayShiftId).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] List<DayShiftWithPackage> models)
        {
            return Ok(_dayShiftRepository.SaveDayShiftPackage(models));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] DayShiftWithPackage model)
        {
            return Ok(_dayShiftRepository.DeleteDayShiftPackage(model));
        }
    }
}