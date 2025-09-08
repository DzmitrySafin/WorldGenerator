using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.TerrainAdjustment
[JsonConverter(typeof(StringEnumConverter))]
public enum TerrainAdjustmentType
{
    [EnumMember(Value = "none")]
    NONE = 0,

    [EnumMember(Value = "bury")]
    BURY,

    [EnumMember(Value = "beard_thin")]
    BEARD_THIN,

    [EnumMember(Value = "beard_box")]
    BEARD_BOX,

    [EnumMember(Value = "encapsulate")]
    ENCAPSULATE
}
