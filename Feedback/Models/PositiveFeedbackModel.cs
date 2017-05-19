using System;

namespace Feedback.Models
{
    public class PositiveFeedbackModel
    {
        public int ClientID { get; set; }
        public int ReporterID { get; set; }
        public DateTime Date { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public int TimeAMPM { get; set; }
        public string Comment { get; set; }
    }
}