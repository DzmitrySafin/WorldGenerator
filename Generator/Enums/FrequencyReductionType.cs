using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.placement.StructurePlacement.FrequencyReductionMethod
[JsonConverter(typeof(StringEnumConverter))]
public enum FrequencyReductionType
{
    [EnumMember(Value = "default")]
    DEFAULT = 0,

    [EnumMember(Value = "legacy_type_1")]
    LEGACY_TYPE_1,

    [EnumMember(Value = "legacy_type_2")]
    LEGACY_TYPE_2,

    [EnumMember(Value = "legacy_type_3")]
    LEGACY_TYPE_3
}
