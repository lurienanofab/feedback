using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LNF.Repository.Feedback;
using LNF.Repository;
using LNF.CommonTools;

namespace Feedback.Models
{
    public static class ReportUtility
    {
        public static IEnumerable<ReportItem> GetNegativeReport(int clientId, DateTime sd, DateTime ed, string status)
        {
            IQueryable<NegativeIssue> query;

            if (status == "All")
                query = DA.Current.Query<NegativeIssue>();
            else
                query = DA.Current.Query<NegativeIssue>().Where(x => x.Status == status);

            query = query.Where(x => x.ClientID == clientId && x.Time >= sd && x.Time < ed);

            return query.Select(x => new ReportItem() { IssueID = x.IssueID, IssueTime = x.Time, Comment = x.Comment });
        }

        public static NegativeIssue GetNegativeIssue(int issueId)
        {
            return DA.Current.Single<NegativeIssue>(issueId);
        }
    }
}