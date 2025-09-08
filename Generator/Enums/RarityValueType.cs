using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.DensityFunctions.WeirdScaledSampler.RarityValueMapper
[JsonConverter(typeof(StringEnumConverter))]
public enum RarityValueType
{
    [EnumMember(Value = "type_1")]
    TYPE1,

    [EnumMember(Value = "type_2")]
    TYPE2
}
