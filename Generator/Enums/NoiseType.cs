using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.levelgen.Noises
[JsonConverter(typeof(StringEnumConverter))]
public enum NoiseType
{
    NONE = 0,

    [EnumMember(Value = "temperature")]
    TEMPERATURE,

    [EnumMember(Value = "vegetation")]
    VEGETATION,

    [EnumMember(Value = "continentalness")]
    CONTINENTALNESS,

    [EnumMember(Value = "erosion")]
    EROSION,

    [EnumMember(Value = "temperature_large")]
    TEMPERATURE_LARGE,

    [EnumMember(Value = "vegetation_large")]
    VEGETATION_LARGE,

    [EnumMember(Value = "continentalness_large")]
    CONTINENTALNESS_LARGE,

    [EnumMember(Value = "erosion_large")]
    EROSION_LARGE,

    [EnumMember(Value = "ridge")]
    RIDGE,

    [EnumMember(Value = "offset")]
    SHIFT,

    [EnumMember(Value = "aquifer_barrier")]
    AQUIFER_BARRIER,

    [EnumMember(Value = "aquifer_fluid_level_floodedness")]
    AQUIFER_FLUID_LEVEL_FLOODEDNESS,

    [EnumMember(Value = "aquifer_lava")]
    AQUIFER_LAVA,

    [EnumMember(Value = "aquifer_fluid_level_spread")]
    AQUIFER_FLUID_LEVEL_SPREAD,

    [EnumMember(Value = "pillar")]
    PILLAR,

    [EnumMember(Value = "pillar_rareness")]
    PILLAR_RARENESS,

    [EnumMember(Value = "pillar_thickness")]
    PILLAR_THICKNESS,

    [EnumMember(Value = "spaghetti_2d")]
    SPAGHETTI_2D,

    [EnumMember(Value = "spaghetti_2d_elevation")]
    SPAGHETTI_2D_ELEVATION,

    [EnumMember(Value = "spaghetti_2d_modulator")]
    SPAGHETTI_2D_MODULATOR,

    [EnumMember(Value = "spaghetti_2d_thickness")]
    SPAGHETTI_2D_THICKNESS,

    [EnumMember(Value = "spaghetti_3d_1")]
    SPAGHETTI_3D_1,

    [EnumMember(Value = "spaghetti_3d_2")]
    SPAGHETTI_3D_2,

    [EnumMember(Value = "spaghetti_3d_rarity")]
    SPAGHETTI_3D_RARITY,

    [EnumMember(Value = "spaghetti_3d_thickness")]
    SPAGHETTI_3D_THICKNESS,

    [EnumMember(Value = "spaghetti_roughness")]
    SPAGHETTI_ROUGHNESS,

    [EnumMember(Value = "spaghetti_roughness_modulator")]
    SPAGHETTI_ROUGHNESS_MODULATOR,

    [EnumMember(Value = "cave_entrance")]
    CAVE_ENTRANCE,

    [EnumMember(Value = "cave_layer")]
    CAVE_LAYER,

    [EnumMember(Value = "cave_cheese")]
    CAVE_CHEESE,

    [EnumMember(Value = "ore_veininess")]
    ORE_VEININESS,

    [EnumMember(Value = "ore_vein_a")]
    ORE_VEIN_A,

    [EnumMember(Value = "ore_vein_b")]
    ORE_VEIN_B,

    [EnumMember(Value = "ore_gap")]
    ORE_GAP,

    [EnumMember(Value = "noodle")]
    NOODLE,

    [EnumMember(Value = "noodle_thickness")]
    NOODLE_THICKNESS,

    [EnumMember(Value = "noodle_ridge_a")]
    NOODLE_RIDGE_A,

    [EnumMember(Value = "noodle_ridge_b")]
    NOODLE_RIDGE_B,

    [EnumMember(Value = "jagged")]
    JAGGED,

    [EnumMember(Value = "surface")]
    SURFACE,

    [EnumMember(Value = "surface_secondary")]
    SURFACE_SECONDARY,

    [EnumMember(Value = "clay_bands_offset")]
    CLAY_BANDS_OFFSET,

    [EnumMember(Value = "badlands_pillar")]
    BADLANDS_PILLAR,

    [EnumMember(Value = "badlands_pillar_roof")]
    BADLANDS_PILLAR_ROOF,

    [EnumMember(Value = "badlands_surface")]
    BADLANDS_SURFACE,

    [EnumMember(Value = "iceberg_pillar")]
    ICEBERG_PILLAR,

    [EnumMember(Value = "iceberg_pillar_roof")]
    ICEBERG_PILLAR_ROOF,

    [EnumMember(Value = "iceberg_surface")]
    ICEBERG_SURFACE,

    [EnumMember(Value = "surface_swamp")]
    SWAMP,

    [EnumMember(Value = "calcite")]
    CALCITE,

    [EnumMember(Value = "gravel")]
    GRAVEL,

    [EnumMember(Value = "powder_snow")]
    POWDER_SNOW,

    [EnumMember(Value = "packed_ice")]
    PACKED_ICE,

    [EnumMember(Value = "ice")]
    ICE,

    [EnumMember(Value = "soul_sand_layer")]
    SOUL_SAND_LAYER,

    [EnumMember(Value = "gravel_layer")]
    GRAVEL_LAYER,

    [EnumMember(Value = "patch")]
    PATCH,

    [EnumMember(Value = "netherrack")]
    NETHERRACK,

    [EnumMember(Value = "nether_wart")]
    NETHER_WART,

    [EnumMember(Value = "nether_state_selector")]
    NETHER_STATE_SELECTOR
}
