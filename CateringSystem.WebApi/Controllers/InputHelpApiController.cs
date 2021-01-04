using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using CateringSystem.Framework;
using CateringSystem.Common.Models;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class InputHelpApiController : ApiController
    {
        private readonly IInputHelp _inputHelp;
        public InputHelpApiController()
        {
            _inputHelp = FtUnityMapper.GetInstance<IInputHelp>();
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] InputHelpModel model)
        {
            return Ok(_inputHelp.Save(model));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var data = _inputHelp.GetAll(id).ToList();
            return Ok(data.Any() ? data.FirstOrDefault() : null);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var data = _inputHelp.GetAll(null).ToList();
            return Ok(data);
        }
        [HttpGet]
        public IHttpActionResult GetInputTypeAll()
        {
            var data = Enum.GetValues(typeof(InputHelpType)).Cast<InputHelpType>().Select(c => new TextValuePairModel { Value = (int)c, Text = EnumUtility.GetDescriptionFromEnumValue(c) });
            return Ok(data);
        }
    }
}
