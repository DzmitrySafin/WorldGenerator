using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Clamp
//source: net.minecraft.world.level.levelgen.DensityFunctions.PureTransformer
public class ClampFunction : IDensityFunction
{
    [JsonProperty("input")]
    public required IDensityFunction InputFunction { get; set; }

    [JsonProperty("min")]
    public double Min { get; set; }

    [JsonProperty("max")]
    public double Max { get; set; }

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
        return densityVisitor.Apply(new ClampFunction
        {
            InputFunction = InputFunction.MapAll(densityVisitor),
            Min = Min,
            Max = Max
        });
    }

    public double MaxValue => Max;

    public double MinValue => Min;

    public double transform(double p_208595_)
    {
        return Mth.clamp(p_208595_, Min, Max);
    }
}
