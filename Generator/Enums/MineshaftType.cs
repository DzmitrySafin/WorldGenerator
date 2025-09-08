using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.structure.structures.MineshaftStructure.Type
[JsonConverter(typeof(StringEnumConverter))]
public enum MineshaftType
{
    [EnumMember(Value = "normal")]
    NORMAL,

    [EnumMember(Value = "mesa")]
    MESA
}
