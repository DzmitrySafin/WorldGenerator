using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.placement.StructurePlacementType
[JsonConverter(typeof(StringEnumConverter))]
public enum StructurePlacementType
{
    [EnumMember(Value = "random_spread")]
    RANDOM_SPREAD,

    [EnumMember(Value = "concentric_rings")]
    CONCENTRIC_RINGS
}
