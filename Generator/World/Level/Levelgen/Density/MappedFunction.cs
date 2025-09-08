using Generator.Enums;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Mapped
//source: net.minecraft.world.level.levelgen.DensityFunctions.PureTransformer
public class MappedFunction : IDensityFunction
{
    [JsonProperty("type")]
    public DensityMappedType MappedType { get; set; }

    [JsonProperty("argument")]
    public required IDensityFunction InputFunction { get; set; }

    private double minValue;
    private double maxValue;

    public static MappedFunction Create(DensityMappedType mappedType, IDensityFunction inputDensity)
    {
        double d0 = inputDensity.MinValue;
        double d1 = transform(mappedType, d0);
        double d2 = transform(mappedType, inputDensity.MaxValue);
        return new MappedFunction
        {
            MappedType = mappedType,
            InputFunction = inputDensity,
            minValue = mappedType != DensityMappedType.ABS && mappedType != DensityMappedType.SQUARE ? d1 : Math.Max(0.0, d0),
            maxValue = mappedType != DensityMappedType.ABS && mappedType != DensityMappedType.SQUARE ? d2 : Math.Max(d1, d2)
        };
    }

    public double Compute(IFunctionContext context)
    {
        return transform(InputFunction.Compute(context));
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputFunction.FillArray(array, contextProvider);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = transform(array[i]);
        }
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return Create(MappedType, InputFunction.MapAll(densityVisitor));
    }

    public double MaxValue => maxValue;

    public double MinValue => minValue;

    private static double transform(DensityMappedType mappedType, double p_208670_)
    {
        return mappedType switch
        {
            DensityMappedType.ABS => Math.Abs(p_208670_),
            DensityMappedType.SQUARE => p_208670_ * p_208670_,
            DensityMappedType.CUBE => p_208670_ * p_208670_ * p_208670_,
            DensityMappedType.HALF_NEGATIVE => p_208670_ > 0.0 ? p_208670_ : p_208670_ * 0.5,
            DensityMappedType.QUARTER_NEGATIVE => p_208670_ > 0.0 ? p_208670_ : p_208670_ * 0.25,
            DensityMappedType.SQUEEZE => ((Func<double>)(() => {
                double d0 = Mth.clamp(p_208670_, -1.0, 1.0);
                return d0 / 2.0 - d0 * d0 * d0 / 24.0;
            }))(),
            _ => throw new NotImplementedException()
        };
    }

    public double transform(double p_208665_)
    {
        return transform(MappedType, p_208665_);
    }
}
