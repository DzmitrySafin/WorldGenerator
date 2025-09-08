using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.ShiftB
//source: net.minecraft.world.level.levelgen.DensityFunctions.ShiftNoise
public class ShiftB : IDensityFunction
{
    [JsonProperty("argument"), JsonConverter(typeof(NoiseParametersConverter))]
    public required NoiseHolder NoiseHolder { get; set; }

    public double Compute(IFunctionContext context)
    {
        return compute(context.BlockZ, context.BlockX, 0.0);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new ShiftB
        {
            NoiseHolder = densityVisitor.VisitNoise(NoiseHolder)
        });
    }

    public double MaxValue => NoiseHolder.MaxValue * 4.0;

    public double MinValue => -MaxValue;

    private double compute(double x, double y, double z)
    {
        return NoiseHolder.GetValue(x * 0.25, y * 0.25, z * 0.25) * 4.0;
    }
}
