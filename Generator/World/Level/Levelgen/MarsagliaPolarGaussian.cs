using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.MarsagliaPolarGaussian
public class MarsagliaPolarGaussian
{
    public IRandomSource RandomSource { get; private set; }
    private double nextNextGaussian;
    private bool haveNextNextGaussian;

    public MarsagliaPolarGaussian(IRandomSource randomSource)
    {
        RandomSource = randomSource;
    }

    public void Reset()
    {
        haveNextNextGaussian = false;
    }

    public double NextGaussian()
    {
        if (haveNextNextGaussian)
        {
            haveNextNextGaussian = false;
            return nextNextGaussian;
        }
        else
        {
            double d0;
            double d1;
            double d2;
            do
            {
                d0 = 2.0 * RandomSource.NextDouble() - 1.0;
                d1 = 2.0 * RandomSource.NextDouble() - 1.0;
                d2 = Mth.square(d0) + Mth.square(d1);
            } while (d2 >= 1.0 || d2 == 0.0);

            double d3 = Math.Sqrt(-2.0 * Math.Log(d2) / d2);
            nextNextGaussian = d1 * d3;
            haveNextNextGaussian = true;
            return d0 * d3;
        }
    }
}
