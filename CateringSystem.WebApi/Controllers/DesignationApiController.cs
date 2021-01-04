﻿using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using CateringSystem.Common.Models;
using CateringSystem.Framework;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class DesignationApiController : ApiController
    {
        private readonly IDesignationRepository _designationRepository;
        public DesignationApiController()
        {
            _designationRepository = FtUnityMapper.GetInstance<IDesignationRepository>();
        }
        //[HttpGet]
        //public IHttpActionResult GetDesignationList()
        //{
        //    var data = _designationRepository.GetDesignationList().ToList();
        //    return Ok(data);
        //}
        [HttpGet]
        public IHttpActionResult GetDesignationList()
        {
            var list = Enum.GetValues(typeof(DesignationEnum)).Cast<DesignationEnum>().Select(v => new NameIdPairModel
            {
                Name = EnumUtility.GetDescriptionFromEnumValue(v),
                Id = (int)v
            }).ToList();
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] DesignationModel model)
        {
            return Ok(_designationRepository.SaveDesignation(model));
        }
    }
}