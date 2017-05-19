using Feedback.Models;
using System.Collections.Generic;
using System.Web.Http;
using System;

namespace Feedback.Controllers.Api
{
    public class UserController : ApiController
    {
        [Route("api/user/{group}")]
        public IEnumerable<UserModel> Get(string group, DateTime? date = null)
        {
            DateTime sd = default(DateTime);
            DateTime ed = default(DateTime);

            if (date.HasValue)
            {
                sd = date.Value;
                ed = sd.AddDays(1);
            }
            
            switch (group)
            {
                case "inlab":
                    return UserModel.GetInLabUsers();
                case "internal":
                    return UserModel.GetInternalUsers(sd, ed);
                case "external":
                    return UserModel.GetExternalUsers(sd, ed);
                case "staff":
                    return UserModel.GetStaffUsers(sd, ed);
                default:
                    return UserModel.GetAllUsers(sd, ed);
            }
        }
    }
}
