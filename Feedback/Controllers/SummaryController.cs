using System.Web.Mvc;

namespace Feedback.Controllers
{
    public class SummaryController : Controller
    {
        [Route("summary")]
        public ActionResult Index()
        {
            ViewBag.Tab = "summary";
            return View();
        }
    }
}