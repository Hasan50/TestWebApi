using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;


namespace CateringSystem.WebApi.Controllers
{

    [JwtAuthentication]
    public class FileInformationApiController : ApiController
    {
        private readonly IFileInformation _iFileInformation;
        public FileInformationApiController()
        {
            _iFileInformation = FtUnityMapper.GetInstance<IFileInformation>();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] FileInformation model)
        {
            return Ok(_iFileInformation.Save(model));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var data = _iFileInformation.GetAll(id).ToList();
            return Ok(data.Any()?data.FirstOrDefault():null);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var data = _iFileInformation.GetAll(null).ToList();
            return Ok(data);
        }
    }
}
