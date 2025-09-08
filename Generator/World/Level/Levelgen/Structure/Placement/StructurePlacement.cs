using Generator.Core;
using Generator.Enums;
using Generator.World.Level.Chunk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Placement;

//source: net.minecraft.world.level.levelgen.structure.placement.StructurePlacement
public abstract class StructurePlacement
{
    //private static readonly int HIGHLY_ARBITRARY_RANDOM_SALT = 10387320;

    [JsonProperty("type")]
    public virtual StructurePlacementType PlacementType { get; set; }

    [JsonProperty("locate_offset")]
    public Vec3i LocateOffset { get; set; }

    [JsonProperty("frequency_reduction_method")]
    public FrequencyReduction FrequencyReductionMethod  { get; set; }

    [JsonProperty("frequency")]
    public float Frequency { get; set; }

    [JsonProperty("salt")]
    public int Salt { get; set; }

    [JsonProperty("exclusion_zone")]
    public ExclusionZone? ExclusionZone { get; set; }

    protected StructurePlacement()
    {
        // default constructor for JSON deserialization
    }

    protected StructurePlacement(
        Vec3i p_227028_,
        FrequencyReductionType p_227029_,
        float p_227030_,
        int p_227031_,
        ExclusionZone? p_227032_
    )
    {
        LocateOffset = p_227028_;
        FrequencyReductionMethod = new FrequencyReduction() { ReductionType = p_227029_ };
        Frequency = p_227030_;
        Salt = p_227031_;
        ExclusionZone = p_227032_;
    }

    public bool isStructureChunk(ChunkGeneratorStructureState p_256635_, int p_255959_, int p_256065_)
    {
        return this.isPlacementChunk(p_256635_, p_255959_, p_256065_)
            && this.applyAdditionalChunkRestrictions(p_255959_, p_256065_, p_256635_.LevelSeed)
            && this.applyInteractionsWithOtherStructures(p_256635_, p_255959_, p_256065_);
    }

    public bool applyAdditionalChunkRestrictions(int p_330491_, int p_330207_, long p_334851_)
    {
        return !(Frequency < 1.0F) || FrequencyReductionMethod.ShouldGenerate(p_334851_, Salt, p_330491_, p_330207_, Frequency);
    }

    public bool applyInteractionsWithOtherStructures(ChunkGeneratorStructureState p_332649_, int p_327790_, int p_329174_)
    {
        return ExclusionZone == null || !ExclusionZone.isPlacementForbidden(p_332649_, p_327790_, p_329174_);
    }

    protected abstract bool isPlacementChunk(ChunkGeneratorStructureState p_256034_, int p_227046_, int p_227047_);

    public BlockPosition getLocatePos(ChunkPosition p_227040_)
    {
        return new BlockPosition(p_227040_.GetMinBlockX(), 0, p_227040_.GetMinBlockZ()).Offset(LocateOffset);
    }
}

public record ExclusionZone(StructureSet otherSet, int chunkCount)
{
    //public static final Codec<StructurePlacement.ExclusionZone> CODEC = RecordCodecBuilder.create(
    //    p_259015_->p_259015_.group(
    //            RegistryFileCodec.create(Registries.STRUCTURE_SET, StructureSet.DIRECT_CODEC, false)
    //                .fieldOf("other_set")
    //                .forGetter(StructurePlacement.ExclusionZone::otherSet),
    //            Codec.intRange(1, 16).fieldOf("chunk_count").forGetter(StructurePlacement.ExclusionZone::chunkCount)
    //        )
    //        .apply(p_259015_, StructurePlacement.ExclusionZone::new)
    //);

    public bool isPlacementForbidden(ChunkGeneratorStructureState p_255745_, int p_255634_, int p_255892_)
    {
        return p_255745_.HasStructureChunkInRange(this.otherSet, p_255634_, p_255892_, this.chunkCount);
    }
}
