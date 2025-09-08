using Generator.World.Level.Levelgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Util;

//source: net.minecraft.util.RandomSource
public interface IRandomSource
{
    //[Obsolete]
    //double GAUSSIAN_SPREAD_FACTOR = 2.297;

    public static IRandomSource Create()
    {
        return Create(RandomSupport.GenerateUniqueSeed());
    }

    //[Obsolete]
    //static IRandomSource createThreadSafe()
    //{
    //    return new ThreadSafeLegacyRandomSource(RandomSupport.GenerateUniqueSeed());
    //}

    static IRandomSource Create(long seed)
    {
        return new LegacyRandomSource(seed);
    }

    //static IRandomSource createNewThreadLocalInstance()
    //{
    //    return new SingleThreadedRandomSource(ThreadLocalRandom.current().nextLong());
    //}

    IRandomSource Fork();

    IPositionalRandomFactory ForkPositional();

    void SetSeed(long seed);

    int NextInt();

    int NextInt(int bound);

    int NextIntBetweenInclusive(int boundLo, int boundHi)
    {
        return NextInt(boundHi - boundLo + 1) + boundLo;
    }

    long NextLong();

    bool NextBoolean();

    float NextFloat();

    double NextDouble();

    double NextGaussian();

    double Triangle(double boundLo, double boundHi)
    {
        return boundLo + boundHi * (NextDouble() - NextDouble());
    }

    float Triangle(float boundLo, float boundHi)
    {
        return boundLo + boundHi * (NextFloat() - NextFloat());
    }

    void ConsumeCount(int numbersCount)
    {
        for (int i = 0; i < numbersCount; i++)
        {
            NextInt();
        }
    }

    int NextInt(int boundLo, int boundHi)
    {
        if (boundLo >= boundHi)
        {
            throw new ArgumentException("bound - origin is non positive");
        }
        else
        {
            return boundLo + NextInt(boundHi - boundLo);
        }
    }
}
