using LNF.Repository;
using LNF.Repository.Scheduler;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.Models
{
    public class ResourceModel
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }

        public static IEnumerable<ResourceModel> GetByProcessTech(int processTechId)
        {
            return DA.Current.Query<Resource>()
                .Where(x => x.IsActive && x.ProcessTech.ProcessTechID == processTechId)
                .OrderBy(x => x.ResourceName)
                .Select(x => new ResourceModel() { ResourceID = x.ResourceID, ResourceName = x.ResourceName });
        }
    }
}