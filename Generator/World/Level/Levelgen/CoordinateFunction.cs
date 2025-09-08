using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunctions.Spline.Coordinate
//source: net.minecraft.util.ToFloatFunction
public class CoordinateFunction
{
    public required IDensityFunction InnerFunction { get; set; }

    public float Apply(IFunctionContext context)
    {
        return (float)InnerFunction.Compute(context);
    }

    public float MinValue()
    {
        // assuming Holder<IDensityFunction>.isBound() is always true
        //return InnerFunction.isBound() ? (float)InnerFunction.MinValue() : float.NegativeInfinity;
        return (float)InnerFunction.MinValue;
    }

    public float MaxValue()
    {
        // assuming Holder<IDensityFunction>.isBound() is always true
        //return InnerFunction.isBound() ? (float)InnerFunction.MaxValue() : float.PositiveInfinity;
        return (float)InnerFunction.MaxValue;
    }

    public CoordinateFunction MapAll(IDensityVisitor densityVisitor)
    {
        return new CoordinateFunction
        {
            InnerFunction = InnerFunction.MapAll(densityVisitor)
        };
    }
}
