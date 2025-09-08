using Generator.World.Level.Levelgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Util;

//source: net.minecraft.util.CubicSpline
//source: net.minecraft.util.ToFloatFunction
public interface ISplinePoint
{
    float Apply(IFunctionContext context);

    float MinValue { get; }

    float MaxValue { get; }

    ISplinePoint MapAll(IDensityVisitor densityVisitor);

    float Location { get; set; }
    //float Value { get; set; }
    float Derivative { get; set; }
}
