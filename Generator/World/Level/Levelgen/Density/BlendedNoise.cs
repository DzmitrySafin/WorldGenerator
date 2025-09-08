using Generator.Util;
using Generator.World.Level.Levelgen.Synth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.synth.BlendedNoise
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class BlendedNoise : IDensityFunction
{
    [JsonProperty("xz_scale")]
    public double XZScale { get; set; }

    [JsonProperty("y_scale")]
    public double YScale { get; set; }

    [JsonProperty("xz_factor")]
    public double XZFactor { get; set; }

    [JsonProperty("y_factor")]
    public double YFactor { get; set; }

    [JsonProperty("smear_scale_multiplier")]
    public double SmearScaleMultiplier { get; set; }

    private readonly PerlinNoise minLimitNoise;
    private readonly PerlinNoise maxLimitNoise;
    private readonly PerlinNoise mainNoise;
    private readonly double xzMultiplier;
    private readonly double yMultiplier;
    private readonly double maxValue;

    public BlendedNoise()
    {
        // default constructor for JSON deserialization
    }

    private BlendedNoise(
        PerlinNoise minLimit,
        PerlinNoise maxLimit,
        PerlinNoise noise,
        double xzScale,
        double yScale,
        double xzFactor,
        double yFactor,
        double scaleMultiplier
    )
    {
        minLimitNoise = minLimit;
        maxLimitNoise = maxLimit;
        mainNoise = noise;
        XZScale = xzScale;
        YScale = yScale;
        XZFactor = xzFactor;
        YFactor = yFactor;
        SmearScaleMultiplier = scaleMultiplier;
        xzMultiplier = 684.412 * XZScale;
        yMultiplier = 684.412 * YScale;
        maxValue = minLimit.MaxBrokenValue(yMultiplier);
    }

    public BlendedNoise(IRandomSource randomSource, double xzScale, double yScale, double xzFactor, double yFactor, double scaleMultiplier)
        : this(
            PerlinNoise.CreateLegacyForBlendedNoise(randomSource, Enumerable.Range(-15, 16).ToList()),
            PerlinNoise.CreateLegacyForBlendedNoise(randomSource, Enumerable.Range(-15, 16).ToList()),
            PerlinNoise.CreateLegacyForBlendedNoise(randomSource, Enumerable.Range(-7, 8).ToList()),
            xzScale,
            yScale,
            xzFactor,
            yFactor,
            scaleMultiplier
        )
    {
    }

    public static BlendedNoise CreateUnseeded(double xzScale, double yScale, double xzFactor, double yFactor, double scaleMultiplier)
    {
        return new BlendedNoise(new XoroshiroRandomSource(0L), xzScale, yScale, xzFactor, yFactor, scaleMultiplier);
    }

    public BlendedNoise WithNewRandom(IRandomSource randomSource)
    {
        return new BlendedNoise(randomSource, XZScale, YScale, XZFactor, YFactor, SmearScaleMultiplier);
    }

    public double Compute(IFunctionContext context)
    {
        double d0 = context.BlockX * xzMultiplier;
        double d1 = context.BlockY * yMultiplier;
        double d2 = context.BlockZ * xzMultiplier;
        double d3 = d0 / XZFactor;
        double d4 = d1 / YFactor;
        double d5 = d2 / XZFactor;
        double d6 = yMultiplier * SmearScaleMultiplier;
        double d7 = d6 / YFactor;
        double d8 = 0.0;
        double d9 = 0.0;
        double d10 = 0.0;
        //bool flag = true;
        double d11 = 1.0;

        for (int i = 0; i < 8; i++)
        {
            ImprovedNoise improvednoise = mainNoise.GetOctaveNoise(i);
            if (improvednoise != null)
            {
                d10 += improvednoise.Noise(PerlinNoise.Wrap(d3 * d11), PerlinNoise.Wrap(d4 * d11), PerlinNoise.Wrap(d5 * d11), d7 * d11, d4 * d11) / d11;
            }

            d11 /= 2.0;
        }

        double d16 = (d10 / 10.0 + 1.0) / 2.0;
        bool flag1 = d16 >= 1.0;
        bool flag2 = d16 <= 0.0;
        d11 = 1.0;

        for (int j = 0; j < 16; j++)
        {
            double d12 = PerlinNoise.Wrap(d0 * d11);
            double d13 = PerlinNoise.Wrap(d1 * d11);
            double d14 = PerlinNoise.Wrap(d2 * d11);
            double d15 = d6 * d11;
            if (!flag1)
            {
                ImprovedNoise improvednoise1 = minLimitNoise.GetOctaveNoise(j);
                if (improvednoise1 != null)
                {
                    d8 += improvednoise1.Noise(d12, d13, d14, d15, d1 * d11) / d11;
                }
            }

            if (!flag2)
            {
                ImprovedNoise improvednoise2 = maxLimitNoise.GetOctaveNoise(j);
                if (improvednoise2 != null)
                {
                    d9 += improvednoise2.Noise(d12, d13, d14, d15, d1 * d11) / d11;
                }
            }

            d11 /= 2.0;
        }

        return Mth.clampedLerp(d8 / 512.0, d9 / 512.0, d16) / 128.0;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => maxValue;

    public double MinValue => -MaxValue;
}
