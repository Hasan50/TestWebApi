using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class FileMovementApiController : ApiController
    {
        private readonly IFileMovement _iFileMovement;
        public FileMovementApiController()
        {
            _iFileMovement = FtUnityMapper.GetInstance<IFileMovement>();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] FileMovementModel model)
        {
            return Ok(_iFileMovement.Save(model));
        }

        [HttpGet]
        public IHttpActionResult GetMovements(int fileId)
        {
            return Ok(_iFileMovement.GetMovements(fileId).OrderByDescending(x=>x.ActionAt));
        }
    }
}
