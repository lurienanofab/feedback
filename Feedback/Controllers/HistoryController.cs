using Feedback.Models;
using LNF.Cache;
using System;
using System.Web.Mvc;

namespace Feedback.Controllers
{
    public class HistoryController : Controller
    {
        [HttpGet, Route("history")]
        public ActionResult Index()
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 1;
            ViewBag.DisplayName = CacheManager.Current.CurrentUser.DisplayName;

            HistoryModel model = new HistoryModel()
            {
                ClientID = CacheManager.Current.CurrentUser.ClientID,
                FeedbackStatus = "All",
                StartDate = DateTime.Now.Date.AddMonths(-6),
                EndDate = DateTime.Now.Date.AddDays(1),
                DateRangePreset = null
            };

            return View(model);
        }

        [HttpPost, Route("history")]
        public ActionResult Index(HistoryModel model)
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 1;
            ViewBag.DisplayName = CacheManager.Current.CurrentUser.DisplayName;

            if (model.RetrieveData)
                model.DateRangePreset = null;

            return View(model);
        }

        [HttpGet, Route("history/report")]
        public ActionResult AggregateReport()
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 2;
            ViewBag.DisplayName = CacheManager.Current.CurrentUser.DisplayName;
            return View();
        }

        [HttpPost, Route("history/report")]
        public ActionResult AggregateReport(HistoryModel model)
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 2;
            ViewBag.DisplayName = CacheManager.Current.CurrentUser.DisplayName;
            return View(model);
        }

        [Route("history/dispute")]
        public ActionResult Dispute()
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 3;
            return View();
        }

        [Route("history/negative/detail/{issueId}")]
        public ActionResult NegativeDetail(int issueId)
        {
            ViewBag.Tab = "history";
            ViewBag.Submenu = 1;
            var issue = ReportUtility.GetNegativeIssue(issueId);
            return View(issue);
        }
    }
}