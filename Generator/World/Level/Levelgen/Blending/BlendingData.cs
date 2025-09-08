using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Blending;

//source: net.minecraft.world.level.levelgen.blending.BlendingData
public class BlendingData
{
    private static readonly double BLENDING_DENSITY_FACTOR = 0.1;
    protected static readonly int CELL_WIDTH = 4;
    protected static readonly int CELL_HEIGHT = 8;
    protected static readonly int CELL_RATIO = 2;
    //private static readonly double SOLID_DENSITY = 1.0;
    //private static readonly double AIR_DENSITY = -1.0;
    private static readonly int CELLS_PER_SECTION_Y = 2;
    private static readonly int QUARTS_PER_SECTION = QuartPosition.FromBlock(16);
    private static readonly int CELL_HORIZONTAL_MAX_INDEX_INSIDE = QUARTS_PER_SECTION - 1;
    private static readonly int CELL_HORIZONTAL_MAX_INDEX_OUTSIDE = QUARTS_PER_SECTION;
    private static readonly int CELL_COLUMN_INSIDE_COUNT = 2 * CELL_HORIZONTAL_MAX_INDEX_INSIDE + 1;
    private static readonly int CELL_COLUMN_OUTSIDE_COUNT = 2 * CELL_HORIZONTAL_MAX_INDEX_OUTSIDE + 1;
    static readonly int CELL_COLUMN_COUNT = CELL_COLUMN_INSIDE_COUNT + CELL_COLUMN_OUTSIDE_COUNT;
    public ILevelHeightAccessor AreaWithOldGeneration { get; private set; }
    //private static readonly List<BlockType> SURFACE_BLOCKS =
    //[
    //    BlockType.PODZOL,
    //    BlockType.GRAVEL,
    //    BlockType.GRASS_BLOCK,
    //    BlockType.STONE,
    //    BlockType.COARSE_DIRT,
    //    BlockType.SAND,
    //    BlockType.RED_SAND,
    //    BlockType.MYCELIUM,
    //    BlockType.SNOW_BLOCK,
    //    BlockType.TERRACOTTA,
    //    BlockType.DIRT
    //];
    protected static readonly double NO_VALUE = double.MaxValue;
    //private bool hasCalculatedData;
    private readonly double[] heights;
    //private readonly List<List<Biome>> biomes;
    private readonly double[][] densities; // transient

    private BlendingData(int p_224740_, int p_224741_, double[]? p_224742_)
    {
        if (p_224742_ == null)
        {
            heights = new double[CELL_COLUMN_COUNT];
            Array.Fill(heights, double.MaxValue);
        }
        else
        {
            heights = p_224742_;
        }
        densities = new double[CELL_COLUMN_COUNT][];

        //ObjectArrayList<List<Holder<Biome>>> objectarraylist = new ObjectArrayList<>(CELL_COLUMN_COUNT);
        //objectarraylist.size(CELL_COLUMN_COUNT);
        //this.biomes = new List<List<Biome>>(CELL_COLUMN_COUNT); // objectarraylist;

        int minY = SectionPosition.SectionToBlockCoord(p_224740_);
        int height = SectionPosition.SectionToBlockCoord(p_224741_) - minY;
        AreaWithOldGeneration = LevelHeightAccessor.Create(minY, height);
    }

    public static BlendingData? Unpack([AllowNull] BlendingDataPacked dataPacked)
    {
        return dataPacked == null ? null : new BlendingData(dataPacked.MinSection, dataPacked.MaxSection, dataPacked.Heights);
    }

    public BlendingDataPacked Pack()
    {
        bool flag = heights.Any(h => h != double.MaxValue);
        return new BlendingDataPacked
        {
            MinSection = AreaWithOldGeneration.GetMinSectionY(),
            MaxSection = AreaWithOldGeneration.GetMaxSectionY() + 1,
            Heights = flag ? (double[])heights.Clone() : null
        };
    }

    //public static BlendingData? getOrUpdateBlendingData(WorldGenRegion p_190305_, int p_190306_, int p_190307_)
    //{
    //    ChunkAccess chunkaccess = p_190305_.getChunk(p_190306_, p_190307_);
    //    BlendingData blendingdata = chunkaccess.getBlendingData();
    //    if (blendingdata != null && !chunkaccess.getHighestGeneratedStatus().isBefore(ChunkStatus.BIOMES))
    //    {
    //        blendingdata.calculateData(chunkaccess, sideByGenerationAge(p_190305_, p_190306_, p_190307_, false));
    //        return blendingdata;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //public static Set<Direction8> sideByGenerationAge(WorldGenLevel p_197066_, int p_197067_, int p_197068_, bool p_197069_)
    //{
    //    Set<Direction8> set = EnumSet.noneOf(Direction8.class);

