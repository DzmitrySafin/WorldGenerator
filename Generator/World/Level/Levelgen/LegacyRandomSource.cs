using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.LegacyRandomSource
public class LegacyRandomSource : IBitRandomSource
{
    private static readonly int MODULUS_BITS = 48;
    private static readonly long MODULUS_MASK = 281474976710655L;
    private static readonly long MULTIPLIER = 25214903917L;
    private static readonly long INCREMENT = 11L;
    private long seed;
    private readonly MarsagliaPolarGaussian gaussianSource;

    public LegacyRandomSource(long seed)
    {
        gaussianSource = new MarsagliaPolarGaussian(this);
        SetSeed(seed);
    }

    public IRandomSource Fork()
    {
        return new LegacyRandomSource(NextLong());
    }

    public IPositionalRandomFactory ForkPositional()
    {
        return new LegacyPositionalRandomFactory(NextLong());
    }

    public void SetSeed(long seed)
    {
        //Interlocked.CompareExchange<long>(ref seed, (seed ^ MULTIPLIER) & MODULUS_MASK, seed);
        Interlocked.Exchange<long>(ref seed, (seed ^ MULTIPLIER) & MODULUS_MASK);
        gaussianSource.Reset();
        //if (!seed.compareAndSet(seed.get(), (seed ^ MULTIPLIER) & MODULUS_MASK))
        //{
        //    throw ThreadingDetector.makeThreadingException("LegacyRandomSource", null);
        //}
        //else
        //{
        //    gaussianSource.Reset();
        //}
    }

    public int Next(int bound)
    {
        long i = seed;
        long j = i * MULTIPLIER + INCREMENT & MODULUS_MASK;
        //Interlocked.CompareExchange<long>(ref seed, j, i);
        Interlocked.Exchange<long>(ref seed, j);
        return (int)(j >> MODULUS_BITS - bound);
        //if (!seed.compareAndSet(i, j))
        //{
        //    throw ThreadingDetector.makeThreadingException("LegacyRandomSource", null);
        //}
        //else
        //{
        //    return (int)(j >> MODULUS_BITS - bound);
        //}
    }

    public int NextInt()
    {
        return ((IBitRandomSource)this).NextInt();
    }

    public int NextInt(int bound)
    {
        return ((IBitRandomSource)this).NextInt(bound);
    }

    public double NextGaussian()
    {
        return gaussianSource.NextGaussian();
    }

    public long NextLong()
    {
        return ((IBitRandomSource)this).NextLong();
    }

    public bool NextBoolean()
    {
        return ((IBitRandomSource)this).NextBoolean();
    }

    public float NextFloat()
    {
        return ((IBitRandomSource)this).NextFloat();
    }

    public double NextDouble()
    {
        return ((IBitRandomSource)this).NextDouble();
    }
}
