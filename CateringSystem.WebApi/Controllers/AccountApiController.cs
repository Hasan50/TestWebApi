using CateringSystem.Business;
using CateringSystem.Business.Interfaces;
using CateringSystem.Business.Models;
using CateringSystem.Framework;
using CateringSystem.WebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace CateringSystem.WebApi.Controllers
{
    [JwtAuthentication]
    public class AccountApiController : ApiController
    {
        private readonly RandomGenerator _randomGenerator;
        private readonly IUserManager _iUserManager;
        private readonly IUserCredientialRepository _iuserCredientialRepository;
        public AccountApiController()
        {
            _randomGenerator = new RandomGenerator();
            _iUserManager = FtUnityMapper.GetInstance<IUserManager>();
            _iuserCredientialRepository = FtUnityMapper.GetInstance<IUserCredientialRepository>();
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Register()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["customerob"];
            var advance = httpRequest.Params["advanceOb"];
            var dayShiftPackge = httpRequest.Params["dayShiftPackge"];
            var weekdayoff = httpRequest.Params["weekdayoff"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var advanceOb = JsonConvert.DeserializeObject<List<UserWithPackage>>(advance);
            var weekDayList = JsonConvert.DeserializeObject<List<UserWeekDayOff>>(weekdayoff);
            modelOb.Password = CryptographyHelper.CreateMD5Hash(modelOb.Password);

            ResponseModel result = _iuserCredientialRepository.SaveUser(modelOb, advanceOb, weekDayList);
             result= SaveImage(httpRequest, modelOb,result);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult UpdateUser()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["customerob"];
            var advance = httpRequest.Params["advanceOb"];
            var dayShiftPackge = httpRequest.Params["dayShiftPackge"];
            var weekdayoff = httpRequest.Params["weekdayoff"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var advanceOb = JsonConvert.DeserializeObject<List<UserWithPackage>>(advance);
            var weekDayList = JsonConvert.DeserializeObject<List<UserWeekDayOff>>(weekdayoff);
            ResponseModel result = _iuserCredientialRepository.UpdateUser(modelOb, advanceOb, weekDayList);
            result = SaveImage(httpRequest, modelOb, result);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult DeleteUser([FromBody] UserWithPackageAdvanceViewModel model)
        {
            return Ok(_iuserCredientialRepository.DeleteUser(model));
        }
        [HttpPost]
        public IHttpActionResult EditProfile()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["customerob"];
            var weekdayoff = httpRequest.Params["weekdayoff"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var weekDayList = JsonConvert.DeserializeObject<List<UserWeekDayOff>>(weekdayoff);
            ResponseModel result = _iuserCredientialRepository.EditProfile(modelOb, weekDayList);
            result = SaveImage(httpRequest, modelOb, result);
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult EmployeeRegister()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["customerob"];
            var advance = httpRequest.Params["employeeOb"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var employeeOb = JsonConvert.DeserializeObject<Employee>(advance);
            modelOb.Password = CryptographyHelper.CreateMD5Hash(modelOb.Password);
            return Ok(_iuserCredientialRepository.SaveEmployee(modelOb, employeeOb));
        }
        [HttpPost]
        public IHttpActionResult EmployeeUpdate()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["customerob"];
            var advance = httpRequest.Params["employeeOb"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var employeeOb = JsonConvert.DeserializeObject<Employee>(advance);
            return Ok(_iuserCredientialRepository.UpdateEmployee(modelOb, employeeOb));
        }
        [HttpPost]
        public IHttpActionResult DeleteEmployee([FromBody] Employee model)
        {
            return Ok(_iuserCredientialRepository.DeleteEmployee(model));
        }
        [HttpPost]
        public IHttpActionResult CompanyRegister()
        {
            var httpRequest = HttpContext.Current.Request;
            var model = httpRequest.Params["registrationob"];
            var company = httpRequest.Params["companyOb"];
            var modelOb = JsonConvert.DeserializeObject<UserCredientialViewModel>(model);
            var companyOb = JsonConvert.DeserializeObject<Company>(company);
            if (companyOb.Id==null)
            {
                modelOb.Password = CryptographyHelper.CreateMD5Hash(modelOb.Password);
                return Ok(_iuserCredientialRepository.SaveCompany(modelOb, companyOb));
            }
            return Ok(_iuserCredientialRepository.UpdateCompany(modelOb, companyOb));
        }
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult GenerateToken([FromBody] LoginModel model)
        {
            var password = CryptographyHelper.CreateMD5Hash(model.Password);
            var user = _iUserManager.Get(model.LoginID, password);
            if (user == null)
            {
                return Ok(new { Success = false,Message="Username or Password is wrong." });
            }
            var token = TokenManager.GenerateToken(model.LoginID);
            return Ok(new { Success = true, Token = TokenManager.GenerateToken(model.LoginID), UserKey = user.Id,Details= new {
                user.UserTypeId,
                user.FullName,
                user.LoginID,
                user.PhoneNumber,
                user.Email
            } });
        }

        [HttpPost]
        public IHttpActionResult ChangePassword([FromBody] LocalPasswordModel model)
        {
            var response = _iUserManager.ChangePassword(model.UserName, CryptographyHelper.CreateMD5Hash(model.ConfirmPassword));
            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult GetUserDetails(string userKey)
        {
            var response = _iUserManager.GetUserDetails(userKey);
            return Ok(response);
        }
        [HttpGet]
        public IHttpActionResult GetExecution()
        {
            var response = _iuserCredientialRepository.GetExecution();
            return Ok(response);
        }
        [HttpGet]
        public IHttpActionResult GetProjectCode()
        {
            var data = _randomGenerator.RandomString(8, false);
            var s = _iuserCredientialRepository.CheckEmployeeCode(data);
            if (s.Count == 0)
            {
                return Ok(data);
            }
            else
            {
                data = _randomGenerator.RandomString(8, false);
            }
            return Ok(data);
        }
        private ResponseModel SaveImage(HttpRequest httpRequest, UserCredientialViewModel modelOb, ResponseModel result)
        {
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0)
                {

                    int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {

                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                    }
                    else if (postedFile.ContentLength > MaxContentLength)
                    {

                        var message = string.Format("Please Upload a file upto 1 mb.");
                    }
                    else
                    {
                        var fileExtension = Path.GetExtension(postedFile.FileName);
                        var fileId = modelOb.FileId;
                        var picPath = HttpContext.Current.Server.MapPath(@"~\UploadFiles\");
                        if (!Directory.Exists(picPath))
                        {
                            Directory.CreateDirectory(picPath);
                        }
                        if (!CommonHelper.IsValidFile(fileExtension))
                            result.Message = "File Formate is not valid";
                        var filePath = Path.Combine(picPath, fileId + fileExtension);
                        if (modelOb.Id != null)
                        {
                            if (System.IO.File.Exists(Path.Combine(picPath, modelOb.FileId + Path.GetExtension(modelOb.FileName))))
                            {
                                System.IO.File.Delete(Path.Combine(picPath, modelOb.FileId + Path.GetExtension(modelOb.FileName)));
                            }
                        }
                        postedFile.SaveAs(filePath);

                    }
                }
            }
            return result;
        }
    }
}
