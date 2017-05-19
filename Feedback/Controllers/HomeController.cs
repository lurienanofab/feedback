using Feedback.Models;
using LNF.Cache;
using System;
using System.Web.Mvc;

namespace Feedback.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            var tab = FeedbackUtility.DefaultTab();
            switch (tab)
            {
                case "positive":
                    return RedirectToAction("Positive");
                case "negative":
                    return RedirectToAction("Negative");
                default:
                    throw new InvalidOperationException(string.Format("Invalid default tab: {0}", tab));
            }
        }

        [HttpGet, Route("positive")]
        public ActionResult Positive()
        {
            ViewBag.Tab = "positive";
            ViewBag.ShowAcknowledgment = false;
            ViewBag.ReporterID = CacheManager.Current.CurrentUser.ClientID;
            return View();
        }

        [HttpPost, Route("positive")]
        public ActionResult Positive(PositiveFeedbackModel model)
        {
            ViewBag.Tab = "positive";
            ViewBag.ShowAcknowledgment = true;
            return View();
        }

        [HttpGet, Route("negative")]
        public ActionResult Negative()
        {
            ViewBag.Tab = "negative";
            ViewBag.ShowAcknowledgment = false;
            ViewBag.ReporterID = CacheManager.Current.CurrentUser.ClientID;
            return View();
        }

        [HttpPost, Route("negative")]
        public ActionResult Negative(NegativeFeedbackModel model)
        {
            ViewBag.Tab = "negative";
            ViewBag.ShowAcknowledgment = true;
            return View();
        }

        [Route("exit")]
        public ActionResult Exit()
        {
            string exitUrl = FeedbackUtility.GetExitUrl();
            return Redirect(exitUrl);
        }
    }
}