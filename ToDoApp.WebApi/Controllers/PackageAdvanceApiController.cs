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
    public class PackageAdvanceApiController : ApiController
    {
        private readonly IPackageRepository _packageRepository;
        public PackageAdvanceApiController()
        {
            _packageRepository = FtUnityMapper.GetInstance<IPackageRepository>();
        }

        [HttpGet]
        public IHttpActionResult GetAdvancePackageList()
        {
            var data = _packageRepository.GetPackageAdvanceList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult SavePackageAdvance([FromBody] PackageAdvance model)
        {
            return Ok(_packageRepository.SavePackageAdvance(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] PackageAdvance model)
        {
            return Ok(_packageRepository.DeletePackageAdvance(model));
        }
    }
}
