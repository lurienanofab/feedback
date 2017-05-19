using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feedback.Models
{
    public class PositiveIssueItem
    {
        public int ClientID { get; set; }
        public string DisplayName { get; set; }
        public int ReporterID {get;set;}
        public string ReporterDisplayName { get; set; }
        public string Comment { get; set; }
        public string FeedbackStatus { get; set; }
        public DateTime IssueDateTime { get; set; }

    }
}