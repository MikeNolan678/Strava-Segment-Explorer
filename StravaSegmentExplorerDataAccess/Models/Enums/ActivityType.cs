using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models.Enums
{
    public enum ActivityType
    {
        [JsonPropertyName("AlpineSki")]
        AlpineSki,
        [JsonPropertyName("BackcountrySki")]
        BackcountrySki,
        [JsonPropertyName("Canoeing")]
        Canoeing,
        [JsonPropertyName("Crossfit")]
        Crossfit,
        [JsonPropertyName("EBikeRide")]
        EBikeRide,
        [JsonPropertyName("Elliptical")]
        Elliptical,
        [JsonPropertyName("Golf")]
        Golf,
        [JsonPropertyName("Handcycle")]
        Handcycle,
        [JsonPropertyName("Hike")]
        Hike,
        [JsonPropertyName("IceSkate")]
        IceSkate,
        [JsonPropertyName("InlineSkate")]
        InlineSkate,
        [JsonPropertyName("Kayaking")]
        Kayaking,
        [JsonPropertyName("Kitesurf")]
        Kitesurf,
        [JsonPropertyName("NordicSki")]
        NordicSki,
        [JsonPropertyName("Ride")]
        Ride,
        [JsonPropertyName("RockClimbing")]
        RockClimbing,
        [JsonPropertyName("RollerSki")]
        RollerSki,
        [JsonPropertyName("Rowing")]
        Rowing,
        [JsonPropertyName("Run")]
        Run,
        [JsonPropertyName("Sail")]
        Sail,
        [JsonPropertyName("Skateboard")]
        Skateboard,
        [JsonPropertyName("Snowboard")]
        Snowboard,
        [JsonPropertyName("Snowshoe")]
        Snowshoe,
        [JsonPropertyName("Soccer")]
        Soccer,
        [JsonPropertyName("StairStepper")]
        StairStepper,
        [JsonPropertyName("StandUpPaddling")]
        StandUpPaddling,
        [JsonPropertyName("Surfing")]
        Surfing,
        [JsonPropertyName("Swim")]
        Swim,
        [JsonPropertyName("Velomobile")]
        Velomobile,
        [JsonPropertyName("VirtualRide")]
        VirtualRide,
        [JsonPropertyName("VirtualRun")]
        VirtualRun,
        [JsonPropertyName("Walk")]
        Walk,
        [JsonPropertyName("WeightTraining")]
        WeightTraining,
        [JsonPropertyName("Wheelchair")]
        Wheelchair,
        [JsonPropertyName("Windsurf")]
        Windsurf,
        [JsonPropertyName("Workout")]
        Workout,
        [JsonPropertyName("Yoga")]
        Yoga,
        Unknown // Add a fallback value to handle unexpected values
    }
}
