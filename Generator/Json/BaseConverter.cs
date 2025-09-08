using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

public abstract class BaseConverter : JsonConverter
{
    public static string DataFolderPath { get; set; } = ".";

    public static JsonSerializerSettings SerializerSettings { get; } = new()
    {
        ContractResolver = new ConverterContractResolver()
    };
}
