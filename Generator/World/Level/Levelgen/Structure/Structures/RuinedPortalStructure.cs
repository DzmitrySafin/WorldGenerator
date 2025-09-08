using Generator.Core;
using Generator.Enums;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.RuinedPortalStructure
public class RuinedPortalStructure : Structure
{
    public override StructureType StructureType => StructureType.RUINED_PORTAL;

    //private static readonly string[] STRUCTURE_LOCATION_PORTALS = [
    //    "ruined_portal/portal_1",
    //    "ruined_portal/portal_2",
    //    "ruined_portal/portal_3",
    //    "ruined_portal/portal_4",
    //    "ruined_portal/portal_5",
    //    "ruined_portal/portal_6",
    //    "ruined_portal/portal_7",
    //    "ruined_portal/portal_8",
    //    "ruined_portal/portal_9",
    //    "ruined_portal/portal_10"
    //];
    //private static readonly string[] STRUCTURE_LOCATION_GIANT_PORTALS = ["ruined_portal/giant_portal_1", "ruined_portal/giant_portal_2", "ruined_portal/giant_portal_3"];
    //private static readonly float PROBABILITY_OF_GIANT_PORTAL = 0.05F;
    //private static readonly int MIN_Y_INDEX = 15;

    //[JsonProperty("setups")]
    //public List<RuinedPortalStructure.Setup> Setups { get; set; }

    public RuinedPortalStructure()
        : base(new StructureSettings())
    {
    }

    public RuinedPortalStructure(StructureSettings settings/*, List<RuinedPortalStructure.Setup> p_229261_*/)
        : base(settings)
    {
        //Setups = p_229261_;
    }

