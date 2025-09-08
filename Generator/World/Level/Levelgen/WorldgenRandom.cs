using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.WorldgenRandom
public class WorldgenRandom : LegacyRandomSource
{
    private readonly IRandomSource randomSource;
    public int Count { get; set; }

    public WorldgenRandom(IRandomSource p_224680_)
        : base(0L)
    {
        randomSource = p_224680_;
    }

    public new IRandomSource Fork()
    {
        return randomSource.Fork();
    }

    public new IPositionalRandomFactory ForkPositional()
    {
        return randomSource.ForkPositional();
    }

    public new int Next(int p_64708_)
    {
        Count++;
        return randomSource is LegacyRandomSource legacyrandomsource
            ? legacyrandomsource.Next(p_64708_)
            : (int)(randomSource.NextLong() >>> 64 - p_64708_);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public new void SetSeed(long p_190073_)
    {
        if (randomSource != null)
        {
            randomSource.SetSeed(p_190073_);
        }
    }

    public long SetDecorationSeed(long p_64691_, int p_64692_, int p_64693_)
    {
        SetSeed(p_64691_);
        long i = NextLong() | 1L;
        long j = NextLong() | 1L;
        long k = p_64692_ * i + p_64693_ * j ^ p_64691_;
        SetSeed(k);
        return k;
    }

    public void SetFeatureSeed(long p_190065_, int p_190066_, int p_190067_)
    {
        long i = p_190065_ + p_190066_ + 10000 * p_190067_;
        SetSeed(i);
    }

    public void SetLargeFeatureSeed(long p_190069_, int p_190070_, int p_190071_)
    {
        SetSeed(p_190069_);
        long i = NextLong();
        long j = NextLong();
        long k = p_190070_ * i ^ p_190071_ * j ^ p_190069_;
        SetSeed(k);
    }

    public void SetLargeFeatureWithSalt(long p_190059_, int p_190060_, int p_190061_, int p_190062_)
    {
        long i = p_190060_ * 341873128712L + p_190061_ * 132897987541L + p_190059_ + p_190062_;
        SetSeed(i);
    }

    public static IRandomSource SeedSlimeChunk(int p_224682_, int p_224683_, long p_224684_, long p_224685_)
    {
        return IRandomSource.Create(p_224684_ + p_224682_ * p_224682_ * 4987142 + p_224682_ * 5947611 + p_224683_ * p_224683_ * 4392871L + p_224683_ * 389711 ^ p_224685_);
    }
}
