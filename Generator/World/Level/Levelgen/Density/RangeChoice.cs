using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.RangeChoice
public class RangeChoice : IDensityFunction
{
    [JsonProperty("input")]
    public required IDensityFunction InputFunction { get; set; }

    [JsonProperty("min_inclusive")]
    public double MinInclusive { get; set; }

    [JsonProperty("max_exclusive")]
    public double MaxExclusive { get; set; }

    [JsonProperty("when_in_range")]
    public required IDensityFunction InRangeFunction { get; set; }

    [JsonProperty("when_out_of_range")]
    public required IDensityFunction OutOfRangeFunction { get; set; }

    public double Compute(IFunctionContext context)
    {
        double d0 = InputFunction.Compute(context);
        return d0 >= MinInclusive && d0 < MaxExclusive ? InRangeFunction.Compute(context) : OutOfRangeFunction.Compute(context);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputFunction.FillArray(array, contextProvider);

        for (int i = 0; i < array.Length; i++)
        {
            double d0 = array[i];
            if (d0 >= MinInclusive && d0 < MaxExclusive)
            {
                array[i] = InRangeFunction.Compute(contextProvider.ForIndex(i));
            }
            else
            {
                array[i] = OutOfRangeFunction.Compute(contextProvider.ForIndex(i));
            }
        }
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new RangeChoice
        {
            InputFunction = InputFunction.MapAll(densityVisitor),
            MinInclusive = MinInclusive,
            MaxExclusive = MaxExclusive,
            InRangeFunction = InRangeFunction.MapAll(densityVisitor),
            OutOfRangeFunction = OutOfRangeFunction.MapAll(densityVisitor)
        });
    }

    public double MaxValue => Math.Max(InRangeFunction.MaxValue, OutOfRangeFunction.MaxValue);

    public double MinValue => Math.Min(InRangeFunction.MinValue, OutOfRangeFunction.MinValue);
}
