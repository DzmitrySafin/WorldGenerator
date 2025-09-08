using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.DensityFunctions.TwoArgumentSimpleFunction.Type
[JsonConverter(typeof(StringEnumConverter))]
public enum TwoArgumentsType
{
    [EnumMember(Value = "minecraft:add")]
    ADD,

    [EnumMember(Value = "minecraft:mul")]
    MUL,

    [EnumMember(Value = "minecraft:min")]
    MIN,

    [EnumMember(Value = "minecraft:max")]
    MAX
}
