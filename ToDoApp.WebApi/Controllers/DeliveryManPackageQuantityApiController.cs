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
    public class DeliveryManPackageQuantityApiController : ApiController
    {
        private readonly IDeliveryManPackageQuantityRepository _repository;
        public DeliveryManPackageQuantityApiController()
        {
            _repository = FtUnityMapper.GetInstance<IDeliveryManPackageQuantityRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetDeliveryManPackageList(string deliveryManId, string date)
        {
            var data = _repository.GetDeliveryManPackageList(10, deliveryManId, Convert.ToDateTime(date)).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] List<DeliveryManPackageQuantity> models)
        {
            return Ok(_repository.SavePackageQuantity(models));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] DeliveryManPackageQuantity model)
        {
            return Ok(_repository.Delete(model));
        }
    }
}