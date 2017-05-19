using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LNF.Repository;
using LNF.Repository.Data;
using LNF.Models.Data;
using LNF.Data;
using LNF;

namespace Feedback.Models
{
    public class UserModel
    {
        private static Dictionary<string, string> roomMap = new Dictionary<string, string>()
        {
            { "Clean Room", "Clean Room" },
            { "Wet Chemistry", "ROBIN" }
        };

        public int ClientID { get; set; }
        public string DisplayName { get; set; }
        public string AreaName { get; set; }

        public static IEnumerable<UserModel> GetAllUsers(DateTime sd, DateTime ed)
        {
            var result = DA.Current.Query<Client>()
                .Where(x => ((x.Privs & ClientPrivilege.Staff) > 0 || (x.Privs & ClientPrivilege.LabUser) > 0) && (x.Privs & ClientPrivilege.PhysicalAccess) > 0)
                .FindActive(x => x.ClientID, sd, ed)
                .OrderBy(x => x.LName).ThenBy(x => x.FName)
                .Select(x => new UserModel() { ClientID = x.ClientID, DisplayName = Client.GetDisplayName(x.LName, x.FName), AreaName = "All Users" });

            return result;
        }

        public static IEnumerable<UserModel> GetStaffUsers(DateTime sd, DateTime ed)
        {
            var result = DA.Current.Query<Client>()
                .Where(x => (x.Privs & ClientPrivilege.Staff) > 0 && (x.Privs & ClientPrivilege.PhysicalAccess) > 0)
                .FindActive(x => x.ClientID, sd, ed)
                .OrderBy(x => x.LName)
                .ThenBy(x => x.FName)
                .Select(x => new UserModel() { ClientID = x.ClientID, DisplayName = Client.GetDisplayName(x.LName, x.FName), AreaName = "Staff" });

            return result;
        }

        public static IEnumerable<UserModel> GetExternalUsers(DateTime sd, DateTime ed)
        {
            var result = DA.Current.Query<ClientInfo>()
                .Where(x => !x.PrimaryOrg && ((x.Privs & ClientPrivilege.LabUser) > 0 || (x.Privs & ClientPrivilege.RemoteUser) > 0))
                .FindActive(x => x.ClientID, sd, ed)
                .OrderBy(x => x.DisplayName)
                .Select(x => new UserModel() { ClientID = x.ClientID, DisplayName = x.DisplayName, AreaName = "External Users" });

            return result;
        }

        public static IEnumerable<UserModel> GetInternalUsers(DateTime sd, DateTime ed)
        {
            var result = DA.Current.Query<ClientInfo>()
                .Where(x => x.PrimaryOrg && ((x.Privs & ClientPrivilege.LabUser) > 0 || (x.Privs & ClientPrivilege.RemoteUser) > 0))
                .FindActive(x => x.ClientID, sd, ed)
                .OrderBy(x => x.DisplayName)
                .Select(x => new UserModel() { ClientID = x.ClientID, DisplayName = x.DisplayName, AreaName = "Internal Users" });

            return result;
        }

        public static IEnumerable<UserModel> GetInLabUsers()
        {
            var badges = Providers.PhysicalAccess.CurrentlyInArea();

            var result = badges
                .Select(x => new UserModel()
                {
                    ClientID = x.ClientID,
                    DisplayName = Client.GetDisplayName(x.LastName, x.FirstName),
                    AreaName = GetRoomName(x.CurrentAreaName)
                }).OrderBy(x => x.AreaName).ThenBy(x => x.DisplayName);

            return result;
        }

        private static string GetRoomName(string key)
        {
            if (roomMap.ContainsKey(key))
                return roomMap[key];
            else
                return key;
        }
    }
}