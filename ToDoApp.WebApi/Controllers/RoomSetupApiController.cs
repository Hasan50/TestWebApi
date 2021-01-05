using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;


namespace ToDoApp.WebApi.Controllers
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
