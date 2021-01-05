using System.Web.Mvc;

namespace ToDoApp.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
    }
}