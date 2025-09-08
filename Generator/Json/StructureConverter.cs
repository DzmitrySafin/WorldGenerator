using Generator.Helpers;
using Generator.World.Level.Levelgen.Structure;
using Generator.World.Level.Levelgen.Structure.Structures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class StructureConverter : BaseConverter
{
    private Dictionary<string, Type> structureTypes = new()
    {
        { "jigsaw", typeof(JigsawStructure) },
        { "buried_treasure", typeof(BuriedTreasureStructure) },
        { "desert_pyramid", typeof(DesertPyramidStructure) },
        { "end_city", typeof(EndCityStructure) },
        { "igloo", typeof(IglooStructure) },
        { "jungle_temple", typeof(JungleTempleStructure) },
        { "mineshaft", typeof(MineshaftStructure) },
        { "mineshaft_mesa", typeof(MineshaftStructure) },
        { "fortress", typeof(NetherFortressStructure) },
        { "nether_fossil", typeof(NetherFossilStructure) },
        { "ocean_monument", typeof(OceanMonumentStructure) },
        { "ocean_ruin", typeof(OceanRuinStructure) },
        { "ruined_portal", typeof(RuinedPortalStructure) },
        { "shipwreck", typeof(ShipwreckStructure) },
        { "stronghold", typeof(StrongholdStructure) },
        { "swamp_hut", typeof(SwampHutStructure) },
        { "woodland_mansion", typeof(WoodlandMansionStructure) },
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
            string structureName = token.StringTrimNamespace();
            string fileName = Path.Combine(DataFolderPath, "worldgen", "structure", $"{structureName}.json");
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            using (StreamReader sr = new StreamReader(fileName))
            {
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<Structure>(jr);
                }
            }
        }

        token.AssertChildExists("type");
        string type = token["type"]!.StringTrimNamespace();

        if (structureTypes.ContainsKey(type))
        {
            var structure = token.ToObject(structureTypes[type], serializer)!;
            ((Structure)structure).Settings = token.ToObject<StructureSettings>(serializer)!;
            return structure;
        }

        throw new NotSupportedException($"Structure type {type} is not supported");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
