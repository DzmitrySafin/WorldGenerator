using Generator.Core;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Blending;

//source: net.minecraft.world.level.levelgen.blending.Blender
public class Blender
{
    //private static readonly NormalNoise SHIFT_NOISE = NormalNoise.Create(new XoroshiroRandomSource(42L), NoiseData.DEFAULT_SHIFT);
    private static readonly int HEIGHT_BLENDING_RANGE_CELLS = QuartPosition.FromSection(7) - 1;
    private static readonly int HEIGHT_BLENDING_RANGE_CHUNKS = QuartPosition.ToSection(HEIGHT_BLENDING_RANGE_CELLS + 3);
    //private static readonly int DENSITY_BLENDING_RANGE_CELLS = 2;
    private static readonly int DENSITY_BLENDING_RANGE_CHUNKS = QuartPosition.ToSection(5);
    //private static readonly double OLD_CHUNK_XZ_RADIUS = 8.0;
    private readonly Dictionary<long, BlendingData> heightAndBiomeBlendingData;
    private readonly Dictionary<long, BlendingData> densityBlendingData;

    Blender(Dictionary<long, BlendingData> heightsData, Dictionary<long, BlendingData> densityData)
    {
        heightAndBiomeBlendingData = heightsData;
        densityBlendingData = densityData;
    }

    public static readonly Blender EMPTY = new Blender(new Dictionary<long, BlendingData>(), new Dictionary<long, BlendingData>());

    //public static Blender Of([AllowNull] WorldGenRegion p_190203_)
    //{
    //}

    public BlendingOutput BlendOffsetAndFactor(int p_209719_, int p_209720_)
    {
        int i = QuartPosition.FromBlock(p_209719_);
        int j = QuartPosition.FromBlock(p_209720_);
        double d0 = getBlendingDataValue(i, 0, j, nameof(BlendingData.GetHeight));
        if (d0 != double.MaxValue)
        {
            return new BlendingOutput(0.0, heightToOffset(d0));
        }
        else
        {
            double mutabledouble = 0.0;
            double mutabledouble1 = 0.0;
            double mutabledouble2 = double.PositiveInfinity;
            foreach (KeyValuePair<long, BlendingData> heightsDataPair in heightAndBiomeBlendingData)
            {
                long p_202249_ = heightsDataPair.Key;
                BlendingData p_202250_ = heightsDataPair.Value;
                p_202250_.IterateHeights(QuartPosition.FromSection(ChunkPosition.GetX(p_202249_)), QuartPosition.FromSection(ChunkPosition.GetZ(p_202249_)), (p_190199_, p_190200_, p_190201_) =>
                {
                    double d3 = Mth.length(i - p_190199_, j - p_190200_);
                    if (!(d3 > HEIGHT_BLENDING_RANGE_CELLS))
                    {
                        if (d3 < mutabledouble2)
                        {
                            mutabledouble2 = d3;
                        }

                        double d4 = 1.0 / (d3 * d3 * d3 * d3);
                        mutabledouble1 += p_190201_ * d4;
                        mutabledouble += d4;
                    }
                });
            }
            if (mutabledouble2 == double.PositiveInfinity)
            {
                return new BlendingOutput(1.0, 0.0);
            }
            else
            {
                double d1 = mutabledouble1 / mutabledouble;
                double d2 = Mth.clamp(mutabledouble2 / (HEIGHT_BLENDING_RANGE_CELLS + 1), 0.0, 1.0);
                d2 = 3.0 * d2 * d2 - 2.0 * d2 * d2 * d2;
                return new BlendingOutput(d2, heightToOffset(d1));
            }
        }
    }

    private static double heightToOffset(double height)
    {
        double d1 = height + 0.5;
        double d2 = Mth.positiveModulo(d1, 8.0);
        return 1.0 * (32.0 * (d1 - 128.0) - 3.0 * (d1 - 120.0) * d2 + 3.0 * d2 * d2) / (128.0 * (32.0 - 3.0 * d2));
    }

