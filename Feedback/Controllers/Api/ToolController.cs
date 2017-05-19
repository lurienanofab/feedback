using Feedback.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Feedback.Controllers.Api
{
    public class ToolController : ApiController
    {
        [Route("api/tool/proctech")]
        public IEnumerable<ProcessTechModel> GetProcessTechs()
        {
            return ProcessTechModel.GetAll();
        }

        [Route("api/tool/proctech/{processTechId}/resource")]
        public IEnumerable<ResourceModel> GetResources(int processTechId)
        {
            return ResourceModel.GetByProcessTech(processTechId);
        }
    }
}
