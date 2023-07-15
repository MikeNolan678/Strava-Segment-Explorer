using StravaSegmentExplorerDataAccess.Models;
using System.Security.Policy;

namespace StravaSegmentExplorerUI.Models
{
    public class MilestoneModel
    {
        public bool ResultIsReady { get; set; } = false;
        public string Sport { get; set; }
        public string Milestone { get; set; }
        public List<ActivityModel> Results { get;}

        public MilestoneModel()
        {
            Results = new List<ActivityModel>();
        }

    }
}
