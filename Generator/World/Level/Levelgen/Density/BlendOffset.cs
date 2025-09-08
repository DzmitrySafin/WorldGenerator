using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.DensityFunctions.BlendOffset
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class BlendOffset : IDensityFunction
{
    public double Compute(IFunctionContext context)
    {
        return 0.0;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        Array.Fill(array, 0.0);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => 0.0;

    public double MinValue => 0.0;
}
