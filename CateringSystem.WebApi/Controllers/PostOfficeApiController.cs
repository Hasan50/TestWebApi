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
    public class PostOfficeApiController : ApiController
    {
        private readonly IPostOfficeRepository _PostOfficeRepository;
        public PostOfficeApiController()
        {
            _PostOfficeRepository = FtUnityMapper.GetInstance<IPostOfficeRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPostOfficeList()
        {
            var data = _PostOfficeRepository.GetPostOfficeList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] PostOffice model)
        {
            return Ok(_PostOfficeRepository.SavePostOffice(model));
        }
    }
}