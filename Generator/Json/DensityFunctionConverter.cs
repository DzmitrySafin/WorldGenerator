using Generator.Helpers;
using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Density;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

internal class DensityFunctionConverter : BaseConverter
{
    private Dictionary<string, Type> densityTypes = new()
    {
        { "blend_alpha", typeof(BlendAlpha) },
        { "blend_density", typeof(BlendDensity) },
        { "blend_offset", typeof(BlendOffset) },
        { "clamp", typeof(ClampFunction) },
        { "noise", typeof(ScaleNoise) },
        { "old_blended_noise", typeof(BlendedNoise) },
        { "range_choice", typeof(RangeChoice) },
        { "shift_a", typeof(ShiftA) },
        { "shift_b", typeof(ShiftB) },
        { "shifted_noise", typeof(ShiftedNoise) },
        { "spline", typeof(SplineFunction) },
        { "weird_scaled_sampler", typeof(WeirdScaledSampler) },
        { "y_clamped_gradient", typeof(YClampedGradient) },
        { "add", typeof(TwoArgumentsFunction) },
        { "max", typeof(TwoArgumentsFunction) },
        { "min", typeof(TwoArgumentsFunction) },
        { "mul", typeof(TwoArgumentsFunction) },
        { "interpolated", typeof(MarkerFunction) },
        { "flat_cache", typeof(MarkerFunction) },
        { "cache_2d", typeof(MarkerFunction) },
        { "cache_once", typeof(MarkerFunction) },
        { "cache_all_in_cell", typeof(MarkerFunction) },
        { "abs", typeof(MappedFunction) },
        { "square", typeof(MappedFunction) },
        { "cube", typeof(MappedFunction) },
        { "half_negative", typeof(MappedFunction) },
        { "quarter_negative", typeof(MappedFunction) },
        { "squeeze", typeof(MappedFunction) },
    };

    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        if (token.Type == JTokenType.String)
        {
            string noiseName = token.StringTrimNamespace();
            string fileName = Path.Combine(DataFolderPath, "worldgen", "density_function", $"{noiseName}.json");
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            using (StreamReader sr = new StreamReader(fileName))
            {
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<IDensityFunction>(jr);
                }
            }
        }

        if (token.Type == JTokenType.Float)
        {
            return new ConstantFunction(token.ToObject<double>());
        }

        token.AssertChildExists("type");
        string type = token["type"]!.StringTrimNamespace();

        if (type == "spline")
        {
            token.AssertChildExists("spline");
            token = token["spline"]!;
        }

        if (densityTypes.ContainsKey(type))
        {
            return token.ToObject(densityTypes[type], serializer);
        }

        throw new NotSupportedException($"Density type {type} is not supported");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
