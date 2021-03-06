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
    public class BloodGroupApiController : ApiController
    {
        private readonly IBloodGroupRepository _BloodGroupRepository;
        public BloodGroupApiController()
        {
            _BloodGroupRepository = FtUnityMapper.GetInstance<IBloodGroupRepository>();
        }
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetBloodGroupList()
        {
            var data = _BloodGroupRepository.GetBloodGroupList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] BloodGroup model)
        {
            return Ok(_BloodGroupRepository.SaveBloodGroup(model));
        }
    }
}