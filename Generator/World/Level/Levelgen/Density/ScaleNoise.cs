using Generator.Json;
using Generator.World.Level.Levelgen.Synth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Noise
public class ScaleNoise : IDensityFunction
{
    [JsonProperty("noise"), JsonConverter(typeof(NoiseParametersConverter))]
    public required NoiseHolder NoiseHolder { get; set; }

    [JsonProperty("xz_scale")]
    public double XZScale { get; set; }

    [JsonProperty("y_scale")]
    public double YScale { get; set; }

    public double Compute(IFunctionContext context)
    {
        return NoiseHolder.GetValue(context.BlockX * XZScale, context.BlockY * YScale, context.BlockZ * XZScale);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new ScaleNoise
        {
            NoiseHolder = densityVisitor.VisitNoise(NoiseHolder),
            XZScale = XZScale,
            YScale = YScale
        });
    }

    public double MaxValue => NoiseHolder.MaxValue;

    public double MinValue => -MaxValue;
}
