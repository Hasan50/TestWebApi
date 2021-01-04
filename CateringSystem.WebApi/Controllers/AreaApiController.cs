﻿using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class AreaApiController : ApiController
    {
        private readonly IAreaRepository _AreaRepository;
        public AreaApiController()
        {
            _AreaRepository = FtUnityMapper.GetInstance<IAreaRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetAreaList()
        {
            var data = _AreaRepository.GetAreaList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] Area model)
        {
            return Ok(_AreaRepository.SaveArea(model));
        }
    }
}