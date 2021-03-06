﻿using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class SectorApiController : ApiController
    {
        private readonly ISectorRepository _SectorRepository;
        public SectorApiController()
        {
            _SectorRepository = FtUnityMapper.GetInstance<ISectorRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetSectorList()
        {
            var data = _SectorRepository.GetSectorList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] Sector model)
        {
            return Ok(_SectorRepository.SaveSector(model));
        }
    }
}