    //    for (Direction8 direction8 : Direction8.values())
    //    {
    //        int i = p_197067_ + direction8.getStepX();
    //        int j = p_197068_ + direction8.getStepZ();
    //        if (p_197066_.getChunk(i, j).isOldNoiseGeneration() == p_197069_)
    //        {
    //            set.add(direction8);
    //        }
    //    }

    //    return set;
    //}

    //private void calculateData(ChunkAccess p_190318_, Set<Direction8> p_190319_)
    //{
    //    if (!this.hasCalculatedData)
    //    {
    //        if (p_190319_.contains(Direction8.NORTH) || p_190319_.contains(Direction8.WEST) || p_190319_.contains(Direction8.NORTH_WEST))
    //        {
    //            this.addValuesForColumn(getInsideIndex(0, 0), p_190318_, 0, 0);
    //        }

    //        if (p_190319_.contains(Direction8.NORTH))
    //        {
    //            for (int i = 1; i < QUARTS_PER_SECTION; i++)
    //            {
    //                this.addValuesForColumn(getInsideIndex(i, 0), p_190318_, 4 * i, 0);
    //            }
    //        }

    //        if (p_190319_.contains(Direction8.WEST))
    //        {
    //            for (int j = 1; j < QUARTS_PER_SECTION; j++)
    //            {
    //                this.addValuesForColumn(getInsideIndex(0, j), p_190318_, 0, 4 * j);
    //            }
    //        }

    //        if (p_190319_.contains(Direction8.EAST))
    //        {
    //            for (int k = 1; k < QUARTS_PER_SECTION; k++)
    //            {
    //                this.addValuesForColumn(getOutsideIndex(CELL_HORIZONTAL_MAX_INDEX_OUTSIDE, k), p_190318_, 15, 4 * k);
    //            }
    //        }

    //        if (p_190319_.contains(Direction8.SOUTH))
    //        {
    //            for (int l = 0; l < QUARTS_PER_SECTION; l++)
    //            {
    //                this.addValuesForColumn(getOutsideIndex(l, CELL_HORIZONTAL_MAX_INDEX_OUTSIDE), p_190318_, 4 * l, 15);
    //            }
    //        }

    //        if (p_190319_.contains(Direction8.EAST) && p_190319_.contains(Direction8.NORTH_EAST))
    //        {
    //            this.addValuesForColumn(getOutsideIndex(CELL_HORIZONTAL_MAX_INDEX_OUTSIDE, 0), p_190318_, 15, 0);
    //        }

    //        if (p_190319_.contains(Direction8.EAST) && p_190319_.contains(Direction8.SOUTH) && p_190319_.contains(Direction8.SOUTH_EAST))
    //        {
    //            this.addValuesForColumn(getOutsideIndex(CELL_HORIZONTAL_MAX_INDEX_OUTSIDE, CELL_HORIZONTAL_MAX_INDEX_OUTSIDE), p_190318_, 15, 15);
    //        }

    //        this.hasCalculatedData = true;
    //    }
    //}

    //private void addValuesForColumn(int p_190300_, ChunkAccess p_190301_, int p_190302_, int p_190303_)
    //{
    //    if (this.heights[p_190300_] == double.MaxValue)
    //    {
    //        this.heights[p_190300_] = this.getHeightAtXZ(p_190301_, p_190302_, p_190303_);
    //    }

    //    this.densities[p_190300_] = this.getDensityColumn(p_190301_, p_190302_, p_190303_, Mth.floor(this.heights[p_190300_]));
    //    this.biomes.set(p_190300_, this.getBiomeColumn(p_190301_, p_190302_, p_190303_));
    //}

    //private int getHeightAtXZ(ChunkAccess p_190311_, int p_190312_, int p_190313_)
    //{
    //    int i;
    //    if (p_190311_.hasPrimedHeightmap(Heightmap.Types.WORLD_SURFACE_WG))
    //    {
    //        i = Math.Min(p_190311_.getHeight(Heightmap.Types.WORLD_SURFACE_WG, p_190312_, p_190313_), AreaWithOldGeneration.GetMaxY());
    //    }
    //    else
    //    {
    //        i = AreaWithOldGeneration.GetMaxY();
    //    }

    //    int j = AreaWithOldGeneration.GetMinY();
    //    BlockPosition.MutableBlockPos blockpos$mutableblockpos = new BlockPosition.MutableBlockPos(p_190312_, i, p_190313_);

    //    while (blockpos$mutableblockpos.getY() > j)
    //    {
    //        if (SURFACE_BLOCKS.contains(p_190311_.getBlockState(blockpos$mutableblockpos).getBlock()))
    //        {
    //            return blockpos$mutableblockpos.getY();
    //        }

