using Generator.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class ResourceLocationConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        if (token.Type != JTokenType.String)
            throw new NotSupportedException("Expect string for JToken");

        string value = token.Value<string>()!;
        return ResourceLocation.Parse(value);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
