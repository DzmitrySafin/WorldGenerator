using Generator.Enums;
using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.Structure
public abstract class Structure
{
    public StructureSettings Settings { get; set; }

    protected Structure(StructureSettings settings)
    {
        Settings = settings;
    }

    public List<Biome.Biome> Biomes => Settings.Biomes;

    //public Dictionary<MobCategory, StructureSpawnOverride> SpawnOverrides => Settings.SpawnOverrides;

    public DecorationType Step => Settings.Step;

    public TerrainAdjustmentType TerrainAdaptation => Settings.TerrainAdaptation;

    public BoundingBox adjustBoundingBox(BoundingBox p_226570_)
    {
        return TerrainAdaptation != TerrainAdjustmentType.NONE ? p_226570_.inflatedBy(12) : p_226570_;
    }

    //public StructureStart generate(
    //    Holder<Structure> p_378494_,
    //    ResourceKey<Level> p_376569_,
    //    RegistryAccess p_226597_,
    //    ChunkGenerator p_226598_,
    //    BiomeSource p_226599_,
    //    RandomState p_226600_,
    //    StructureTemplateManager p_226601_,
    //    long p_226602_,
    //    ChunkPosition p_226603_,
    //    int p_226604_,
    //    LevelHeightAccessor p_226605_,
    //    Predicate<Holder<Biome>> p_226606_
    //)
    //{
    //    ProfiledDuration profiledduration = JvmProfiler.INSTANCE.onStructureGenerate(p_226603_, p_376569_, p_378494_);
    //    Structure.GenerationContext structure$generationcontext = new Structure.GenerationContext(
    //        p_226597_, p_226598_, p_226599_, p_226600_, p_226601_, p_226602_, p_226603_, p_226605_, p_226606_
    //    );
    //    Optional<Structure.GenerationStub> optional = this.findValidGenerationPoint(structure$generationcontext);
    //    if (optional.isPresent())
    //    {
    //        StructurePiecesBuilder structurepiecesbuilder = optional.get().getPiecesBuilder();
    //        StructureStart structurestart = new StructureStart(this, p_226603_, p_226604_, structurepiecesbuilder.build());
    //        if (structurestart.isValid())
    //        {
    //            if (profiledduration != null)
    //            {
    //                profiledduration.finish(true);
    //            }

    //            return structurestart;
    //        }
    //    }

    //    if (profiledduration != null)
    //    {
    //        profiledduration.finish(false);
    //    }

    //    return StructureStart.INVALID_START;
    //}

    //protected static Optional<Structure.GenerationStub> onTopOfChunkCenter(
    //    Structure.GenerationContext p_226586_, Heightmap.Types p_226587_, Consumer<StructurePiecesBuilder> p_226588_
    //)
    //{
    //    ChunkPosition chunkpos = p_226586_.chunkPos();
    //    int i = chunkpos.GetMiddleBlockX();
    //    int j = chunkpos.GetMiddleBlockZ();
    //    int k = p_226586_.chunkGenerator().getFirstOccupiedHeight(i, j, p_226587_, p_226586_.heightAccessor(), p_226586_.randomState());
    //    return Optional.of(new Structure.GenerationStub(new BlockPosition(i, k, j), p_226588_));
    //}

    //private static bool isValidBiome(Structure.GenerationStub p_263042_, Structure.GenerationContext p_263005_)
    //{
    //    BlockPosition blockpos = p_263042_.position();
    //    return p_263005_.validBiome
    //        .test(
    //            p_263005_.chunkGenerator
    //                .getBiomeSource()
    //                .getNoiseBiome(
    //                    QuartPosition.FromBlock(blockpos.X),
    //                    QuartPosition.FromBlock(blockpos.Y),
    //                    QuartPosition.FromBlock(blockpos.Z),
    //                    p_263005_.randomState.sampler()
    //                )
    //        );
    //}

