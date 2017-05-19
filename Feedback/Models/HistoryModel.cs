using System;

namespace Feedback.Models
{
    public class HistoryModel
    {
        public int ClientID { get; set; }
        public string FeedbackStatus { get; set; }
        public string DateRangePreset { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool RetrieveData { get; set; }

        public DateTime GetStartDate()
        {
            if (RetrieveData)
                return StartDate.Date;

            switch (DateRangePreset)
            {
                case "7dy":
                    return DateTime.Now.Date.AddDays(-7);
                case "1mo":
                    return DateTime.Now.Date.AddMonths(-1);
                case "3mo":
                    return DateTime.Now.Date.AddMonths(-3);
                case "6mo":
                    return DateTime.Now.Date.AddMonths(-6);
                case "9mo":
                    return DateTime.Now.Date.AddMonths(-9);
                case "1yr":
                    return DateTime.Now.Date.AddYears(-1);
                case "2yr":
                    return DateTime.Now.Date.AddYears(-2);
                default:
                    return StartDate.Date;
            }
        }

        public DateTime GetEndDate()
        {
            if (RetrieveData)
                return EndDate.Date;

            switch (DateRangePreset)
            {
                case "7dy":
                case "1mo":
                case "3mo":
                case "6mo":
                case "9mo":
                case "1yr":
                case "2yr":
                    return DateTime.Now.Date.AddDays(1);
                default:
                    return EndDate.Date;
            }
        }
    }
}