using Generator.Core;
using Generator.Enums;
using Generator.Json;
using Generator.World.Level.Chunk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Placement;

//source: net.minecraft.world.level.levelgen.structure.placement.ConcentricRingsStructurePlacement
public class ConcentricRingsStructurePlacement : StructurePlacement
{
    [JsonProperty("distance")]
    public int Distance { get; set; }

    [JsonProperty("spread")]
    public int Spread { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("preferred_biomes"), JsonConverter(typeof(StructureBiomesConverter))]
    public List<Biome.Biome> PreferredBiomes { get; set; }

    public override StructurePlacementType PlacementType => StructurePlacementType.CONCENTRIC_RINGS;

    public ConcentricRingsStructurePlacement()
        : base()
    {
        // default constructor for JSON deserialization
    }

    public ConcentricRingsStructurePlacement(
        Vec3i p_226981_,
        FrequencyReductionType p_226982_,
        float p_226983_,
        int p_226984_,
        ExclusionZone? p_226985_,
        int p_226986_,
        int p_226987_,
        int p_226988_,
        List<Biome.Biome> p_226989_
    )
        : base(p_226981_, p_226982_, p_226983_, p_226984_, p_226985_)
    {
        Distance = p_226986_;
        Spread = p_226987_;
        Count = p_226988_;
        PreferredBiomes = p_226989_;
    }

    public ConcentricRingsStructurePlacement(int p_226976_, int p_226977_, int p_226978_, List<Biome.Biome> p_226979_)
        : this(Vec3i.ZERO, FrequencyReductionType.DEFAULT, 1.0F, 0, null, p_226976_, p_226977_, p_226978_, p_226979_)
    {
    }

    protected override bool isPlacementChunk(ChunkGeneratorStructureState p_256631_, int p_256202_, int p_255915_)
    {
        List<ChunkPosition> list = p_256631_.GetRingPositionsFor(this);
        return list == null ? false : list.Contains(new ChunkPosition(p_256202_, p_255915_));
    }
}
