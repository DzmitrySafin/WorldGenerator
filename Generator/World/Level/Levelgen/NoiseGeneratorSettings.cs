using Generator.Util;
using Generator.World.Level.Biome;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.NoiseGeneratorSettings
//source: data/minecraft/worldgen/noise_settings/overworld.json
public class NoiseGeneratorSettings
{
    [JsonProperty("noise")]
    public required NoiseSettings Noise { get; set; }

    //[JsonProperty("default_block")]
    //BlockState DefaultBlock { get; set; }

    //[JsonProperty("default_fluid")]
    //BlockState DefaultFluid { get; set; }

    [JsonProperty("noise_router")]
    public required NoiseRouter NoiseRouter { get; set; }

    //[JsonProperty("surface_rule")]
    //SurfaceRules.RuleSource SurfaceRule { get; set; }

    [JsonProperty("spawn_target")]
    public required List<ClimateParameterPoint> SpawnTargetPoints { get; set; }

    [JsonProperty("sea_level")]
    public int SeaLevel { get; set; }

    [JsonProperty("disable_mob_generation")]
    [Obsolete]
    public bool DisableMobGeneration { get; set; }

    [JsonProperty("aquifers_enabled")]
    public bool IsAquifersEnabled { get; set; }

    [JsonProperty("ore_veins_enabled")]
    public bool OreVeinsEnabled { get; set; }

    [JsonProperty("legacy_random_source")]
    public bool UseLegacyRandomSource { get; set; }

    public IRandomSource GetRandomSource(long seed)
    {
        //return UseLegacyRandomSource ? WorldgenRandom.Algorithm.LEGACY : WorldgenRandom.Algorithm.XOROSHIRO;
        return UseLegacyRandomSource ? new LegacyRandomSource(seed) : new XoroshiroRandomSource(seed);
    }
}
