using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Synth;

//source: net.minecraft.world.level.levelgen.synth.ImprovedNoise
public class ImprovedNoise
{
    private static float SHIFT_UP_EPSILON = 1.0E-7F;
    private byte[] p;
    public double xo;
    public double yo;
    public double zo;

    public ImprovedNoise(IRandomSource randomSource)
    {
        xo = randomSource.NextDouble() * 256.0;
        yo = randomSource.NextDouble() * 256.0;
        zo = randomSource.NextDouble() * 256.0;
        p = new byte[256];

        for (int i = 0; i < 256; i++)
        {
            p[i] = (byte)i;
        }

        for (int k = 0; k < 256; k++)
        {
            int j = randomSource.NextInt(256 - k);
            byte b0 = p[k];
            p[k] = p[k + j];
            p[k + j] = b0;
        }
    }

    public double Noise(double p_164309_, double p_164310_, double p_164311_)
    {
        return Noise(p_164309_, p_164310_, p_164311_, 0.0, 0.0);
    }

    [Obsolete]
    public double Noise(double p_75328_, double p_75329_, double p_75330_, double p_75331_, double p_75332_)
    {
        double d0 = p_75328_ + xo;
        double d1 = p_75329_ + yo;
        double d2 = p_75330_ + zo;
        int i = Mth.floor(d0);
        int j = Mth.floor(d1);
        int k = Mth.floor(d2);
        double d3 = d0 - i;
        double d4 = d1 - j;
        double d5 = d2 - k;
        double d6;
        if (p_75331_ != 0.0)
        {
            double d7;
            if (p_75332_ >= 0.0 && p_75332_ < d4)
            {
                d7 = p_75332_;
            }
            else
            {
                d7 = d4;
            }

            d6 = Mth.floor(d7 / p_75331_ + SHIFT_UP_EPSILON) * p_75331_;
        }
        else
        {
            d6 = 0.0;
        }

        return sampleAndLerp(i, j, k, d3, d4 - d6, d5, d4);
    }

    public double NoiseWithDerivative(double p_164313_, double p_164314_, double p_164315_, double[] p_164316_)
    {
        double d0 = p_164313_ + xo;
        double d1 = p_164314_ + yo;
        double d2 = p_164315_ + zo;
        int i = Mth.floor(d0);
        int j = Mth.floor(d1);
        int k = Mth.floor(d2);
        double d3 = d0 - i;
        double d4 = d1 - j;
        double d5 = d2 - k;
        return sampleWithDerivative(i, j, k, d3, d4, d5, p_164316_);
    }

    private static double gradDot(int p_75336_, double p_75337_, double p_75338_, double p_75339_)
    {
        return SimplexNoise.Dot(SimplexNoise.GRADIENT[p_75336_ & 15], p_75337_, p_75338_, p_75339_);
    }

    private int pb(int p_75334_)
    {
        return p[p_75334_ & 0xFF] & 0xFF;
    }

    private double sampleAndLerp(int p_164318_, int p_164319_, int p_164320_, double p_164321_, double p_164322_, double p_164323_, double p_164324_)
    {
        int i = pb(p_164318_);
        int j = pb(p_164318_ + 1);
        int k = pb(i + p_164319_);
        int l = pb(i + p_164319_ + 1);
        int i1 = pb(j + p_164319_);
        int j1 = pb(j + p_164319_ + 1);
        double d0 = gradDot(pb(k + p_164320_), p_164321_, p_164322_, p_164323_);
        double d1 = gradDot(pb(i1 + p_164320_), p_164321_ - 1.0, p_164322_, p_164323_);
        double d2 = gradDot(pb(l + p_164320_), p_164321_, p_164322_ - 1.0, p_164323_);
        double d3 = gradDot(pb(j1 + p_164320_), p_164321_ - 1.0, p_164322_ - 1.0, p_164323_);
        double d4 = gradDot(pb(k + p_164320_ + 1), p_164321_, p_164322_, p_164323_ - 1.0);
        double d5 = gradDot(pb(i1 + p_164320_ + 1), p_164321_ - 1.0, p_164322_, p_164323_ - 1.0);
        double d6 = gradDot(pb(l + p_164320_ + 1), p_164321_, p_164322_ - 1.0, p_164323_ - 1.0);
        double d7 = gradDot(pb(j1 + p_164320_ + 1), p_164321_ - 1.0, p_164322_ - 1.0, p_164323_ - 1.0);
        double d8 = Mth.smoothstep(p_164321_);
        double d9 = Mth.smoothstep(p_164324_);
        double d10 = Mth.smoothstep(p_164323_);
        return Mth.lerp3(d8, d9, d10, d0, d1, d2, d3, d4, d5, d6, d7);
    }