    public double BlendDensity(IFunctionContext context, double p_209722_)
    {
        int i = QuartPosition.FromBlock(context.BlockX);
        int j = context.BlockY / 8;
        int k = QuartPosition.FromBlock(context.BlockZ);
        double d0 = getBlendingDataValue(i, j, k, nameof(BlendingData.GetDensity));
        if (d0 != double.MaxValue)
        {
            return d0;
        }
        else
        {
            double mutabledouble = 0.0;
            double mutabledouble1 = 0.0;
            double mutabledouble2 = double.PositiveInfinity;
            foreach (KeyValuePair<long, BlendingData> densityDataPair in densityBlendingData)
            {
                long p_202241_ = densityDataPair.Key;
                BlendingData p_202242_ = densityDataPair.Value;
                p_202242_.IterateDensities(QuartPosition.FromSection(ChunkPosition.GetX(p_202241_)), QuartPosition.FromSection(ChunkPosition.GetZ(p_202241_)), j - 1, j + 1, (p_202230_, p_202231_, p_202232_, p_202233_) =>
                {
                    double d3 = Mth.length(i - p_202230_, (j - p_202231_) * 2, k - p_202232_);
                    if (!(d3 > 2.0))
                    {
                        if (d3 < mutabledouble2)
                        {
                            mutabledouble2 = d3;
                        }

                        double d4 = 1.0 / (d3 * d3 * d3 * d3);
                        mutabledouble1 += p_202233_ * d4;
                        mutabledouble += d4;
                    }
                });
            }
            if (mutabledouble2 == double.PositiveInfinity)
            {
                return p_209722_;
            }
            else
            {
                double d1 = mutabledouble1 / mutabledouble;
                double d2 = Mth.clamp(mutabledouble2 / 3.0, 0.0, 1.0);
                return Mth.lerp(d2, d1, p_209722_);
            }
        }
    }

    private double getBlendingDataValue(int p_190175_, int p_190176_, int p_190177_, string cellValueGetter)
    {
        int i = QuartPosition.ToSection(p_190175_);
        int j = QuartPosition.ToSection(p_190177_);
        bool flag = (p_190175_ & 3) == 0;
        bool flag1 = (p_190177_ & 3) == 0;
        double d0 = getBlendingDataValue(cellValueGetter, i, j, p_190175_, p_190176_, p_190177_);
        if (d0 == double.MaxValue)
        {
            if (flag && flag1)
            {
                d0 = getBlendingDataValue(cellValueGetter, i - 1, j - 1, p_190175_, p_190176_, p_190177_);
            }

            if (d0 == double.MaxValue)
            {
                if (flag)
                {
                    d0 = getBlendingDataValue(cellValueGetter, i - 1, j, p_190175_, p_190176_, p_190177_);
                }

                if (d0 == double.MaxValue && flag1)
                {
                    d0 = getBlendingDataValue(cellValueGetter, i, j - 1, p_190175_, p_190176_, p_190177_);
                }
            }
        }

        return d0;
    }

    private double getBlendingDataValue(string cellValueGetter, int p_190213_, int p_190214_, int p_190215_, int p_190216_, int p_190217_)
    {
        long height = ChunkPosition.AsLong(p_190213_, p_190214_);
        if (heightAndBiomeBlendingData.ContainsKey(height))
        {
            BlendingData blendingdata = heightAndBiomeBlendingData[height];
            MethodInfo methodInfo = blendingdata.GetType().GetMethod(cellValueGetter)!;
            return (double) methodInfo.Invoke(blendingdata, [p_190215_ - QuartPosition.FromSection(p_190213_), p_190216_, p_190217_ - QuartPosition.FromSection(p_190214_)])!;
        }
        else
        {
            return double.MaxValue;
        }
    }

    //TODO: to be continued...
}
