using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.StructureSet.StructureSelectionEntry
public class StructureSelectionEntry
{
    [JsonProperty("structure")]
    public Structure Structure { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    public StructureSelectionEntry(Structure structure, int weight)
    {
        Structure = structure;
        Weight = weight;
    }
}
