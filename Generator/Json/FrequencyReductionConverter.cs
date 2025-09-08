using Generator.Enums;
using Generator.World.Level.Levelgen.Structure.Placement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class FrequencyReductionConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        var reductionType = token.ToObject<FrequencyReductionType>(serializer);

        return new FrequencyReduction
        {
            ReductionType = reductionType
        };
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
