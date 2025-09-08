using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Mapped.Type
[JsonConverter(typeof(StringEnumConverter))]
public enum DensityMappedType
{
    NONE = 0,

    [EnumMember(Value = "minecraft:abs")]
    ABS,

    [EnumMember(Value = "minecraft:square")]
    SQUARE,

    [EnumMember(Value = "minecraft:cube")]
    CUBE,

    [EnumMember(Value = "minecraft:half_negative")]
    HALF_NEGATIVE,

    [EnumMember(Value = "minecraft:quarter_negative")]
    QUARTER_NEGATIVE,

    [EnumMember(Value = "minecraft:squeeze")]
    SQUEEZE
}
