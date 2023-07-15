using StravaSegmentExplorerDataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class ActivityModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("athlete")]
        public MetaAthleteModel Athlete { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("distance")]
        public float Distance { get; set; }

        private double _distanceAsKm;

        public double DistanceAsKm
        {
            get { return _distanceAsKm; }
            set 
            {
                _distanceAsKm = Math.Round(Distance / 1000,2); 
            }
        }


        [JsonPropertyName("moving_time")]
        public int MovingTime { get; set; }

        private string _formatMovingTime;
        public string FormatMovingTime
        {
            get { return _formatMovingTime; }
            set
            {
                TimeSpan t = TimeSpan.FromSeconds(MovingTime);

                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                t.Hours,
                                t.Minutes,
                                t.Seconds);
                _formatMovingTime = answer; // Assign the value to the backing field
            }
        }


        [JsonPropertyName("elapsed_time")]
        public int ElapsedTime { get; set; }

        [JsonPropertyName("total_elevation_gain")]
        public float TotalElevationGain { get; set; }

        [JsonPropertyName("elev_high")]
        public float ElevHigh { get; set; }

        [JsonPropertyName("elev_low")]
        public float ElevLow { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sport_type")]
        public string SportType { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("start_date_local")]
        public DateTime StartDateLocal { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("achievement_count")]
        public int AchievementCount { get; set; }

        [JsonPropertyName("kudos_count")]
        public int KudosCount { get; set; }

        [JsonPropertyName("comment_count")]
        public int CommentCount { get; set; }

        [JsonPropertyName("athlete_count")]
        public int AthleteCount { get; set; }

        [JsonPropertyName("photo_count")]
        public int PhotoCount { get; set; }

        [JsonPropertyName("total_photo_count")]
        public int TotalPhotoCount { get; set; }

        [JsonPropertyName("trainer")]
        public bool Trainer { get; set; }

        [JsonPropertyName("commute")]
        public bool Commute { get; set; }

        [JsonPropertyName("manual")]
        public bool Manual { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("flagged")]
        public bool Flagged { get; set; }

        [JsonPropertyName("upload_id_str")]
        public string UploadIdStr { get; set; }

        [JsonPropertyName("average_speed")]
        public float AverageSpeed { get; set; }
        
        private double _AverageSpeedInKmh;

        public double AverageSpeedInKmh
        {
            get { return _AverageSpeedInKmh; }
            set
            {
                _AverageSpeedInKmh = Math.Round(AverageSpeed * 3.6f,2);
            }
        }


        [JsonPropertyName("max_speed")]
        public float MaxSpeed { get; set; }

        [JsonPropertyName("has_kudoed")]
        public bool HasKudoed { get; set; }

        [JsonPropertyName("hide_from_home")]
        public bool HideFromHome { get; set; }

        [JsonPropertyName("gear_id")]
        public string GearId { get; set; }

        [JsonPropertyName("kilojoules")]
        public float Kilojoules { get; set; }

        [JsonPropertyName("average_watts")]
        public float AverageWatts { get; set; }

        [JsonPropertyName("device_watts")]
        public bool DeviceWatts { get; set; }

        [JsonPropertyName("max_watts")]
        public int MaxWatts { get; set; }

        [JsonPropertyName("weighted_average_watts")]
        public int WeightedAverageWatts { get; set; }
    }
}
