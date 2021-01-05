using ToDoApp.Business;
using ToDoApp.Business.Interfaces;
using ToDoApp.Business.Models;
using System.Web.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using ToDoApp.Framework;

namespace ToDoApp.WebApi.Controllers
{
    [JwtAuthentication]
    public class PackageProductApiController : ApiController
    {
        private readonly IPackageProductRepository _packageProductRepository;

        public PackageProductApiController()
        {
            _packageProductRepository = FtUnityMapper.GetInstance<IPackageProductRepository>();
        }
        [HttpGet]
        public IHttpActionResult GetPackageProductList()
        {
            var data = _packageProductRepository.GetPackageProductList(10).ToList();
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Save()
        {
            var httpRequest = HttpContext.Current.Request;
            var postOb = httpRequest["postOb"];
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            PackageProduct post = JsonConvert.DeserializeObject<PackageProduct>(postOb, settings);
            ResponseModel result= _packageProductRepository.SavePackageProduct(post);
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
                        var fileId = post.FileId;
                        var picPath = HttpContext.Current.Server.MapPath(@"~\UploadFiles\");
                        if (!Directory.Exists(picPath))
                        {
                            Directory.CreateDirectory(picPath);
                        }
                        if (!CommonHelper.IsValidFile(fileExtension))
                            result.Message = "File Formate is not valid";
                        var filePath = Path.Combine(picPath, fileId + fileExtension);
                        if (post.Id != null)
                        {
                            if (System.IO.File.Exists(Path.Combine(picPath, post.FileId + Path.GetExtension(post.FileName))))
                            {
                                System.IO.File.Delete(Path.Combine(picPath, post.FileId + Path.GetExtension(post.FileName)));
                            }
                        }
                        postedFile.SaveAs(filePath);

                    }
                }
            }
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult Delete([FromBody] PackageProduct model)
        {
            return Ok(_packageProductRepository.DeletePackageProduct(model));
        }
    }
}