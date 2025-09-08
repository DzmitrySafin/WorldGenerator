using Generator.World.Level.Levelgen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Util;

//source: net.minecraft.util.CubicSpline.Constant
public class SplineSinglepoint : ISplinePoint
{
    [JsonProperty("location")]
    public float Location { get; set; }

    [JsonProperty("value")]
    public float Value { get; set; }

    [JsonProperty("derivative")]
    public float Derivative { get; set; }

    public float Apply(IFunctionContext context)
    {
        return Value;
    }

    public ISplinePoint MapAll(IDensityVisitor densityVisitor)
    {
        return this;
    }

    public float MinValue => Value;

    public float MaxValue => Value;
}
