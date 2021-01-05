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
    public class CompanyApiController : ApiController
    {
        private readonly ICompanyRepository _CompanyRepository;
        public CompanyApiController()
        {
            _CompanyRepository = FtUnityMapper.GetInstance<ICompanyRepository>();
        }
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCompanyList()
        {
            var data = _CompanyRepository.GetCompanyList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save([FromBody] Company model)
        {
            return Ok(_CompanyRepository.SaveCompany(model));
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] Company model)
        {
            return Ok(_CompanyRepository.DeleteCompany(model));
        }
    }
}