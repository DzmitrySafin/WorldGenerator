using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.BlendAlpha
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class BlendAlpha : IDensityFunction
{
    public double Compute(IFunctionContext context)
    {
        return 1.0;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        Array.Fill(array, 1.0);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => 1.0;

    public double MinValue => 1.0;
}