    private double sampleWithDerivative(int p_164326_, int p_164327_, int p_164328_, double p_164329_, double p_164330_, double p_164331_, double[] p_164332_)
    {
        int i = pb(p_164326_);
        int j = pb(p_164326_ + 1);
        int k = pb(i + p_164327_);
        int l = pb(i + p_164327_ + 1);
        int i1 = pb(j + p_164327_);
        int j1 = pb(j + p_164327_ + 1);
        int k1 = pb(k + p_164328_);
        int l1 = pb(i1 + p_164328_);
        int i2 = pb(l + p_164328_);
        int j2 = pb(j1 + p_164328_);
        int k2 = pb(k + p_164328_ + 1);
        int l2 = pb(i1 + p_164328_ + 1);
        int i3 = pb(l + p_164328_ + 1);
        int j3 = pb(j1 + p_164328_ + 1);
        int[] aint = SimplexNoise.GRADIENT[k1 & 15];
        int[] aint1 = SimplexNoise.GRADIENT[l1 & 15];
        int[] aint2 = SimplexNoise.GRADIENT[i2 & 15];
        int[] aint3 = SimplexNoise.GRADIENT[j2 & 15];
        int[] aint4 = SimplexNoise.GRADIENT[k2 & 15];
        int[] aint5 = SimplexNoise.GRADIENT[l2 & 15];
        int[] aint6 = SimplexNoise.GRADIENT[i3 & 15];
        int[] aint7 = SimplexNoise.GRADIENT[j3 & 15];
        double d0 = SimplexNoise.Dot(aint, p_164329_, p_164330_, p_164331_);
        double d1 = SimplexNoise.Dot(aint1, p_164329_ - 1.0, p_164330_, p_164331_);
        double d2 = SimplexNoise.Dot(aint2, p_164329_, p_164330_ - 1.0, p_164331_);
        double d3 = SimplexNoise.Dot(aint3, p_164329_ - 1.0, p_164330_ - 1.0, p_164331_);
        double d4 = SimplexNoise.Dot(aint4, p_164329_, p_164330_, p_164331_ - 1.0);
        double d5 = SimplexNoise.Dot(aint5, p_164329_ - 1.0, p_164330_, p_164331_ - 1.0);
        double d6 = SimplexNoise.Dot(aint6, p_164329_, p_164330_ - 1.0, p_164331_ - 1.0);
        double d7 = SimplexNoise.Dot(aint7, p_164329_ - 1.0, p_164330_ - 1.0, p_164331_ - 1.0);
        double d8 = Mth.smoothstep(p_164329_);
        double d9 = Mth.smoothstep(p_164330_);
        double d10 = Mth.smoothstep(p_164331_);
        double d11 = Mth.lerp3(d8, d9, d10, aint[0], aint1[0], aint2[0], aint3[0], aint4[0], aint5[0], aint6[0], aint7[0]);
        double d12 = Mth.lerp3(d8, d9, d10, aint[1], aint1[1], aint2[1], aint3[1], aint4[1], aint5[1], aint6[1], aint7[1]);
        double d13 = Mth.lerp3(d8, d9, d10, aint[2], aint1[2], aint2[2], aint3[2], aint4[2], aint5[2], aint6[2], aint7[2]);
        double d14 = Mth.lerp2(d9, d10, d1 - d0, d3 - d2, d5 - d4, d7 - d6);
        double d15 = Mth.lerp2(d10, d8, d2 - d0, d6 - d4, d3 - d1, d7 - d5);
        double d16 = Mth.lerp2(d8, d9, d4 - d0, d5 - d1, d6 - d2, d7 - d3);
        double d17 = Mth.smoothstepDerivative(p_164329_);
        double d18 = Mth.smoothstepDerivative(p_164330_);
        double d19 = Mth.smoothstepDerivative(p_164331_);
        double d20 = d11 + d17 * d14;
        double d21 = d12 + d18 * d15;
        double d22 = d13 + d19 * d16;
        p_164332_[0] += d20;
        p_164332_[1] += d21;
        p_164332_[2] += d22;
        return Mth.lerp3(d8, d9, d10, d0, d1, d2, d3, d4, d5, d6, d7);
    }
}
