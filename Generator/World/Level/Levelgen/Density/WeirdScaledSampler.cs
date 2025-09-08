using Generator.Json;
using Generator.World.Level.Levelgen.Synth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.WeirdScaledSampler
public class WeirdScaledSampler : IDensityFunction
{
    [JsonProperty("input")]
    public required IDensityFunction InputFunction { get; set; }

    [JsonProperty("noise"), JsonConverter(typeof(NoiseParametersConverter))]
    public required NoiseHolder NoiseHolder { get; set; }

    [JsonProperty("rarity_value_mapper")]
    public required QuantizedSpaghettiRarity RarityValueMapper { get; set; }

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
        return densityVisitor.Apply(new WeirdScaledSampler
        {
            InputFunction = InputFunction.MapAll(densityVisitor),
            NoiseHolder = densityVisitor.VisitNoise(NoiseHolder),
            RarityValueMapper = RarityValueMapper
        });
    }

    public double MaxValue => RarityValueMapper.MaxRarity * NoiseHolder.MaxValue;

    public double MinValue => 0.0;

    public double transform(IFunctionContext context, double p_208441_)
    {
        double d0 = RarityValueMapper.GetSphaghettiRarity(p_208441_);
        return d0 * Math.Abs(NoiseHolder.GetValue(context.BlockX / d0, context.BlockY / d0, context.BlockZ / d0));
    }
}
