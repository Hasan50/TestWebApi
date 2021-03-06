﻿using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using System.Web.Http;
using System.Linq;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class FileReportApiController : ApiController
    {
        private readonly IFileInformation _iFileInformation;
        public FileReportApiController()
        {
            _iFileInformation = FtUnityMapper.GetInstance<IFileInformation>();
        }

        [HttpGet]
        public IHttpActionResult GetCurrentStatus()
        {
            var data = _iFileInformation.GetAll(null).Where(x=>x.IsMoving.HasValue && x.IsMoving.Value).ToList();
            return Ok(data);
        }
    }
}
