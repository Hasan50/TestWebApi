using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class CustomerPaymentApiController : ApiController
    {
        private readonly ICustomerPaymentRepository _customerPaymentRepository;
        public CustomerPaymentApiController()
        {
            _customerPaymentRepository = FtUnityMapper.GetInstance<ICustomerPaymentRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPaymentList(string userId)
        {
            var data = _customerPaymentRepository.GetPaymentList(userId).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save(CustomerPayment model)
        {
            return Ok(_customerPaymentRepository.SavePayment(model));
        }
        [HttpPost]
        public IHttpActionResult DeleteCustomerPayment([FromBody] CustomerPayment model)
        {
            return Ok(_customerPaymentRepository.DeleteCustomerPayment(model));
        }
    }
}