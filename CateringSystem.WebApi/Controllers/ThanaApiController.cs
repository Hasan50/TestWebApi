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
    public class ThanaApiController : ApiController
    {
        private readonly IThanaRepository _ThanaRepository;
        public ThanaApiController()
        {
            _ThanaRepository = FtUnityMapper.GetInstance<IThanaRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetThanaList()
        {
            var data = _ThanaRepository.GetThanaList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] Thana model)
        {
            return Ok(_ThanaRepository.SaveThana(model));
        }
    }
}