using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;


namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class RoomSetupApiController : ApiController
    {
        private readonly IRoomSetup _roomSetup;
        public RoomSetupApiController()
        {
            _roomSetup = FtUnityMapper.GetInstance<IRoomSetup>();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] RoomSetupModel model)
        {
            return Ok(_roomSetup.Save(model));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var data = _roomSetup.GetAll(id).ToList();
            return Ok(data.Any() ? data.FirstOrDefault() : null);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var data = _roomSetup.GetAll(null).ToList();
            return Ok(data);
        }
    }
}
