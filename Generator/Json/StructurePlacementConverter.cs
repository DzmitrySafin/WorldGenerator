using Generator.Helpers;
using Generator.World.Level.Levelgen.Structure.Placement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class StructurePlacementConverter : BaseConverter
{
    private Dictionary<string, Type> placementTypes = new()
    {
        { "random_spread", typeof(RandomSpreadStructurePlacement) },
        { "concentric_rings", typeof(ConcentricRingsStructurePlacement) },
    };

    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        token.AssertChildExists("type");
        string type = token["type"]!.StringTrimNamespace();

        if (placementTypes.ContainsKey(type))
        {
            return token.ToObject(placementTypes[type], serializer);
        }

        throw new NotSupportedException($"StructurePlacement type {type} is not supported");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
