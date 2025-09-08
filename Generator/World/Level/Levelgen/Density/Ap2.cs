using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Ap2
public class Ap2 : TwoArgumentsFunction
{
    [SetsRequiredMembers]
    public Ap2(TwoArgumentsType twoArgsType, IDensityFunction inputDensity1, IDensityFunction inputDensity2, double minValue, double maxValue)
    {
        TwoArgsType = twoArgsType;
        InputArgument1 = inputDensity1;
        InputArgument2 = inputDensity2;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    public override double Compute(IFunctionContext context)
    {
        double d0 = InputArgument1.Compute(context);

        return TwoArgsType switch
        {
            TwoArgumentsType.ADD => d0 + InputArgument2.Compute(context),
            TwoArgumentsType.MUL => d0 == 0.0 ? 0.0 : d0 * InputArgument2.Compute(context),
            TwoArgumentsType.MIN => d0 < InputArgument2.MinValue ? d0 : Math.Min(d0, InputArgument2.Compute(context)),
            TwoArgumentsType.MAX => d0 > InputArgument2.MaxValue ? d0 : Math.Max(d0, InputArgument2.Compute(context)),
            _ => throw new NotImplementedException()
        };
    }

    public override void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        InputArgument1.FillArray(array, contextProvider);
        switch (TwoArgsType)
        {
            case TwoArgumentsType.ADD:
                double[] adouble = new double[array.Length];
                InputArgument2.FillArray(adouble, contextProvider);

                for (int k = 0; k < array.Length; k++)
                {
                    array[k] += adouble[k];
                }
                break;
            case TwoArgumentsType.MUL:
                for (int j = 0; j < array.Length; j++)
                {
                    double d1 = array[j];
                    array[j] = d1 == 0.0 ? 0.0 : d1 * InputArgument2.Compute(contextProvider.ForIndex(j));
                }
                break;
            case TwoArgumentsType.MIN:
                double d3 = InputArgument2.MinValue;

                for (int l = 0; l < array.Length; l++)
                {
                    double d4 = array[l];
                    array[l] = d4 < d3 ? d4 : Math.Min(d4, InputArgument2.Compute(contextProvider.ForIndex(l)));
                }
                break;
            case TwoArgumentsType.MAX:
                double d0 = InputArgument2.MaxValue;

                for (int i = 0; i < array.Length; i++)
                {
                    double d2 = array[i];
                    array[i] = d2 > d0 ? d2 : Math.Max(d2, InputArgument2.Compute(contextProvider.ForIndex(i)));
                }
                break;
        }
    }

    public override IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(TwoArgumentsFunction.Create(TwoArgsType, InputArgument1.MapAll(densityVisitor), InputArgument2.MapAll(densityVisitor)));
    }
}
