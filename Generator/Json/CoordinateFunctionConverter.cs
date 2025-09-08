using Generator.Helpers;
using Generator.World.Level.Levelgen;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public class CoordinateFunctionConverter : BaseConverter
{
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        string noiseName = token.StringTrimNamespace();
        string fileName = Path.Combine(DataFolderPath, "worldgen", "density_function", $"{noiseName}.json");
        if (!File.Exists(fileName))
            throw new FileNotFoundException(fileName);

        using (StreamReader sr = new StreamReader(fileName))
        {
            using (JsonReader jr = new JsonTextReader(sr))
            {
                return new CoordinateFunction
                {
                    InnerFunction = serializer.Deserialize<IDensityFunction>(jr)!
                };
            }
        }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
