using Generator.Util;
using Generator.World.Level.Levelgen.Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.EndIslandDensityFunction
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class EndIslandDensityFunction : IDensityFunction
{
    private static readonly float ISLAND_THRESHOLD = -0.9F;
    private readonly SimplexNoise islandNoise;

    public EndIslandDensityFunction(long seed)
    {
        IRandomSource randomsource = new LegacyRandomSource(seed);
        randomsource.ConsumeCount(17292);
        islandNoise = new SimplexNoise(randomsource);
    }

    public double Compute(IFunctionContext context)
    {
        return (getHeightValue(islandNoise, context.BlockX / 8, context.BlockZ / 8) - 8.0) / 128.0;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => 0.5625;

    public double MinValue => -0.84375;

    private static float getHeightValue(SimplexNoise p_224063_, int p_224064_, int p_224065_)
    {
        int i = p_224064_ / 2;
        int j = p_224065_ / 2;
        int k = p_224064_ % 2;
        int l = p_224065_ % 2;
        float f = 100.0F - Mth.sqrt(p_224064_ * p_224064_ + p_224065_ * p_224065_) * 8.0F;
        f = Mth.clamp(f, -100.0F, 80.0F);

        for (int i1 = -12; i1 <= 12; i1++)
        {
            for (int j1 = -12; j1 <= 12; j1++)
            {
                long k1 = i + i1;
                long l1 = j + j1;
                if (k1 * k1 + l1 * l1 > 4096L && p_224063_.GetValue(k1, l1) < ISLAND_THRESHOLD)
                {
                    float f1 = (Mth.abs((float)k1) * 3439.0F + Mth.abs((float)l1) * 147.0F) % 13.0F + 9.0F;
                    float f2 = k - i1 * 2;
                    float f3 = l - j1 * 2;
                    float f4 = 100.0F - Mth.sqrt(f2 * f2 + f3 * f3) * f1;
                    f4 = Mth.clamp(f4, -100.0F, 80.0F);
                    f = Math.Max(f, f4);
                }
            }
        }

        return f;
    }
}
