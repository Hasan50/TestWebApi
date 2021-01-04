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
    public class DeliveryStatusApiController : ApiController
    {
        private readonly IDeliveryManPackageQuantityRepository _repository;
        public DeliveryStatusApiController()
        {
            _repository = FtUnityMapper.GetInstance<IDeliveryManPackageQuantityRepository>();
        }

        [HttpGet]
        public IHttpActionResult GetDeliveryStatusList(string deliveryManId, string date)
        {
            var data = _repository.GetDeliveryStatusList(deliveryManId, Convert.ToDateTime(date)).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetExtraDeliveryStatusList(string deliveryManId, string date)
        {
            var data = _repository.GetExtraDeliveryStatusList(deliveryManId, Convert.ToDateTime(date)).ToList();
            return Ok(data);
        }
    }
}