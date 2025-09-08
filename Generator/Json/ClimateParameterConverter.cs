using Generator.World.Level.Biome;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class ClimateParameterConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        if (token.Type == JTokenType.Array)
        {
            float[] values = token.ToObject<float[]>()!;
            if (values.Length != 2) throw new ArgumentOutOfRangeException("Expected two values for ClimateParameter");
            return new ClimateParameter(values[0], values[1]);
        }
        else if (token.Type == JTokenType.Float)
        {
            float value = token.ToObject<float>();
            return new ClimateParameter(value, value);
        }

        throw new NotImplementedException("Failed to read ClimateParameter values");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
