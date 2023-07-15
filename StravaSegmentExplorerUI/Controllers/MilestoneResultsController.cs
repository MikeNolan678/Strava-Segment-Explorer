using Microsoft.AspNetCore.Mvc;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerUI.Models;
using System.Composition;
using System.Diagnostics;

namespace StravaSegmentExplorerUI.Controllers
{
    public static class MilestoneResultsController
    {
        public static List<ActivityModel> GetMilestoneResultsActivityList(List<ActivityModel> activityList, string milestone, string sport)
        {
            List<ActivityModel> results = new List<ActivityModel>();

            var distanceRange = GetDistanceRangeFromMilestone(milestone);

            foreach (var activity in activityList)
            {
                if (activity.SportType == sport &&
                    activity.Distance >= distanceRange.MinDistance &&
                    activity.Distance <= distanceRange.MaxDistance)
                {
                    results.Add(activity);
                }

                //initialise the formatted version for all activities
                activity.FormatMovingTime = activity.FormatMovingTime;
                activity.DistanceAsKm = activity.DistanceAsKm;
                activity.AverageSpeedInKmh = activity.AverageSpeedInKmh;

            }
            return results;
        }

        private static (int MinDistance, int MaxDistance) GetDistanceRangeFromMilestone(string milestone)
        {
            if (MilestoneDistances.TryGetValue(milestone, out var distanceRange))
            {
                return (distanceRange.MinDistance, distanceRange.MaxDistance);
            }

            throw new ArgumentException("Invalid milestone", nameof(milestone));
        }


        private static Dictionary<string, (int MinDistance, int MaxDistance)> MilestoneDistances = new Dictionary<string, (int, int)>
            {
                { "5k", (4700, 5500) },
                { "10k", (9700, 10500) },
                { "HalfMarathon", (21000, 21598) },
                { "Marathon", (42000, 42595) },
                { "50k", (49600, 50500) },
                { "100k", (99500, 100500) },
                { "200k", (19600, 200500) },
                { "50M", (80000, 80767) },
                { "100M", (16500, 170534) }
            };
    }
}

