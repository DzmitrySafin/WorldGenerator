using Generator.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class Vec3iConverter : BaseConverter
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
            int[] values = token.ToObject<int[]>()!;
            if (values.Length != 3) throw new ArgumentOutOfRangeException("Expected three values for Vec3i");
            return new Vec3i(values[0], values[1], values[2]);
        }

        throw new NotImplementedException("Failed to read Vec3i values");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
