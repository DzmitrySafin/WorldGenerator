using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.GenerationStep.Decoration
[JsonConverter(typeof(StringEnumConverter))]
public enum DecorationType
{
    NONE = 0,

    [EnumMember(Value = "raw_generation")]
    RAW_GENERATION,

    [EnumMember(Value = "lakes")]
    LAKES,

    [EnumMember(Value = "local_modifications")]
    LOCAL_MODIFICATIONS,

    [EnumMember(Value = "underground_structures")]
    UNDERGROUND_STRUCTURES,

    [EnumMember(Value = "surface_structures")]
    SURFACE_STRUCTURES,

    [EnumMember(Value = "strongholds")]
    STRONGHOLDS,

    [EnumMember(Value = "underground_ores")]
    UNDERGROUND_ORES,

    [EnumMember(Value = "underground_decoration")]
    UNDERGROUND_DECORATION,

    [EnumMember(Value = "fluid_springs")]
    FLUID_SPRINGS,

    [EnumMember(Value = "vegetal_decoration")]
    VEGETAL_DECORATION,

    [EnumMember(Value = "top_layer_modification")]
    TOP_LAYER_MODIFICATION
}
