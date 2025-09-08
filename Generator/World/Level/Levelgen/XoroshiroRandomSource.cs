using Generator.Helpers;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.XoroshiroRandomSource
public class XoroshiroRandomSource : IRandomSource
{
    private static readonly float FLOAT_UNIT = 5.9604645E-8F;
    private static readonly double DOUBLE_UNIT = 1.110223E-16F;

    private Xoroshiro128PlusPlus randomNumberGenerator;
    private readonly MarsagliaPolarGaussian gaussianSource;

    public XoroshiroRandomSource(long seed)
    {
        randomNumberGenerator = new Xoroshiro128PlusPlus(RandomSupport.UpgradeSeedTo128bit(seed));
        gaussianSource = new MarsagliaPolarGaussian(this);
    }

    public XoroshiroRandomSource(Seed128bit seed128)
    {
        randomNumberGenerator = new Xoroshiro128PlusPlus(seed128);
        gaussianSource = new MarsagliaPolarGaussian(this);
    }

    public XoroshiroRandomSource(long seedLo, long seedHi)
    {
        randomNumberGenerator = new Xoroshiro128PlusPlus(seedLo, seedHi);
        gaussianSource = new MarsagliaPolarGaussian(this);
    }

    private XoroshiroRandomSource(Xoroshiro128PlusPlus numberGenerator)
    {
        randomNumberGenerator = numberGenerator;
        gaussianSource = new MarsagliaPolarGaussian(this);
    }

    public IRandomSource Fork()
    {
        return new XoroshiroRandomSource(randomNumberGenerator.NextLong(), randomNumberGenerator.NextLong());
    }

    public IPositionalRandomFactory ForkPositional()
    {
        return new XoroshiroPositionalRandomFactory(randomNumberGenerator.NextLong(), randomNumberGenerator.NextLong());
    }

    public void SetSeed(long seed)
    {
        randomNumberGenerator = new Xoroshiro128PlusPlus(RandomSupport.UpgradeSeedTo128bit(seed));
        gaussianSource.Reset();
    }

    public int NextInt()
    {
        return (int)randomNumberGenerator.NextLong();
    }

    public int NextInt(int bound)
    {
        if (bound <= 0)
        {
            throw new ArgumentException("Bound must be positive");
        }
        else
        {
            long i = IntegerHelper.JavaUnsignedLong(NextInt());
            long j = i * bound;
            long k = j & 4294967295L;
            if (k < bound)
            {
                for (int l = (int)(IntegerHelper.JavaUnsignedLong(~bound + 1) % IntegerHelper.JavaUnsignedLong(bound)); k < l; k = j & 4294967295L)
                {
                    i = IntegerHelper.JavaUnsignedLong(NextInt());
                    j = i * bound;
                }
            }

            long i1 = j >> 32;
            return (int)i1;
        }
    }

    public long NextLong()
    {
        return randomNumberGenerator.NextLong();
    }

    public bool NextBoolean()
    {
        return (randomNumberGenerator.NextLong() & 1L) != 0L;
    }

    public float NextFloat()
    {
        return nextBits(24) * FLOAT_UNIT;
    }

    public double NextDouble()
    {
        return nextBits(53) * DOUBLE_UNIT;
    }

    public double NextGaussian()
    {
        return gaussianSource.NextGaussian();
    }

    public void ConsumeCount(int numbersCount)
    {
        for (int i = 0; i < numbersCount; i++)
        {
            randomNumberGenerator.NextLong();
        }
    }

    private long nextBits(int bitsCount)
    {
        return randomNumberGenerator.NextLong() >>> 64 - bitsCount;
    }
}
