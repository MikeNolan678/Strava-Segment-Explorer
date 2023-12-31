﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models.Enums
{
    public enum SportType
    {
        [JsonPropertyName("AlpineSki")]
        AlpineSki,
        [JsonPropertyName("BackcountrySki")]
        BackcountrySki,
        [JsonPropertyName("Badminton")]
        Badminton,
        [JsonPropertyName("Canoeing")]
        Canoeing,
        [JsonPropertyName("Crossfit")]
        Crossfit,
        [JsonPropertyName("EBikeRide")]
        EBikeRide,
        [JsonPropertyName("Elliptical")]
        Elliptical,
        [JsonPropertyName("EMountainBikeRide")]
        EMountainBikeRide,
        [JsonPropertyName("Golf")]
        Golf,
        [JsonPropertyName("GravelRide")]
        GravelRide,
        [JsonPropertyName("Handcycle")]
        Handcycle,
        [JsonPropertyName("HighIntensityIntervalTraining")]
        HighIntensityIntervalTraining,
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
        [JsonPropertyName("MountainBikeRide")]
        MountainBikeRide,
        [JsonPropertyName("NordicSki")]
        NordicSki,
        [JsonPropertyName("Pickleball")]
        Pickleball,
        [JsonPropertyName("Pilates")]
        Pilates,
        [JsonPropertyName("Racquetball")]
        Racquetball,
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
        [JsonPropertyName("Squash")]
        Squash,
        [JsonPropertyName("StairStepper")]
        StairStepper,
        [JsonPropertyName("StandUpPaddling")]
        StandUpPaddling,
        [JsonPropertyName("Surfing")]
        Surfing,
        [JsonPropertyName("Swim")]
        Swim,
        [JsonPropertyName("TableTennis")]
        TableTennis,
        [JsonPropertyName("Tennis")]
        Tennis,
        [JsonPropertyName("TrailRun")]
        TrailRun,
        [JsonPropertyName("Velomobile")]
        Velomobile,
        [JsonPropertyName("VirtualRide")]
        VirtualRide,
        [JsonPropertyName("VirtualRow")]
        VirtualRow,
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
        Yoga
    }
}
