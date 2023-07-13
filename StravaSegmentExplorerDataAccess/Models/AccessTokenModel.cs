using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static StravaSegmentExplorerDataAccess.Models.AthleteModel;
using static System.Net.Mime.MediaTypeNames;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class AccessTokenModel
    {
        public int Id { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_at")]
        public int ExpiresAt { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("athlete")]
        public Athlete Athlete { get; set; }

    }
}
