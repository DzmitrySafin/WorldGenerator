using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.structures.OceanRuinStructure.Type
[JsonConverter(typeof(StringEnumConverter))]
public enum TemperatureType
{
    [EnumMember(Value = "warm")]
    WARM,

    [EnumMember(Value = "cold")]
    COLD
}
