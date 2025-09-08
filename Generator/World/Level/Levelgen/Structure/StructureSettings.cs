using Generator.Enums;
using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.Structure.StructureSettings
public class StructureSettings
{
    [JsonProperty("biomes"), JsonConverter(typeof(StructureBiomesConverter))]
    public List<Biome.Biome> Biomes { get; set; }

    //[JsonProperty("spawn_overrides")]
    //public Dictionary<MobCategory, StructureSpawnOverride> SpawnOverrides { get; set; }

    [JsonProperty("step")]
    public DecorationType Step { get; set; }

    [JsonProperty("terrain_adaptation")]
    public TerrainAdjustmentType TerrainAdaptation { get; set; }

    public StructureSettings()
    {
        // default constructor for JSON deserialization
    }

    public StructureSettings(List<Biome.Biome> biomes, DecorationType step, TerrainAdjustmentType terrainAdaptation)
    {
        Biomes = biomes;
        Step = step;
        TerrainAdaptation = terrainAdaptation;
    }

    static readonly StructureSettings DEFAULT = new StructureSettings(
        new List<Biome.Biome>(),
        //new Dictionary<MobCategory, StructureSpawnOverride>(),
        DecorationType.SURFACE_STRUCTURES,
        TerrainAdjustmentType.NONE);

    public StructureSettings(List<Biome.Biome> biomes)
        : this(biomes, /*DEFAULT.SpawnOverrides,*/ DEFAULT.Step, DEFAULT.TerrainAdaptation)
    {
    }
}
