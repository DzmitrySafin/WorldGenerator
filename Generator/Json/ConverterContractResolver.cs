using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Structure;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Json;

internal class ConverterContractResolver : DefaultContractResolver
{
    protected override JsonContract CreateContract(Type objectType)
    {
        JsonContract contract = base.CreateContract(objectType);

        if (objectType == typeof(IDensityFunction))
        {
            contract.Converter = new DensityFunctionConverter();
        }
        else if (objectType == typeof(Structure))
        {
            contract.Converter = new StructureConverter();
        }

        return contract;
    }
}
