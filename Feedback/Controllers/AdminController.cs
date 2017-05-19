using System.Web.Mvc;

namespace Feedback.Controllers
{
    public class AdminController : Controller
    {
        [Route("admin")]
        public ActionResult Index()
        {
            ViewBag.Tab = "admin";
            return View();
        }
    }
}