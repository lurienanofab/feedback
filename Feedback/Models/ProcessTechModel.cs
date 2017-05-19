using LNF.Repository;
using LNF.Repository.Scheduler;
using System.Collections.Generic;
using System.Linq;

namespace Feedback.Models
{
    public class ProcessTechModel
    {
        public int ProcessTechID { get; set; }
        public string ProcessTechName { get; set; }
        public string LabName { get; set; }

        public static IEnumerable<ProcessTechModel> GetAll()
        {
            return DA.Current.Query<ProcessTech>()
                .Where(x => x.IsActive && x.Lab.IsActive && x.Lab.Room.Active).ToList()
                .Select(x => new ProcessTechModel() { ProcessTechID = x.ProcessTechID, ProcessTechName = x.ProcessTechName, LabName = x.Lab.GetDisplayName() })
                .OrderBy(x => x.LabName)
                .ThenBy(x => x.ProcessTechName);
        }
    }
}