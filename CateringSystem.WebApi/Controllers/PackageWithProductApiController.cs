using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class PackageWithProductApiController : ApiController
    {
        private readonly IPackageProductRepository _packageProductRepository;
        public PackageWithProductApiController()
        {
            _packageProductRepository = FtUnityMapper.GetInstance<IPackageProductRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPackageWithProductList(string packageId,DateTime date)
        {
            var data = _packageProductRepository.GetPackageWithProductList(10,packageId,date).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["master"];
            var models = httpRequest.Params["detailList"];
            //var format = "dd/MM/yyyy"; // your datetime format
            //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
            var modelOb = JsonConvert.DeserializeObject<PackageWithProductMaster>(model);
            var modelList = JsonConvert.DeserializeObject<List<PackageWithProductMasterDetail>>(models);
            return Ok(_packageProductRepository.SavePackageWithProduct(modelList, modelOb));
        }
    }
}