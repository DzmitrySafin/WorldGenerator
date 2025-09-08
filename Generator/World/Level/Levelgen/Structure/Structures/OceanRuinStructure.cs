using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.OceanRuinStructure
public class OceanRuinStructure : Structure
{
    public override StructureType StructureType => StructureType.OCEAN_RUIN;

    [JsonProperty("biome_temp")]
    public TemperatureType BiomeTemp { get; set; }

    [JsonProperty("large_probability")]
    public float LargeProbability { get; set; }

    [JsonProperty("cluster_probability")]
    public float ClusterProbability { get; set; }

    public OceanRuinStructure()
        : base(new StructureSettings())
    {
    }

    public OceanRuinStructure(StructureSettings settings, TemperatureType p_229061_, float p_229062_, float p_229063_)
        : base(settings)
    {
        BiomeTemp = p_229061_;
        LargeProbability = p_229062_;
        ClusterProbability = p_229063_;
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_229065_)
    //{
    //    return onTopOfChunkCenter(p_229065_, Heightmap.Types.OCEAN_FLOOR_WG, p_229068_-> this.generatePieces(p_229068_, p_229065_));
    //}

    //private void generatePieces(StructurePiecesBuilder p_229070_, Structure.GenerationContext p_229071_)
    //{
    //    BlockPosition blockpos = new BlockPosition(p_229071_.chunkPos().getMinBlockX(), 90, p_229071_.chunkPos().getMinBlockZ());
    //    Rotation rotation = Rotation.getRandom(p_229071_.random());
    //    OceanRuinPieces.addPieces(p_229071_.structureTemplateManager(), blockpos, rotation, p_229070_, p_229071_.random(), this);
    //}
}
