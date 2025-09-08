using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Synth;

//source: net.minecraft.world.level.levelgen.synth.NormalNoise
public class NormalNoise
{
    private static double INPUT_FACTOR = 1.0181268882175227;
    //private static double TARGET_DEVIATION = 0.3333333333333333;
    public double ValueFactor { get; private set; }
    private PerlinNoise first;
    private PerlinNoise second;
    public double MaxValue { get; private set; }
    public NoiseParameters Parameters { get; private set; }

    [Obsolete]
    public static NormalNoise createLegacyNetherBiome(IRandomSource randomSource, NoiseParameters noiseParameters)
    {
        return new NormalNoise(randomSource, noiseParameters, false);
    }

    public static NormalNoise Create(IRandomSource randomSource, int octave, params double[] noiseParameters)
    {
        return Create(randomSource, new NoiseParameters(octave, noiseParameters));
    }

    public static NormalNoise Create(IRandomSource randomSource, NoiseParameters noiseParameters)
    {
        return new NormalNoise(randomSource, noiseParameters, true);
    }

    private NormalNoise(IRandomSource randomSource, NoiseParameters noiseParameters, bool p_230503_)
    {
        int i = noiseParameters.FirstOctave;
        List<double> doublelist = noiseParameters.Amplitudes;
        Parameters = noiseParameters;
        if (p_230503_)
        {
            first = PerlinNoise.Create(randomSource, i, doublelist);
            second = PerlinNoise.Create(randomSource, i, doublelist);
        }
        else
        {
            first = PerlinNoise.CreateLegacyForLegacyNetherBiome(randomSource, i, doublelist);
            second = PerlinNoise.CreateLegacyForLegacyNetherBiome(randomSource, i, doublelist);
        }

        int j = int.MaxValue;
        int k = int.MinValue;
        for (int l = 0; l < doublelist.Count; l++)
        {
            double d0 = doublelist[l];
            if (d0 != 0.0)
            {
                j = Math.Min(j, l);
                k = Math.Max(k, l);
            }
        }

        ValueFactor = 0.16666666666666666 / expectedDeviation(k - j);
        MaxValue = (first.MaxValue + second.MaxValue) * ValueFactor;
    }

    private static double expectedDeviation(int p_75385_)
    {
        return 0.1 * (1.0 + 1.0 / (p_75385_ + 1));
    }

    public double GetValue(double p_75381_, double p_75382_, double p_75383_)
    {
        double d0 = p_75381_ * INPUT_FACTOR;
        double d1 = p_75382_ * INPUT_FACTOR;
        double d2 = p_75383_ * INPUT_FACTOR;
        return (first.GetValue(p_75381_, p_75382_, p_75383_) + second.GetValue(d0, d1, d2)) * ValueFactor;
    }
}
