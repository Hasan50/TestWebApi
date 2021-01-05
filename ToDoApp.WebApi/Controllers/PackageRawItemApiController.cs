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
    public class PackageRawItemApiController : ApiController
    {
        private readonly IRawItemRepository _rawItemRepository;
        public PackageRawItemApiController()
        {
            _rawItemRepository = FtUnityMapper.GetInstance<IRawItemRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPackageRawItemList(string packageId)
        {
            var data = _rawItemRepository.GetPackageRawItemList(10,packageId).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] List<PackageRawItem> models)
        {
            return Ok(_rawItemRepository.SavePackageRawItem(models));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] PackageRawItem model)
        {
            return Ok(_rawItemRepository.DeletePackageRawItem(model));
        }
    }
}