    //        blockpos$mutableblockpos.move(Direction.DOWN);
    //    }

    //    return j;
    //}

    //private static double read1(ChunkAccess p_198298_, BlockPosition.MutableBlockPos p_198299_)
    //{
    //    return isGround(p_198298_, p_198299_.move(Direction.DOWN)) ? 1.0 : -1.0;
    //}

    //private static double read7(ChunkAccess p_198301_, BlockPosition.MutableBlockPos p_198302_)
    //{
    //    double d0 = 0.0;

    //    for (int i = 0; i < 7; i++)
    //    {
    //        d0 += read1(p_198301_, p_198302_);
    //    }

    //    return d0;
    //}

    //private double[] getDensityColumn(ChunkAccess p_198293_, int p_198294_, int p_198295_, int p_198296_)
    //{
    //    double[] adouble = new double[this.cellCountPerColumn()];
    //    Array.Fill(adouble, -1.0);
    //    BlockPosition.MutableBlockPos blockpos$mutableblockpos = new BlockPosition.MutableBlockPos(p_198294_, AreaWithOldGeneration.GetMaxY() + 1, p_198295_);
    //    double d0 = read7(p_198293_, blockpos$mutableblockpos);

    //    for (int i = adouble.Length - 2; i >= 0; i--)
    //    {
    //        double d1 = read1(p_198293_, blockpos$mutableblockpos);
    //        double d2 = read7(p_198293_, blockpos$mutableblockpos);
    //        adouble[i] = (d0 + d1 + d2) / 15.0;
    //        d0 = d2;
    //    }

    //    int j = this.getCellYIndex(Mth.floorDiv(p_198296_, 8));
    //    if (j >= 0 && j < adouble.Length - 1)
    //    {
    //        double d4 = (p_198296_ + 0.5) % 8.0 / 8.0;
    //        double d5 = (1.0 - d4) / d4;
    //        double d3 = Math.Max(d5, 1.0) * 0.25;
    //        adouble[j + 1] = -d5 / d3;
    //        adouble[j] = 1.0 / d3;
    //    }

    //    return adouble;
    //}

    //private List<Holder<Biome>> getBiomeColumn(ChunkAccess p_224758_, int p_224759_, int p_224760_)
    //{
    //    ObjectArrayList<Holder<Biome>> objectarraylist = new ObjectArrayList<>(this.quartCountPerColumn());
    //    objectarraylist.size(this.quartCountPerColumn());

    //    for (int i = 0; i < objectarraylist.size(); i++)
    //    {
    //        int j = i + QuartPosition.FromBlock(AreaWithOldGeneration.GetMinY());
    //        objectarraylist.set(i, p_224758_.getNoiseBiome(QuartPosition.FromBlock(p_224759_), j, QuartPosition.FromBlock(p_224760_)));
    //    }

    //    return objectarraylist;
    //}

    //private static bool isGround(ChunkAccess p_190315_, BlockPosition p_190316_)
    //{
    //    BlockState blockstate = p_190315_.getBlockState(p_190316_);
    //    if (blockstate.isAir())
    //    {
    //        return false;
    //    }
    //    else if (blockstate.is (BlockTags.LEAVES))
    //    {
    //        return false;
    //    }
    //    else if (blockstate.is (BlockTags.LOGS))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return blockstate.is (Blocks.BROWN_MUSHROOM_BLOCK) || blockstate.is (Blocks.RED_MUSHROOM_BLOCK) ? false : !blockstate.getCollisionShape(p_190315_, p_190316_).isEmpty();
    //    }
    //}

    public double GetHeight(int p_190286_, int p_190287_, int p_190288_)
    {
        if (p_190286_ == CELL_HORIZONTAL_MAX_INDEX_OUTSIDE || p_190288_ == CELL_HORIZONTAL_MAX_INDEX_OUTSIDE)
        {
            return heights[getOutsideIndex(p_190286_, p_190288_)];
        }
        else
        {
            return p_190286_ != 0 && p_190288_ != 0 ? double.MaxValue : heights[getInsideIndex(p_190286_, p_190288_)];
        }
    }

    private double getDensity(double[]? p_190325_, int p_190326_)
    {
        if (p_190325_ == null)
        {
            return double.MaxValue;
        }
        else
        {
            int i = this.getCellYIndex(p_190326_);
            return i >= 0 && i < p_190325_.Length ? p_190325_[i] * BLENDING_DENSITY_FACTOR : double.MaxValue;
        }
    }

