using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.BlendDensity
//source: net.minecraft.world.level.levelgen.DensityFunctions.TransformerWithContext
public class BlendDensity : IDensityFunction
{
    [JsonProperty("argument")]
    public required IDensityFunction InputFunction { get; set; }

    public double Compute(IFunctionContext context)
    {
        return transform(context, InputFunction.Compute(context));
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputFunction.FillArray(array, contextProvider);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = transform(contextProvider.ForIndex(i), array[i]);
        }
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new BlendDensity
        {
            InputFunction = InputFunction.MapAll(densityVisitor)
        });
    }

    public double MaxValue => double.PositiveInfinity;

    public double MinValue => double.NegativeInfinity;

    private double transform(IFunctionContext context, double p_208554_)
    {
        return context.GetBlender().BlendDensity(context, p_208554_);
    }
}
