using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Generator.Enums;

//source: net.minecraft.world.level.biome.Biomes
[JsonConverter(typeof(StringEnumConverter))]
public enum BiomeType
{
    NONE = 0,

    [EnumMember(Value = "the_void")]
    THE_VOID,

    [EnumMember(Value = "plains")]
    PLAINS,

    [EnumMember(Value = "sunflower_plains")]
    SUNFLOWER_PLAINS,

    [EnumMember(Value = "snowy_plains")]
    SNOWY_PLAINS,

    [EnumMember(Value = "ice_spikes")]
    ICE_SPIKES,

    [EnumMember(Value = "desert")]
    DESERT,

    [EnumMember(Value = "swamp")]
    SWAMP,

    [EnumMember(Value = "mangrove_swamp")]
    MANGROVE_SWAMP,

    [EnumMember(Value = "forest")]
    FOREST,

    [EnumMember(Value = "flower_forest")]
    FLOWER_FOREST,

    [EnumMember(Value = "birch_forest")]
    BIRCH_FOREST,

    [EnumMember(Value = "dark_forest")]
    DARK_FOREST,

    [EnumMember(Value = "pale_garden")]
    PALE_GARDEN,

    [EnumMember(Value = "old_growth_birch_forest")]
    OLD_GROWTH_BIRCH_FOREST,

    [EnumMember(Value = "old_growth_pine_taiga")]
    OLD_GROWTH_PINE_TAIGA,

    [EnumMember(Value = "old_growth_spruce_taiga")]
    OLD_GROWTH_SPRUCE_TAIGA,

    [EnumMember(Value = "taiga")]
    TAIGA,

    [EnumMember(Value = "snowy_taiga")]
    SNOWY_TAIGA,

    [EnumMember(Value = "savanna")]
    SAVANNA,

    [EnumMember(Value = "savanna_plateau")]
    SAVANNA_PLATEAU,

    [EnumMember(Value = "windswept_hills")]
    WINDSWEPT_HILLS,

    [EnumMember(Value = "windswept_gravelly_hills")]
    WINDSWEPT_GRAVELLY_HILLS,

    [EnumMember(Value = "windswept_forest")]
    WINDSWEPT_FOREST,

    [EnumMember(Value = "windswept_savanna")]
    WINDSWEPT_SAVANNA,

    [EnumMember(Value = "jungle")]
    JUNGLE,

    [EnumMember(Value = "sparse_jungle")]
    SPARSE_JUNGLE,

    [EnumMember(Value = "bamboo_jungle")]
    BAMBOO_JUNGLE,

    [EnumMember(Value = "badlands")]
    BADLANDS,

    [EnumMember(Value = "eroded_badlands")]
    ERODED_BADLANDS,

    [EnumMember(Value = "wooded_badlands")]
    WOODED_BADLANDS,

    [EnumMember(Value = "meadow")]
    MEADOW,

    [EnumMember(Value = "cherry_grove")]
    CHERRY_GROVE,

    [EnumMember(Value = "grove")]
    GROVE,

    [EnumMember(Value = "snowy_slopes")]
    SNOWY_SLOPES,

    [EnumMember(Value = "frozen_peaks")]
    FROZEN_PEAKS,

    [EnumMember(Value = "jagged_peaks")]
    JAGGED_PEAKS,

    [EnumMember(Value = "stony_peaks")]
    STONY_PEAKS,

    [EnumMember(Value = "river")]
    RIVER,

    [EnumMember(Value = "frozen_river")]
    FROZEN_RIVER,

    [EnumMember(Value = "beach")]
    BEACH,

    [EnumMember(Value = "snowy_beach")]
    SNOWY_BEACH,

    [EnumMember(Value = "stony_shore")]
    STONY_SHORE,

    [EnumMember(Value = "warm_ocean")]
    WARM_OCEAN,

    [EnumMember(Value = "lukewarm_ocean")]
    LUKEWARM_OCEAN,

    [EnumMember(Value = "deep_lukewarm_ocean")]
    DEEP_LUKEWARM_OCEAN,

    [EnumMember(Value = "ocean")]
    OCEAN,

    [EnumMember(Value = "deep_ocean")]
    DEEP_OCEAN,

    [EnumMember(Value = "cold_ocean")]
    COLD_OCEAN,

    [EnumMember(Value = "deep_cold_ocean")]
    DEEP_COLD_OCEAN,

    [EnumMember(Value = "frozen_ocean")]
    FROZEN_OCEAN,

    [EnumMember(Value = "deep_frozen_ocean")]
    DEEP_FROZEN_OCEAN,

    [EnumMember(Value = "mushroom_fields")]
    MUSHROOM_FIELDS,

    [EnumMember(Value = "dripstone_caves")]
    DRIPSTONE_CAVES,

    [EnumMember(Value = "lush_caves")]
    LUSH_CAVES,

    [EnumMember(Value = "deep_dark")]
    DEEP_DARK,

    [EnumMember(Value = "nether_wastes")]
    NETHER_WASTES,

    [EnumMember(Value = "warped_forest")]
    WARPED_FOREST,

    [EnumMember(Value = "crimson_forest")]
    CRIMSON_FOREST,

    [EnumMember(Value = "soul_sand_valley")]
    SOUL_SAND_VALLEY,

    [EnumMember(Value = "basalt_deltas")]
    BASALT_DELTAS,

    [EnumMember(Value = "the_end")]
    THE_END,

    [EnumMember(Value = "end_highlands")]
    END_HIGHLANDS,

    [EnumMember(Value = "end_midlands")]
    END_MIDLANDS,

    [EnumMember(Value = "small_end_islands")]
    SMALL_END_ISLANDS,

    [EnumMember(Value = "end_barrens")]
    END_BARRENS
}
