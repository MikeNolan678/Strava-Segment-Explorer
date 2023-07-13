using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class AppUserModel
    {
        public int AthleteId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Sex { get; set; }
        public double Weight { get; set; }
        public string Profile { get; set; }
        public int ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public bool IsStravaConnected { get; set; }

    }
}
