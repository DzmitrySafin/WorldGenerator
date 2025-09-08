using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.BitRandomSource
public interface IBitRandomSource : IRandomSource
{
    //float FLOAT_MULTIPLIER = 5.9604645E-8F;
    //double DOUBLE_MULTIPLIER = 1.110223E-16F;

    int Next(int bound);

    int IRandomSource.NextInt()
    {
        return Next(32);
    }

    int IRandomSource.NextInt(int bound)
    {
        if (bound <= 0)
        {
            throw new ArgumentException("Bound must be positive");
        }
        else if ((bound & bound - 1) == 0)
        {
            return (int)((long)bound * Next(31) >> 31);
        }
        else
        {
            int i;
            int j;
            do
            {
                i = Next(31);
                j = i % bound;
            } while (i - j + (bound - 1) < 0);

            return j;
        }
    }

    long IRandomSource.NextLong()
    {
        int i = Next(32);
        int j = Next(32);
        long k = (long)i << 32;
        return k + j;
    }

    bool IRandomSource.NextBoolean()
    {
        return Next(1) != 0;
    }

    float IRandomSource.NextFloat()
    {
        return Next(24) * 5.9604645E-8F;
    }

    double IRandomSource.NextDouble()
    {
        int i = Next(26);
        int j = Next(27);
        long k = ((long)i << 27) + j;
        return k * 1.110223E-16F;
    }
}
