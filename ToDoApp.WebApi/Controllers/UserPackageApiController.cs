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
    public class UserPackageApiController : ApiController
    {
        private readonly IPackageRepository _packageRepository;
        public UserPackageApiController()
        {
            _packageRepository = FtUnityMapper.GetInstance<IPackageRepository>();
        }

 
        [HttpPost]
        public IHttpActionResult Save([FromBody] Package model)
        {
            return Ok(_packageRepository.SavePackage(model));
        }


    }
}
