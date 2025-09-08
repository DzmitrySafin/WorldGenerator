using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Constant
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class ConstantFunction : IDensityFunction
{
    public double Value { get; private set; }

    public ConstantFunction(double value)
    {
        Value = value;
    }

    public static readonly ConstantFunction ZERO = new ConstantFunction(0.0);

    public double Compute(IFunctionContext context)
    {
        return Value;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        Array.Fill(array, Value);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => Value;

    public double MinValue => Value;
}
