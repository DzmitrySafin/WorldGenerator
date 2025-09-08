using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.YClampedGradient
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class YClampedGradient : IDensityFunction
{
    [JsonProperty("from_y")]
    public int FromY { get; set; }

    [JsonProperty("to_y")]
    public int ToY { get; set; }

    [JsonProperty("from_value")]
    public double FromValue { get; set; }

    [JsonProperty("to_value")]
    public double ToValue { get; set; }

    public double Compute(IFunctionContext context)
    {
        return Mth.clampedMap(context.BlockY, FromY, ToY, FromValue, ToValue);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => Math.Max(FromValue, ToValue);

    public double MinValue => Math.Min(FromValue, ToValue);
}