    //public RuinedPortalStructure(StructureSettings settings, RuinedPortalStructure.Setup p_229258_)
    //    : this(settings, [.. p_229258_])
    //{
    //}

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_229285_)
    //{
    //    RuinedPortalPiece.Properties ruinedportalpiece$properties = new RuinedPortalPiece.Properties();
    //    WorldgenRandom worldgenrandom = p_229285_.random();
    //    RuinedPortalStructure.Setup ruinedportalstructure$setup = null;
    //    if (this.setups.size() > 1)
    //    {
    //        float f = 0.0F;

    //        for (RuinedPortalStructure.Setup ruinedportalstructure$setup1 : this.setups)
    //        {
    //            f += ruinedportalstructure$setup1.weight();
    //        }

    //        float f1 = worldgenrandom.nextFloat();

    //        for (RuinedPortalStructure.Setup ruinedportalstructure$setup2 : this.setups)
    //        {
    //            f1 -= ruinedportalstructure$setup2.weight() / f;
    //            if (f1 < 0.0F)
    //            {
    //                ruinedportalstructure$setup = ruinedportalstructure$setup2;
    //                break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        ruinedportalstructure$setup = this.setups.get(0);
    //    }

    //    if (ruinedportalstructure$setup == null)
    //    {
    //        throw new IllegalStateException();
    //    }
    //    else
    //    {
    //        RuinedPortalStructure.Setup ruinedportalstructure$setup3 = ruinedportalstructure$setup;
    //        ruinedportalpiece$properties.airPocket = sample(worldgenrandom, ruinedportalstructure$setup3.airPocketProbability());
    //        ruinedportalpiece$properties.mossiness = ruinedportalstructure$setup3.mossiness();
    //        ruinedportalpiece$properties.overgrown = ruinedportalstructure$setup3.overgrown();
    //        ruinedportalpiece$properties.vines = ruinedportalstructure$setup3.vines();
    //        ruinedportalpiece$properties.replaceWithBlackstone = ruinedportalstructure$setup3.replaceWithBlackstone();
    //        ResourceLocation resourcelocation;
    //        if (worldgenrandom.nextFloat() < PROBABILITY_OF_GIANT_PORTAL)
    //        {
    //            resourcelocation = ResourceLocation.withDefaultNamespace(STRUCTURE_LOCATION_GIANT_PORTALS[worldgenrandom.nextInt(STRUCTURE_LOCATION_GIANT_PORTALS.length)]);
    //        }
    //        else
    //        {
    //            resourcelocation = ResourceLocation.withDefaultNamespace(STRUCTURE_LOCATION_PORTALS[worldgenrandom.nextInt(STRUCTURE_LOCATION_PORTALS.length)]);
    //        }

    //        StructureTemplate structuretemplate = p_229285_.structureTemplateManager().getOrCreate(resourcelocation);
    //        Rotation rotation = Util.getRandom(Rotation.values(), worldgenrandom);
    //        Mirror mirror = worldgenrandom.nextFloat() < 0.5F ? Mirror.NONE : Mirror.FRONT_BACK;
    //        BlockPosition blockpos = new BlockPosition(structuretemplate.getSize().getX() / 2, 0, structuretemplate.getSize().getZ() / 2);
    //        ChunkGenerator chunkgenerator = p_229285_.chunkGenerator();
    //        LevelHeightAccessor levelheightaccessor = p_229285_.heightAccessor();
    //        RandomState randomstate = p_229285_.randomState();
    //        BlockPosition blockpos1 = p_229285_.chunkPos().getWorldPosition();
    //        BoundingBox boundingbox = structuretemplate.getBoundingBox(blockpos1, rotation, blockpos, mirror);
    //        BlockPosition blockpos2 = boundingbox.getCenter();
    //        int i = chunkgenerator.getBaseHeight(
    //                blockpos2.getX(),
    //                blockpos2.getZ(),
    //                RuinedPortalPiece.getHeightMapType(ruinedportalstructure$setup3.placement()),
    //                levelheightaccessor,
    //                randomstate
    //            )
    //            - 1;
    //        int j = findSuitableY(
    //            worldgenrandom,
    //            chunkgenerator,
    //            ruinedportalstructure$setup3.placement(),
    //            ruinedportalpiece$properties.airPocket,
    //            i,
    //            boundingbox.getYSpan(),
    //            boundingbox,
    //            levelheightaccessor,
    //            randomstate
    //        );

    //        BlockPosition blockpos3 = new BlockPosition(blockpos1.getX(), j, blockpos1.getZ());
    //        return Optional.of(
    //            new Structure.GenerationStub(
    //                blockpos3,
    //                p_229297_-> {
    //                    if (ruinedportalstructure$setup3.canBeCold())
    //                    {
    //                        ruinedportalpiece$properties.cold = isCold(
    //                        blockpos3,
    //                        p_229285_.chunkGenerator()
    //                            .getBiomeSource()
    //                            .getNoiseBiome(
    //                                QuartPos.fromBlock(blockpos3.getX()),
    //                                QuartPos.fromBlock(blockpos3.getY()),
    //                                QuartPos.fromBlock(blockpos3.getZ()),
    //                                randomstate.sampler()
    //                            ),
    //                        chunkgenerator.getSeaLevel());
    //                    }

    //                p_229297_.addPiece(
    //                    new RuinedPortalPiece(
    //                        p_229285_.structureTemplateManager(),
    //                        blockpos3,
    //                        ruinedportalstructure$setup3.placement(),
    //                        ruinedportalpiece$properties,
    //                        resourcelocation,
    //                        structuretemplate,
    //                        rotation,
    //                        mirror,
    //                        blockpos
    //                    ));
    //                }
    //            )
    //        );
    //    }
    //}

    private static bool sample(WorldgenRandom p_229282_, float p_229283_)
    {
        if (p_229283_ == 0.0F)
        {
            return false;
        }
        else
        {
            return p_229283_ == 1.0F ? true : p_229282_.NextFloat() < p_229283_;
        }
    }

    //private static bool isCold(BlockPosition p_229301_, Biome.Biome p_229302_, int p_361622_)
    //{
    //    return p_229302_.coldEnoughToSnow(p_229301_, p_361622_);
    //}

    //private static int findSuitableY(
    //    IRandomSource p_229267_,
    //    ChunkGenerator p_229268_,
    //    RuinedPortalPiece.VerticalPlacement p_229269_,
    //    bool p_229270_,
    //    int p_229271_,
    //    int p_229272_,
    //    BoundingBox p_229273_,
    //    LevelHeightAccessor p_229274_,
    //    RandomState p_229275_
    //)
    //{
    //    int j = p_229274_.GetMinY() + MIN_Y_INDEX;
    //    int i;
    //    if (p_229269_ == RuinedPortalPiece.VerticalPlacement.IN_NETHER)
    //    {
    //        if (p_229270_)
    //        {
    //            i = Mth.randomBetweenInclusive(p_229267_, 32, 100);
    //        }
    //        else if (p_229267_.NextFloat() < 0.5F)
    //        {
    //            i = Mth.randomBetweenInclusive(p_229267_, 27, 29);
    //        }
    //        else
    //        {
    //            i = Mth.randomBetweenInclusive(p_229267_, 29, 100);
    //        }
    //    }
    //    else if (p_229269_ == RuinedPortalPiece.VerticalPlacement.IN_MOUNTAIN)
    //    {
    //        int k = p_229271_ - p_229272_;
    //        i = getRandomWithinInterval(p_229267_, 70, k);
    //    }
    //    else if (p_229269_ == RuinedPortalPiece.VerticalPlacement.UNDERGROUND)
    //    {
    //        int j1 = p_229271_ - p_229272_;
    //        i = getRandomWithinInterval(p_229267_, j, j1);
    //    }
    //    else if (p_229269_ == RuinedPortalPiece.VerticalPlacement.PARTLY_BURIED)
    //    {
    //        i = p_229271_ - p_229272_ + Mth.randomBetweenInclusive(p_229267_, 2, 8);
    //    }
    //    else
    //    {
    //        i = p_229271_;
    //    }

    //    List<BlockPosition> list1 = ImmutableList.of(
    //        new BlockPosition(p_229273_.MinX, 0, p_229273_.MinZ),
    //        new BlockPosition(p_229273_.MaxX, 0, p_229273_.MinZ),
    //        new BlockPosition(p_229273_.MinX, 0, p_229273_.MaxZ),
    //        new BlockPosition(p_229273_.MaxX, 0, p_229273_.MaxZ)
    //    );
    //    List<NoiseColumn> list = list1.stream()
    //        .map(p_229280_->p_229268_.getBaseColumn(p_229280_.getX(), p_229280_.getZ(), p_229274_, p_229275_))
    //        .collect(Collectors.toList());
    //    Heightmap.Types heightmap$types = p_229269_ == RuinedPortalPiece.VerticalPlacement.ON_OCEAN_FLOOR
    //        ? Heightmap.Types.OCEAN_FLOOR_WG
    //        : Heightmap.Types.WORLD_SURFACE_WG;

    //    int l;
    //    for (l = i; l > j; l--)
    //    {
    //        int i1 = 0;

    //        foreach (NoiseColumn noisecolumn in list)
    //        {
    //            BlockState blockstate = noisecolumn.getBlock(l);
    //            if (heightmap$types.isOpaque().test(blockstate))
    //            {
    //                if (++i1 == 3)
    //                {
    //                    return l;
    //                }
    //            }
    //        }
    //    }

    //    return l;
    //}

    private static int getRandomWithinInterval(IRandomSource p_229263_, int p_229264_, int p_229265_)
    {
        return p_229264_ < p_229265_ ? Mth.randomBetweenInclusive(p_229263_, p_229264_, p_229265_) : p_229265_;
    }
}