    //public void afterPlace(
    //    WorldGenLevel p_226560_,
    //    StructureManager p_226561_,
    //    ChunkGenerator p_226562_,
    //    IRandomSource p_226563_,
    //    BoundingBox p_226564_,
    //    ChunkPosition p_226565_,
    //    PiecesContainer p_226566_
    //)
    //{
    //}

    //private static int[] getCornerHeights(Structure.GenerationContext p_226614_, int p_226615_, int p_226616_, int p_226617_, int p_226618_)
    //{
    //    ChunkGenerator chunkgenerator = p_226614_.chunkGenerator();
    //    LevelHeightAccessor levelheightaccessor = p_226614_.heightAccessor();
    //    RandomState randomstate = p_226614_.randomState();
    //    return new int[]{
    //        chunkgenerator.getFirstOccupiedHeight(p_226615_, p_226617_, Heightmap.Types.WORLD_SURFACE_WG, levelheightaccessor, randomstate),
    //        chunkgenerator.getFirstOccupiedHeight(p_226615_, p_226617_ + p_226618_, Heightmap.Types.WORLD_SURFACE_WG, levelheightaccessor, randomstate),
    //        chunkgenerator.getFirstOccupiedHeight(p_226615_ + p_226616_, p_226617_, Heightmap.Types.WORLD_SURFACE_WG, levelheightaccessor, randomstate),
    //        chunkgenerator.getFirstOccupiedHeight(p_226615_ + p_226616_, p_226617_ + p_226618_, Heightmap.Types.WORLD_SURFACE_WG, levelheightaccessor, randomstate)
    //    };
    //}

    //public static int getMeanFirstOccupiedHeight(Structure.GenerationContext p_334739_, int p_329786_, int p_332089_, int p_333818_, int p_333198_)
    //{
    //    int[] aint = getCornerHeights(p_334739_, p_329786_, p_332089_, p_333818_, p_333198_);
    //    return (aint[0] + aint[1] + aint[2] + aint[3]) / 4;
    //}

    //protected static int getLowestY(Structure.GenerationContext p_226573_, int p_226574_, int p_226575_)
    //{
    //    ChunkPosition chunkpos = p_226573_.chunkPos();
    //    int i = chunkpos.GetMinBlockX();
    //    int j = chunkpos.GetMinBlockZ();
    //    return getLowestY(p_226573_, i, j, p_226574_, p_226575_);
    //}

    //protected static int getLowestY(Structure.GenerationContext p_226577_, int p_226578_, int p_226579_, int p_226580_, int p_226581_)
    //{
    //    int[] aint = getCornerHeights(p_226577_, p_226578_, p_226580_, p_226579_, p_226581_);
    //    return Math.Min(Math.Min(aint[0], aint[1]), Math.Min(aint[2], aint[3]));
    //}

    //[Obsolete]
    //protected BlockPosition getLowestYIn5by5BoxOffset7Blocks(Structure.GenerationContext p_226583_, RotationType p_226584_)
    //{
    //    int i = 5;
    //    int j = 5;
    //    if (p_226584_ == RotationType.CLOCKWISE_90)
    //    {
    //        i = -5;
    //    }
    //    else if (p_226584_ == RotationType.CLOCKWISE_180)
    //    {
    //        i = -5;
    //        j = -5;
    //    }
    //    else if (p_226584_ == RotationType.COUNTERCLOCKWISE_90)
    //    {
    //        j = -5;
    //    }

    //    ChunkPosition chunkpos = p_226583_.chunkPos();
    //    int k = chunkpos.GetBlockX(7);
    //    int l = chunkpos.GetBlockZ(7);
    //    return new BlockPosition(k, getLowestY(p_226583_, k, l, i, j), l);
    //}

    //protected abstract Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_226571_);

    //public Optional<Structure.GenerationStub> findValidGenerationPoint(Structure.GenerationContext p_263060_)
    //{
    //    return this.findGenerationPoint(p_263060_).filter(p_262911_->isValidBiome(p_262911_, p_263060_));
    //}

    //public abstract StructureType type();
    public virtual StructureType StructureType { get; set; }
}
