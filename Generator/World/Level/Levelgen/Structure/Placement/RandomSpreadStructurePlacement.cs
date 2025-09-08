using Generator.Core;
using Generator.Enums;
using Generator.Helpers;
using Generator.World.Level.Chunk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Placement;

//source: net.minecraft.world.level.levelgen.structure.placement.RandomSpreadStructurePlacement
public class RandomSpreadStructurePlacement : StructurePlacement
{
    [JsonProperty("spacing")]
    public int Spacing { get; set; }

    [JsonProperty("separation")]
    public int Separation { get; set; }

    [JsonProperty("spread_type")]
    public RandomSpreadType SpreadType { get; set; }

    public override StructurePlacementType PlacementType => StructurePlacementType.RANDOM_SPREAD;

    public RandomSpreadStructurePlacement()
        : base()
    {
        // default constructor for JSON deserialization
    }

    //private static DataResult<RandomSpreadStructurePlacement> validate(RandomSpreadStructurePlacement p_286361_)
    //{
    //    return p_286361_.Spacing <= p_286361_.Separation ? DataResult.error(()-> "Spacing has to be larger than separation") : DataResult.success(p_286361_);
    //}

    public RandomSpreadStructurePlacement(
        Vec3i p_227000_,
        FrequencyReductionType p_227001_,
        float p_227002_,
        int p_227003_,
        ExclusionZone? p_227004_,
        int p_227005_,
        int p_227006_,
        RandomSpreadType p_227007_
    )
        : base(p_227000_, p_227001_, p_227002_, p_227003_, p_227004_)
    {
        Spacing = p_227005_;
        Separation = p_227006_;
        SpreadType = p_227007_;
    }

    public RandomSpreadStructurePlacement(int p_204980_, int p_204981_, RandomSpreadType p_204982_, int p_204983_)
        : this(Vec3i.ZERO, FrequencyReductionType.DEFAULT, 1.0F, p_204983_, null, p_204980_, p_204981_, p_204982_)
    {
    }

    public ChunkPosition getPotentialStructureChunk(long p_227009_, int p_227010_, int p_227011_)
    {
        int i = (int) Math.Floor((decimal)p_227010_ / Spacing);
        int j = (int) Math.Floor((decimal)p_227011_ / Spacing);
        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(0L));
        worldgenrandom.SetLargeFeatureWithSalt(p_227009_, i, j, Salt);
        int k = Spacing - Separation;
        int l = SpreadType.Evaluate(worldgenrandom, k);
        int i1 = SpreadType.Evaluate(worldgenrandom, k);
        return new ChunkPosition(i * Spacing + l, j * Spacing + i1);
    }

    protected override bool isPlacementChunk(ChunkGeneratorStructureState p_256267_, int p_256050_, int p_255975_)
    {
        ChunkPosition chunkpos = getPotentialStructureChunk(p_256267_.LevelSeed, p_256050_, p_255975_);
        return chunkpos.X == p_256050_ && chunkpos.Z == p_255975_;
    }
}
