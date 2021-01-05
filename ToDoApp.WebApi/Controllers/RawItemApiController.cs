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
    public class RawItemApiController : ApiController
    {
        private readonly IRawItemRepository _rawItemRepository;
        public RawItemApiController()
        {
            _rawItemRepository = FtUnityMapper.GetInstance<IRawItemRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetRawItemList()
        {
            var data = _rawItemRepository.GetRawItemList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] RawItem model)
        {
            return Ok(_rawItemRepository.SaveRawItem(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] RawItem model)
        {
            return Ok(_rawItemRepository.DeleteRawItem(model));
        }
    }
}