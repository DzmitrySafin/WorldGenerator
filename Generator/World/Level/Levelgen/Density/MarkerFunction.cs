using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Marker
//source: net.minecraft.world.level.levelgen.DensityFunctions.MarkerOrMarked
public class MarkerFunction : IDensityFunction
{
    [JsonProperty("argument")]
    public required IDensityFunction InputFunction { get; set; }

    [JsonProperty("type")]
    public DensityMarkerType MarkerType { get; set; }

    public double Compute(IFunctionContext context)
    {
        return InputFunction.Compute(context);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputFunction.FillArray(array, contextProvider);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new MarkerFunction
        {
            MarkerType = MarkerType,
            InputFunction = InputFunction.MapAll(densityVisitor)
        });
    }

    public double MaxValue => InputFunction.MaxValue;

    public double MinValue => InputFunction.MinValue;
}
