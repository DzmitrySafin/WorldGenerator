using Generator.Util;
using Generator.World.Level.Levelgen;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Synth;

//source: net.minecraft.world.level.levelgen.synth.PerlinNoise
public class PerlinNoise
{
    //private static int ROUND_OFF = 33554432;
    protected ImprovedNoise[] NoiseLevels { get; private set; }
    protected int FirstOctave { get; private set; }
    protected List<double> Amplitudes { get; private set; }
    private double lowestFreqValueFactor;
    private double lowestFreqInputFactor;
    public double MaxValue { get; private set; }

    [Obsolete]
    public static PerlinNoise CreateLegacyForBlendedNoise(IRandomSource randomSource, /*IntStream*/IList<int> p_230534_)
    {
        //return new PerlinNoise(randomSource, makeAmplitudes(new IntRBTreeSet(p_230534_.boxed().collect(ImmutableList.toImmutableList()))), false);
        return new PerlinNoise(randomSource, makeAmplitudes([.. p_230534_.ToImmutableList()]), false);
    }

    [Obsolete]
    public static PerlinNoise CreateLegacyForLegacyNetherBiome(IRandomSource randomSource, int p_230527_, List<double> p_230528_)
    {
        return new PerlinNoise(randomSource, KeyValuePair.Create(p_230527_, p_230528_), false);
    }

    public static PerlinNoise Create(IRandomSource randomSource, /*IntStream*/IList<int> p_230541_)
    {
        //return create(randomSource, p_230541_.boxed().collect(ImmutableList.toImmutableList()));
        return Create(randomSource, p_230541_.ToImmutableList());
    }

    public static PerlinNoise Create(IRandomSource randomSource, List<int> p_230531_)
    {
        return new PerlinNoise(randomSource, makeAmplitudes([.. p_230531_]), true);
    }

    public static PerlinNoise Create(IRandomSource randomSource, int p_230522_, double p_230523_, params double[] p_230524_)
    {
        List<double> doublearraylist = [.. p_230524_];
        doublearraylist.Insert(0, p_230523_);
        return new PerlinNoise(randomSource, KeyValuePair.Create(p_230522_, doublearraylist), true);
    }

    public static PerlinNoise Create(IRandomSource randomSource, int p_230537_, List<double> p_230538_)
    {
        return new PerlinNoise(randomSource, KeyValuePair.Create(p_230537_, p_230538_), true);
    }

    private static KeyValuePair<int, List<double>> makeAmplitudes(/*IntSortedSet*/SortedSet<int> p_75431_)
    {
        if (!p_75431_.Any())
        {
            throw new ArgumentException("Need some octaves!");
        }
        else
        {
            int i = -p_75431_.First();
            int j = p_75431_.Last();
            int k = i + j + 1;
            if (k < 1)
            {
                throw new ArgumentException("Total number of octaves needs to be >= 1");
            }
            else
            {
                List<double> doublelist = [.. new double[k]];
                foreach (int l in p_75431_)
                {
                    doublelist[l + i] = 1.0;
                }

                return KeyValuePair.Create(-i, doublelist);
            }
        }
    }

    protected PerlinNoise(IRandomSource randomSource, KeyValuePair<int, List<double>> p_230516_, bool p_230517_)
    {
        FirstOctave = p_230516_.Key;
        Amplitudes = p_230516_.Value;
        int i = Amplitudes.Count;
        int j = -FirstOctave;
        NoiseLevels = new ImprovedNoise[i];
        if (p_230517_)
        {
            IPositionalRandomFactory positionalrandomfactory = randomSource.ForkPositional();

            for (int k = 0; k < i; k++)
            {
                if (Amplitudes[k] != 0.0)
                {
                    int l = FirstOctave + k;
                    NoiseLevels[k] = new ImprovedNoise(positionalrandomfactory.FromHashOf("octave_" + l));
                }
            }
        }
        else
        {
            ImprovedNoise improvednoise = new ImprovedNoise(randomSource);
            if (j >= 0 && j < i)
            {
                double d0 = Amplitudes[j];
                if (d0 != 0.0)
                {
                    NoiseLevels[j] = improvednoise;
                }
            }

            for (int i1 = j - 1; i1 >= 0; i1--)
            {
                if (i1 < i)
                {
                    double d1 = Amplitudes[i1];
                    if (d1 != 0.0)
                    {
                        NoiseLevels[i1] = new ImprovedNoise(randomSource);
                    }
                    else
                    {
                        skipOctave(randomSource);
                    }
                }
                else
                {
                    skipOctave(randomSource);
                }
            }

            //if (Arrays.stream(this.noiseLevels).filter(Objects::nonNull).count() != this.Amplitudes.stream().filter(p_192897_->p_192897_ != 0.0).count())
            //{
            //    throw new IllegalStateException("Failed to create correct number of noise levels for given non-zero amplitudes");
            //}

            if (j < i - 1)
            {
                throw new ArgumentException("Positive octaves are temporarily disabled");
            }
        }

        lowestFreqInputFactor = Math.Pow(2.0, -j);
        lowestFreqValueFactor = Math.Pow(2.0, i - 1) / (Math.Pow(2.0, i) - 1.0);
        MaxValue = edgeValue(2.0);
    }

    private static void skipOctave(IRandomSource randomSource)
    {
        randomSource.ConsumeCount(262);
    }

    public double GetValue(double p_75409_, double p_75410_, double p_75411_)
    {
        return GetValue(p_75409_, p_75410_, p_75411_, 0.0, 0.0, false);
    }

    [Obsolete]
    public double GetValue(double p_75418_, double p_75419_, double p_75420_, double p_75421_, double p_75422_, bool p_75423_)
    {
        double d0 = 0.0;
        double d1 = lowestFreqInputFactor;
        double d2 = lowestFreqValueFactor;

        for (int i = 0; i < NoiseLevels.Length; i++)
        {
            ImprovedNoise improvednoise = NoiseLevels[i];
            if (improvednoise != null)
            {
                double d3 = improvednoise.Noise(
                    Wrap(p_75418_ * d1),
                    p_75423_ ? -improvednoise.yo : Wrap(p_75419_ * d1),
                    Wrap(p_75420_ * d1),
                    p_75421_ * d1,
                    p_75422_ * d1
                );
                d0 += Amplitudes[i] * d3 * d2;
            }

            d1 *= 2.0;
            d2 /= 2.0;
        }

        return d0;
    }

    public double MaxBrokenValue(double p_210644_)
    {
        return edgeValue(p_210644_ + 2.0);
    }

    private double edgeValue(double p_210650_)
    {
        double d0 = 0.0;
        double d1 = lowestFreqValueFactor;

        for (int i = 0; i < NoiseLevels.Length; i++)
        {
            ImprovedNoise improvednoise = NoiseLevels[i];
            if (improvednoise != null)
            {
                d0 += Amplitudes[i] * p_210650_ * d1;
            }

            d1 /= 2.0;
        }

        return d0;
    }

    public ImprovedNoise GetOctaveNoise(int p_75425_)
    {
        return NoiseLevels[NoiseLevels.Length - 1 - p_75425_];
    }

    public static double Wrap(double p_75407_)
    {
        return p_75407_ - Mth.lfloor(p_75407_ / 3.3554432E7 + 0.5) * 3.3554432E7;
    }
}
