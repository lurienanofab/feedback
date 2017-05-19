using System.Configuration;

namespace Feedback.Models
{
    public static class FeedbackUtility
    {
        public static string DefaultTab()
        {
            return ConfigurationManager.AppSettings["DefaultTab"];
        }

        public static SidebarModel GetSidebar()
        {
            return new SidebarModel()
            {
                Message = ConfigurationManager.AppSettings["Sidebar.Message"],
                DirectorName = ConfigurationManager.AppSettings["Sidebar.DirectorName"],
                DirectorEmail = ConfigurationManager.AppSettings["Sidebar.DirectorEmail"]
            };
        }

        public static string GetExitUrl()
        {
            return ConfigurationManager.AppSettings["ExitApplicationRedirect"];
        }
    }
}