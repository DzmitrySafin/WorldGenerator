using Generator.Json;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Spline
public class SplineFunction : IDensityFunction
{
    [JsonProperty("coordinate"), JsonConverter(typeof(CoordinateFunctionConverter))]
    public required CoordinateFunction Coordinate { get; set; }

    [JsonProperty("points"), JsonConverter(typeof(SplinePointsConverter))]
    public required List<ISplinePoint> Points { get; set; }

    private float[] locations;
    private float[] derivatives;
    private float minValue;
    private float maxValue;

    public static SplineFunction Create(CoordinateFunction coordinate, float[] locations, List<ISplinePoint> values, float[] derivatives)
    {
        validateSizes(locations, values, derivatives);
        int i = locations.Length - 1;
        float f = float.PositiveInfinity;
        float f1 = float.NegativeInfinity;
        float f2 = coordinate.MinValue();
        float f3 = coordinate.MaxValue();
        if (f2 < locations[0])
        {
            float f4 = linearExtend(f2, locations, values[0].MinValue, derivatives, 0);
            float f5 = linearExtend(f2, locations, values[0].MaxValue, derivatives, 0);
            f = Math.Min(f, Math.Min(f4, f5));
            f1 = Math.Max(f1, Math.Max(f4, f5));
        }

        if (f3 > locations[i])
        {
            float f24 = linearExtend(f3, locations, values[i].MinValue, derivatives, i);
            float f25 = linearExtend(f3, locations, values[i].MaxValue, derivatives, i);
            f = Math.Min(f, Math.Min(f24, f25));
            f1 = Math.Max(f1, Math.Max(f24, f25));
        }

        foreach (ISplinePoint cubicspline2 in values)
        {
            f = Math.Min(f, cubicspline2.MinValue);
            f1 = Math.Max(f1, cubicspline2.MaxValue);
        }

        for (int j = 0; j < i; j++)
        {
            float f26 = locations[j];
            float f6 = locations[j + 1];
            float f7 = f6 - f26;
            ISplinePoint cubicspline = values[j];
            ISplinePoint cubicspline1 = values[j + 1];
            float f8 = cubicspline.MinValue;
            float f9 = cubicspline.MaxValue;
            float f10 = cubicspline1.MinValue;
            float f11 = cubicspline1.MaxValue;
            float f12 = derivatives[j];
            float f13 = derivatives[j + 1];
            if (f12 != 0.0F || f13 != 0.0F)
            {
                float f14 = f12 * f7;
                float f15 = f13 * f7;
                float f16 = Math.Min(f8, f10);
                float f17 = Math.Max(f9, f11);
                float f18 = f14 - f11 + f8;
                float f19 = f14 - f10 + f9;
                float f20 = -f15 + f10 - f9;
                float f21 = -f15 + f11 - f8;
                float f22 = Math.Min(f18, f20);
                float f23 = Math.Max(f19, f21);
                f = Math.Min(f, f16 + 0.25F * f22);
                f1 = Math.Max(f1, f17 + 0.25F * f23);
            }
        }

        return new SplineFunction
        {
            Coordinate = coordinate,
            locations = locations,
            Points = values,
            derivatives = derivatives,
            minValue = f,
            maxValue = f1
        };
    }

    public float Apply(IFunctionContext context)
    {
        float f = Coordinate.Apply(context);
        int i = findIntervalStart(locations, f);
        int j = locations.Length - 1;
        if (i < 0)
        {
            return linearExtend(f, locations, Points[0].Apply(context), derivatives, 0);
        }
        else if (i == j)
        {
            return linearExtend(f, locations, Points[j].Apply(context), derivatives, j);
        }
        else
        {
            float f1 = locations[i];
            float f2 = locations[i + 1];
            float f3 = (f - f1) / (f2 - f1);
            float f4 = derivatives[i];
            float f5 = derivatives[i + 1];
            float f6 = Points[i].Apply(context);
            float f7 = Points[i + 1].Apply(context);
            float f8 = f4 * (f2 - f1) - (f7 - f6);
            float f9 = -f5 * (f2 - f1) + (f7 - f6);
            return Mth.lerp(f3, f6, f7) + f3 * (1.0F - f3) * Mth.lerp(f3, f8, f9);
        }
    }

    public double Compute(IFunctionContext context)
    {
        return Apply(context);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return Create(
                Coordinate.MapAll(densityVisitor),
                Points.Select(p => p.Location).ToArray(),
                Points.Select(p => p.MapAll(densityVisitor)).ToList(),
                Points.Select(p => p.Derivative).ToArray()
            );
    }

    public double MaxValue => maxValue;

    public double MinValue => minValue;

    private static float linearExtend(float p_216134_, float[] p_216135_, float p_216136_, float[] p_216137_, int p_216138_)
    {
        float f = p_216137_[p_216138_];
        return f == 0.0F ? p_216136_ : p_216136_ + f * (p_216134_ - p_216135_[p_216138_]);
    }

    private static void validateSizes(float[] p_216152_, List<ISplinePoint> p_216153_, float[] p_216154_)
    {
        if (p_216152_.Length != p_216153_.Count() || p_216152_.Length != p_216154_.Length)
        {
            throw new ArgumentException("All lengths must be equal, got: " + p_216152_.Length + " " + p_216153_.Count() + " " + p_216154_.Length);
        }
        else if (p_216152_.Length == 0)
        {
            throw new ArgumentException("Cannot create a multipoint spline with no points");
        }
    }

    private static int findIntervalStart(float[] p_216149_, float p_216150_)
    {
        return Mth.binarySearch(0, p_216149_.Length, p_216142_ => p_216150_ < p_216149_[p_216142_]) - 1;
    }
}
