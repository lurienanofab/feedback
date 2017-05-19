namespace Feedback.Models
{
    public class NegativeFeedbackModel : PositiveFeedbackModel
    {
        public int[] SelectedRules { get; set; }
        public int ResourceID { get; set; }
    }
}