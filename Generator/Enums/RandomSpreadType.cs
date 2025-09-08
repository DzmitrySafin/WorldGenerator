using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.placement.RandomSpreadType
[JsonConverter(typeof(StringEnumConverter))]
public enum RandomSpreadType
{
    [EnumMember(Value = "linear")]
    LINEAR,

    [EnumMember(Value = "triangular")]
    TRIANGULAR
}
