using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class CustomerInvoiceApiController : ApiController
    {
        private readonly ICustomerInvoiceRepository _customerInvoiceRepository;
        public CustomerInvoiceApiController()
        {
            _customerInvoiceRepository = FtUnityMapper.GetInstance<ICustomerInvoiceRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetInvoiceList(string userId)
        {
            var data = _customerInvoiceRepository.GetInvoiceList(userId).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetInvoiceDetail(string invoiceId)
        {
            var data = _customerInvoiceRepository.GetInvoiceDetail(invoiceId).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save()
        {
            var httpRequest = HttpContext.Current.Request;
            var customerInvoiceob = httpRequest.Params["customerInvoiceob"];
            var invoiceDetailList = httpRequest.Params["invoiceDetailList"];
            var modelOb = JsonConvert.DeserializeObject<CustomerInvoice>(customerInvoiceob);
            var modelList = JsonConvert.DeserializeObject<List<CustomerInvoiceDetail>>(invoiceDetailList);
            if (modelOb.Id==null)
            {
                return Ok(_customerInvoiceRepository.SaveInvoice(modelOb, modelList));
            }
            else
            {
                return Ok(_customerInvoiceRepository.UpdateInvoice(modelOb, modelList));
            }
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] CustomerInvoiceViewModel model)
        {
            return Ok(_customerInvoiceRepository.DeleteInvoice(model));
        }
        [HttpPost]
        public IHttpActionResult DeleteInvoiceDetail([FromBody] CustomerInvoiceDetail model)
        {
            return Ok(_customerInvoiceRepository.DeleteInvoiceDetail(model));
        }
    }
}