    public double GetDensity(int p_190334_, int p_190335_, int p_190336_)
    {
        if (p_190335_ == this.getMinY())
        {
            return BLENDING_DENSITY_FACTOR;
        }
        else if (p_190334_ == CELL_HORIZONTAL_MAX_INDEX_OUTSIDE || p_190336_ == CELL_HORIZONTAL_MAX_INDEX_OUTSIDE)
        {
            return getDensity(this.densities[getOutsideIndex(p_190334_, p_190336_)], p_190335_);
        }
        else
        {
            return p_190334_ != 0 && p_190336_ != 0 ? double.MaxValue : getDensity(this.densities[getInsideIndex(p_190334_, p_190336_)], p_190335_);
        }
    }

    //protected void iterateBiomes(int p_224749_, int p_224750_, int p_224751_, BlendingData.BiomeConsumer p_224752_)
    //{
    //    if (p_224750_ >= QuartPosition.FromBlock(AreaWithOldGeneration.GetMinY()) && p_224750_ <= QuartPosition.FromBlock(AreaWithOldGeneration.GetMaxY()))
    //    {
    //        int i = p_224750_ - QuartPosition.FromBlock(AreaWithOldGeneration.GetMinY());

    //        for (int j = 0; j < this.biomes.size(); j++)
    //        {
    //            if (this.biomes.get(j) != null)
    //            {
    //                Holder<Biome> holder = this.biomes.get(j).get(i);
    //                if (holder != null)
    //                {
    //                    p_224752_.consume(p_224749_ + getX(j), p_224751_ + getZ(j), holder);
    //                }
    //            }
    //        }
    //    }
    //}

    public void IterateHeights(int p_190296_, int p_190297_, Action<int, int, double> heightConsumer)
    {
        for (int i = 0; i < heights.Length; i++)
        {
            double d0 = heights[i];
            if (d0 != double.MaxValue)
            {
                heightConsumer(p_190296_ + getX(i), p_190297_ + getZ(i), d0);
            }
        }
    }

    public void IterateDensities(int p_190290_, int p_190291_, int p_190292_, int p_190293_, Action<int, int, int, double> densityConsumer)
    {
        int i = getColumnMinY();
        int j = Math.Max(0, p_190292_ - i);
        int k = Math.Min(cellCountPerColumn(), p_190293_ - i);

        for (int l = 0; l < this.densities.Length; l++)
        {
            double[] adouble = this.densities[l];
            if (adouble != null)
            {
                int i1 = p_190290_ + getX(l);
                int j1 = p_190291_ + getZ(l);

                for (int k1 = j; k1 < k; k1++)
                {
                    densityConsumer(i1, k1 + i, j1, adouble[k1] * BLENDING_DENSITY_FACTOR);
                }
            }
        }
    }

    private int cellCountPerColumn()
    {
        return AreaWithOldGeneration.GetSectionsCount() * CELLS_PER_SECTION_Y;
    }

    private int quartCountPerColumn()
    {
        return QuartPosition.FromSection(AreaWithOldGeneration.GetSectionsCount());
    }

    private int getColumnMinY()
    {
        return getMinY() + 1;
    }

    private int getMinY()
    {
        return AreaWithOldGeneration.GetMinSectionY() * 2;
    }

    private int getCellYIndex(int p_224747_)
    {
        return p_224747_ - getColumnMinY();
    }

    private static int getInsideIndex(int p_190331_, int p_190332_)
    {
        return CELL_HORIZONTAL_MAX_INDEX_INSIDE - p_190331_ + p_190332_;
    }

    private static int getOutsideIndex(int p_190351_, int p_190352_)
    {
        return CELL_COLUMN_INSIDE_COUNT + p_190351_ + CELL_HORIZONTAL_MAX_INDEX_OUTSIDE - p_190352_;
    }

    private static int getX(int p_190349_)
    {
        if (p_190349_ < CELL_COLUMN_INSIDE_COUNT)
        {
            return zeroIfNegative(CELL_HORIZONTAL_MAX_INDEX_INSIDE - p_190349_);
        }
        else
        {
            int i = p_190349_ - CELL_COLUMN_INSIDE_COUNT;
            return CELL_HORIZONTAL_MAX_INDEX_OUTSIDE - zeroIfNegative(CELL_HORIZONTAL_MAX_INDEX_OUTSIDE - i);
        }
    }

    private static int getZ(int p_190355_)
    {
        if (p_190355_ < CELL_COLUMN_INSIDE_COUNT)
        {
            return zeroIfNegative(p_190355_ - CELL_HORIZONTAL_MAX_INDEX_INSIDE);
        }
        else
        {
            int i = p_190355_ - CELL_COLUMN_INSIDE_COUNT;
            return CELL_HORIZONTAL_MAX_INDEX_OUTSIDE - zeroIfNegative(i - CELL_HORIZONTAL_MAX_INDEX_OUTSIDE);
        }
    }

    private static int zeroIfNegative(int p_190357_)
    {
        return p_190357_ & ~(p_190357_ >> 31);
    }
}
