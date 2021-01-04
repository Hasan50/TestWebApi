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
    public class UnitOfMeasuremetnApiController : ApiController
    {
        private readonly IUnitOfMeasurementRepository _UnitOfMeasurementRepository;
        public UnitOfMeasuremetnApiController()
        {
            _UnitOfMeasurementRepository = FtUnityMapper.GetInstance<IUnitOfMeasurementRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetUnitOfMeasurementList()
        {
            var data = _UnitOfMeasurementRepository.GetUnitOfMeasurementList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] UnitOfMeasurement model)
        {
            return Ok(_UnitOfMeasurementRepository.SaveUnitOfMeasurement(model));
        }
    }
}