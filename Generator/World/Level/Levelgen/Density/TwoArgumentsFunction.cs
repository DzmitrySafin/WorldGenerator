using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.TwoArgumentSimpleFunction
public class TwoArgumentsFunction : IDensityFunction
{
    [JsonProperty("type")]
    public TwoArgumentsType TwoArgsType { get; set; }

    [JsonProperty("argument1")]
    public required IDensityFunction InputArgument1 { get; set; }

    [JsonProperty("argument2")]
    public required IDensityFunction InputArgument2 { get; set; }

    protected double minValue;
    protected double maxValue;

    public TwoArgumentsFunction()
    {
        // default constructor for JSON deserialization
    }

    public static TwoArgumentsFunction Create(TwoArgumentsType twoArgsType, IDensityFunction inputDensity1, IDensityFunction inputDensity2)
    {
        double d0 = inputDensity1.MinValue;
        double d1 = inputDensity2.MinValue;
        double d2 = inputDensity1.MaxValue;
        double d3 = inputDensity2.MaxValue;
        if (twoArgsType == TwoArgumentsType.MIN || twoArgsType == TwoArgumentsType.MAX)
        {
            bool flag = d0 >= d3;
            bool flag1 = d1 >= d2;
            if (flag || flag1)
            {
                //LOGGER.warn("Creating a " + twoArgsType + " function between two non-overlapping inputs: " + inputDensity1 + " and " + inputDensity2);
                Console.WriteLine($"Creating a {twoArgsType} function between two non-overlapping inputs: {inputDensity1} and {inputDensity2}");
            }
        }
        double d5 = twoArgsType switch
        {
            TwoArgumentsType.ADD => d0 + d1,
            TwoArgumentsType.MUL => d0 > 0.0 && d1 > 0.0 ? d0 * d1 : (d2 < 0.0 && d3 < 0.0 ? d2 * d3 : Math.Min(d0 * d3, d2 * d1)),
            TwoArgumentsType.MIN => Math.Min(d0, d1),
            TwoArgumentsType.MAX => Math.Max(d0, d1),
            _ => throw new NotImplementedException()
        };

        double d4 = twoArgsType switch
        {
            TwoArgumentsType.ADD => d2 + d3,
            TwoArgumentsType.MUL => d0 > 0.0 && d1 > 0.0 ? d2 * d3 : (d2 < 0.0 && d3 < 0.0 ? d0 * d1 : Math.Max(d0 * d1, d2 * d3)),
            TwoArgumentsType.MIN => Math.Min(d2, d3),
            TwoArgumentsType.MAX => Math.Max(d2, d3),
            _ => throw new NotImplementedException()
        };

        if (twoArgsType == TwoArgumentsType.MUL || twoArgsType == TwoArgumentsType.ADD)
        {
            if (inputDensity1 is ConstantFunction constant1) {
                return new MulOrAdd(
                    twoArgsType,
                    inputDensity2,
                    d5,
                    d4,
                    constant1.Value
                );
            }

            if (inputDensity2 is ConstantFunction constant2) {
                return new MulOrAdd(
                    twoArgsType,
                    inputDensity1,
                    d5,
                    d4,
                    constant2.Value
                );
            }
        }

        return new Ap2(twoArgsType, inputDensity1, inputDensity2, d5, d4);
    }

    public virtual double Compute(IFunctionContext context)
    {
        throw new NotImplementedException("Must be overridden in derived class");
    }

    public virtual void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        throw new NotImplementedException("Must be overridden in derived class");
    }

    public virtual IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(Create(TwoArgsType, InputArgument1.MapAll(densityVisitor), InputArgument2.MapAll(densityVisitor)));
    }

    public double MaxValue => maxValue;

    public double MinValue => minValue;
}
