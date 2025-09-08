using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Density;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Util;

//source: net.minecraft.util.CubicSpline.Multipoint
public class SplineMultipoint : ISplinePoint
{
    [JsonProperty("location")]
    public float Location { get; set; }

    [JsonProperty("value")]
    public required SplineFunction Value { get; set; }

    [JsonProperty("derivative")]
    public float Derivative { get; set; }

    //private float[] locations;
    //private float[] derivatives;

    public float Apply(IFunctionContext context)
    {
        return Value.Apply(context);
    }

    public ISplinePoint MapAll(IDensityVisitor densityVisitor)
    {
        return new SplineMultipoint
        {
            Location = Location,
            Value = (SplineFunction)Value.MapAll(densityVisitor),
            Derivative = Derivative
        };
    }

    public float MinValue => (float)Value.MinValue;

    public float MaxValue => (float)Value.MaxValue;
}
