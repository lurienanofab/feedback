using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feedback.Models
{
    public class ReportItem
    {
        public int IssueID { get; set; }
        public DateTime IssueTime { get; set; }
        public string Comment { get; set; }
    }
}