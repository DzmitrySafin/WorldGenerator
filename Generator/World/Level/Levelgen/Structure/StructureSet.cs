using Generator.Json;
using Generator.World.Level.Levelgen.Structure.Placement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.StructureSet
public class StructureSet
{
    [JsonProperty("structures")]
    public List<StructureSelectionEntry> Structures { get; set; }

    [JsonProperty("placement"), JsonConverter(typeof(StructurePlacementConverter))]
    public StructurePlacement Placement { get; set; }

    public StructureSet()
    {
        // default constructor for JSON deserialization
    }

    public StructureSet(List<StructureSelectionEntry> structures, StructurePlacement placement)
    {
        Structures = structures;
        Placement = placement;
    }

    public StructureSet(Structure structure, StructurePlacement placement)
        : this([new StructureSelectionEntry(structure, 1)], placement)
    {
    }

    public static StructureSelectionEntry entry(Structure structure, int weight)
    {
        return new StructureSelectionEntry(structure, weight);
    }

    public static StructureSelectionEntry entry(Structure structure)
    {
        return new StructureSelectionEntry(structure, 1);
    }
}
