using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Marker.Type
[JsonConverter(typeof(StringEnumConverter))]
public enum DensityMarkerType
{
    [EnumMember(Value = "minecraft:interpolated")]
    Interpolated,

    [EnumMember(Value = "minecraft:flat_cache")]
    FlatCache,

    [EnumMember(Value = "minecraft:cache_2d")]
    Cache2D,

    [EnumMember(Value = "minecraft:cache_once")]
    CacheOnce,

    [EnumMember(Value = "minecraft:cache_all_in_cell")]
    CacheAllInCell
}
