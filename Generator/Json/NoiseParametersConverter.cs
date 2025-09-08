using Generator.Enums;
using Generator.Helpers;
using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Synth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

internal class NoiseParametersConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        string noiseName = token.StringTrimNamespace();
        string fileName = Path.Combine(DataFolderPath, "worldgen", "noise", $"{noiseName}.json");
        if (!File.Exists(fileName))
            throw new FileNotFoundException(fileName);

        using (StreamReader sr = new StreamReader(fileName))
        {
            using (JsonReader jr = new JsonTextReader(sr))
            {
                NoiseParameters parameters = serializer.Deserialize<NoiseParameters>(jr)!;
                parameters.NoiseType = EnumHelper.GetEnumFromMemberValue<NoiseType>(noiseName);
                return new NoiseHolder(parameters);
            }
        }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
