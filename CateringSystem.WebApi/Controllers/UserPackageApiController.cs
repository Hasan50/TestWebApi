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
