using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.ShiftedNoise
public class ShiftedNoise : IDensityFunction
{
    [JsonProperty("shift_x")]
    public required IDensityFunction ShiftX { get; set; }

    [JsonProperty("shift_y")]
    public required IDensityFunction ShiftY { get; set; }

    [JsonProperty("shift_z")]
    public required IDensityFunction ShiftZ { get; set; }

    [JsonProperty("xz_scale")]
    public double XZScale { get; set; }

    [JsonProperty("y_scale")]
    public double YScale { get; set; }

    [JsonProperty("noise"), JsonConverter(typeof(NoiseParametersConverter))]
    public required NoiseHolder NoiseHolder { get; set; }

    public double Compute(IFunctionContext context)
    {
        double d0 = context.BlockX * XZScale + ShiftX.Compute(context);
        double d1 = context.BlockY * YScale + ShiftY.Compute(context);
        double d2 = context.BlockZ * XZScale + ShiftZ.Compute(context);
        return NoiseHolder.GetValue(d0, d1, d2);
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(new ShiftedNoise
        {
            ShiftX = ShiftX.MapAll(densityVisitor),
            ShiftY = ShiftY.MapAll(densityVisitor),
            ShiftZ = ShiftZ.MapAll(densityVisitor),
            XZScale = XZScale,
            YScale = YScale,
            NoiseHolder = densityVisitor.VisitNoise(NoiseHolder)
        });
    }

    public double MaxValue => NoiseHolder.MaxValue;

    public double MinValue => -MaxValue;
}
