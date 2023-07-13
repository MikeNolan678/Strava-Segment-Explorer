using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class AthleteModel
    {
        public class Athlete
        {
            [JsonPropertyName("id")]
            public int AthleteId { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("resource_state")]
            public int ResourceState { get; set; }

            [JsonPropertyName("firstname")]
            public string FirstName { get; set; }

            [JsonPropertyName("lastname")]
            public string LastName { get; set; }

            [JsonPropertyName("bio")]
            public string Bio { get; set; }

            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("state")]
            public string State { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }

            [JsonPropertyName("sex")]
            public string Sex { get; set; }

            [JsonPropertyName("premium")]
            public bool Premium { get; set; }

            [JsonPropertyName("summit")]
            public bool Summit { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [JsonPropertyName("badge_type_id")]
            public int BadgeTypeId { get; set; }

            [JsonPropertyName("weight")]
            public double Weight { get; set; }

            [JsonPropertyName("profile_medium")]
            public string ProfileMedium { get; set; }

            [JsonPropertyName("profile")]
            public string Profile { get; set; }

            [JsonPropertyName("friend")]
            public string Friend { get; set; }

            [JsonPropertyName("follower")]
            public string Follower { get; set; }
        }

    }
}