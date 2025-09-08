using Generator.World.Level.Biome;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class StructureBiomesConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        //string attributeName = token.StringTrimNamespaceTag();
        //BiomeTag tag = JToken.FromObject(attributeName).ToObject<BiomeTag>();
        //if (!BiomeTagsProvider.BiomeTagMap.ContainsKey(tag))
        //    throw new ArgumentOutOfRangeException(attributeName);

        //List<BiomeType> biomes = [.. BiomeTagsProvider.BiomeTagMap.Where(e => BiomeTagsProvider.BiomeTagMap[tag].Tags.Contains(e.Key)).SelectMany(e => e.Value.Biomes)];
        //biomes.AddRange(BiomeTagsProvider.BiomeTagMap[tag].Biomes);
        //return biomes.Distinct().ToList();

        return new List<Biome>();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
