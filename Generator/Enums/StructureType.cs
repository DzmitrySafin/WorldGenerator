using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.StructureType
[JsonConverter(typeof(StringEnumConverter))]
public enum StructureType
{
    NONE = 0,

    [EnumMember(Value = "buried_treasure")]
    BURIED_TREASURE,

    [EnumMember(Value = "desert_pyramid")]
    DESERT_PYRAMID,

    [EnumMember(Value = "end_city")]
    END_CITY,

    [EnumMember(Value = "fortress")]
    FORTRESS,

    [EnumMember(Value = "igloo")]
    IGLOO,

    [EnumMember(Value = "jigsaw")]
    JIGSAW,

    [EnumMember(Value = "jungle_temple")]
    JUNGLE_TEMPLE,

    [EnumMember(Value = "mineshaft")]
    MINESHAFT,

    [EnumMember(Value = "nether_fossil")]
    NETHER_FOSSIL,

    [EnumMember(Value = "ocean_monument")]
    OCEAN_MONUMENT,

    [EnumMember(Value = "ocean_ruin")]
    OCEAN_RUIN,

    [EnumMember(Value = "ruined_portal")]
    RUINED_PORTAL,

    [EnumMember(Value = "shipwreck")]
    SHIPWRECK,

    [EnumMember(Value = "stronghold")]
    STRONGHOLD,

    [EnumMember(Value = "swamp_hut")]
    SWAMP_HUT,

    [EnumMember(Value = "woodland_mansion")]
    WOODLAND_MANSION
}
