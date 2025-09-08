using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.MulOrAdd
//source: net.minecraft.world.level.levelgen.DensityFunctions.PureTransformer
public class MulOrAdd : TwoArgumentsFunction
{
    private double argument;

    [SetsRequiredMembers]
    public MulOrAdd(TwoArgumentsType twoArgsType, IDensityFunction inputDensity2, double minValue, double maxValue, double argument)
    {
        TwoArgsType = twoArgsType;
        InputArgument1 = new ConstantFunction(argument);
        InputArgument2 = inputDensity2;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.argument = argument;
    }

    public override double Compute(IFunctionContext context)
    {
        return transform(InputArgument2.Compute(context));
    }

    public override void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputArgument2.FillArray(array, contextProvider);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = transform(array[i]);
        }
    }

    public override IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        IDensityFunction densityFunction = InputArgument2.MapAll(densityVisitor);
        double d0 = densityFunction.MinValue;
        double d1 = densityFunction.MaxValue;
        double d2;
        double d3;
        if (TwoArgsType == TwoArgumentsType.ADD)
        {
            d2 = d0 + argument;
            d3 = d1 + argument;
        }
        else if (argument >= 0.0)
        {
            d2 = d0 * argument;
            d3 = d1 * argument;
        }
        else
        {
            d2 = d1 * argument;
            d3 = d0 * argument;
        }

        return new MulOrAdd(TwoArgsType, densityFunction, d2, d3, argument);
    }

    private double transform(double p_208759_)
    {
        return TwoArgsType switch
        {
            TwoArgumentsType.MUL => p_208759_ * argument,
            TwoArgumentsType.ADD => p_208759_ + argument,
            _ => throw new NotImplementedException()
        };
    }
}
