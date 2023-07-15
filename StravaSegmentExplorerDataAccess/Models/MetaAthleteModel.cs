using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class MetaAthleteModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
