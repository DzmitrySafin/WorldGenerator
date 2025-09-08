using Generator.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class SplinePointsConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        var points = new List<ISplinePoint>();
        foreach (var tokenPt in token)
        {
            var tokenVal = tokenPt["value"]!;
            if (tokenVal.Type == JTokenType.Float)
            {
                points.Add(tokenPt.ToObject<SplineSinglepoint>()!);
            }
            else
            {
                points.Add(tokenPt.ToObject<SplineMultipoint>(serializer)!);
            }
        }

        return points;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
