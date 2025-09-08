using Generator.Core;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.BiomeSource
public abstract class BiomeSource : IBiomeResolver
{
    public HashSet<Biome> PossibleBiomes { get; private set; }
    //private readonly Supplier<HashSet<Biome>> possibleBiomes = Suppliers.memoize(()-> this.collectPossibleBiomes().distinct().collect(ImmutableSet.toImmutableSet()));
    //public HashSet<Biome> possibleBiomes() { return this.possibleBiomes.get(); }

    protected BiomeSource()
    {
        PossibleBiomes = new Lazy<HashSet<Biome>>(() => collectPossibleBiomes().Distinct().ToHashSet()).Value;
    }

    protected abstract IEnumerable<Biome> collectPossibleBiomes();

    public HashSet<Biome> getBiomesWithin(int p_186705_, int p_186706_, int p_186707_, int p_186708_, ClimateSampler p_186709_)
    {
        int i = QuartPosition.FromBlock(p_186705_ - p_186708_);
        int j = QuartPosition.FromBlock(p_186706_ - p_186708_);
        int k = QuartPosition.FromBlock(p_186707_ - p_186708_);
        int l = QuartPosition.FromBlock(p_186705_ + p_186708_);
        int i1 = QuartPosition.FromBlock(p_186706_ + p_186708_);
        int j1 = QuartPosition.FromBlock(p_186707_ + p_186708_);
        int k1 = l - i + 1;
        int l1 = i1 - j + 1;
        int i2 = j1 - k + 1;
        HashSet<Biome> set = new HashSet<Biome>();

        for (int j2 = 0; j2 < i2; j2++)
        {
            for (int k2 = 0; k2 < k1; k2++)
            {
                for (int l2 = 0; l2 < l1; l2++)
                {
                    int i3 = i + k2;
                    int j3 = j + l2;
                    int k3 = k + j2;
                    set.Add(this.GetNoiseBiome(i3, j3, k3, p_186709_));
                }
            }
        }

        return set;
    }

    public Tuple<BlockPosition, Biome>? findBiomeHorizontal(int p_220571_, int p_220572_, int p_220573_, int p_220574_, Predicate<Biome> p_220575_, IRandomSource p_220576_, ClimateSampler p_220577_)
    {
        return this.findBiomeHorizontal(p_220571_, p_220572_, p_220573_, p_220574_, 1, p_220575_, p_220576_, false, p_220577_);
    }

    //public Tuple<BlockPosition, Biome>? findClosestBiome3d(BlockPosition p_220578_, int p_220579_, int p_220580_, int p_220581_, Predicate<Biome> p_220582_, ClimateSampler p_220583_, LevelReader p_220584_)
    //{
    //    HashSet<Biome> set = this.possibleBiomes().stream().filter(p_220582_).collect(Collectors.toUnmodifiableSet());
    //    if (!set.Any())
    //    {
    //        return null;
    //    }
    //    else
    //    {
    //        int i = (int) Math.Floor((decimal)p_220579_ / p_220580_);
    //        int[] aint = Mth.outFromOrigin(p_220578_.Y, p_220584_.getMinY() + 1, p_220584_.getMaxY() + 1, p_220581_).toArray();

    //        foreach (BlockPosition.MutableBlockPos mutableblockpos in BlockPosition.spiralAround(BlockPosition.ZERO, i, DirectionType.EAST, DirectionType.SOUTH))
    //        {
    //            int j = p_220578_.X + mutableblockpos.X * p_220580_;
    //            int k = p_220578_.Z + mutableblockpos.Z * p_220580_;
    //            int l = QuartPosition.FromBlock(j);
    //            int i1 = QuartPosition.FromBlock(k);

    //            foreach (int j1 in aint)
    //            {
    //                int k1 = QuartPosition.FromBlock(j1);
    //                Biome holder = this.GetNoiseBiome(l, k1, i1, p_220583_);
    //                if (set.Contains(holder))
    //                {
    //                    return new Tuple<BlockPosition, Biome>(new BlockPosition(j, j1, k), holder);
    //                }
    //            }
    //        }

    //        return null;
    //    }
    //}

    public Tuple<BlockPosition, Biome> findBiomeHorizontal(
        int p_220561_,
        int p_220562_,
        int p_220563_,
        int p_220564_,
        int p_220565_,
        Predicate<Biome> p_220566_,
        IRandomSource p_220567_,
        bool p_220568_,
        ClimateSampler p_220569_
    )
    {
        int i = QuartPosition.FromBlock(p_220561_);
        int j = QuartPosition.FromBlock(p_220563_);
        int k = QuartPosition.FromBlock(p_220564_);
        int l = QuartPosition.FromBlock(p_220562_);
        Tuple<BlockPosition, Biome> pair = null;
        int i1 = 0;
        int j1 = p_220568_ ? 0 : k;
        int k1 = j1;

        while (k1 <= k)
        {
            for (int l1 = SharedConstants.debugGenerateSquareTerrainWithoutNoise ? 0 : -k1; l1 <= k1; l1 += p_220565_)
            {
                bool flag = Math.Abs(l1) == k1;

                for (int i2 = -k1; i2 <= k1; i2 += p_220565_)
                {
                    if (p_220568_)
                    {
                        bool flag1 = Math.Abs(i2) == k1;
                        if (!flag1 && !flag)
                        {
                            continue;
                        }
                    }

                    int k2 = i + i2;
                    int j2 = j + l1;
                    Biome holder = this.GetNoiseBiome(k2, l, j2, p_220569_);
                    if (p_220566_(holder))
                    {
                        if (pair == null || p_220567_.NextInt(i1 + 1) == 0)
                        {
                            BlockPosition blockpos = new BlockPosition(QuartPosition.ToBlock(k2), p_220562_, QuartPosition.ToBlock(j2));
                            if (p_220568_)
                            {
                                return new Tuple<BlockPosition, Biome>(blockpos, holder);
                            }

                            pair = new Tuple<BlockPosition, Biome>(blockpos, holder);
                        }

                        i1++;
                    }
                }
            }

            k1 += p_220565_;
        }

        return pair;
    }

    public abstract Biome GetNoiseBiome(int p_204221_, int p_204222_, int p_204223_, ClimateSampler p_204224_);
}
