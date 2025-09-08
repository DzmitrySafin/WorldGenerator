using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.IglooStructure
public class IglooStructure : Structure
{
    public override StructureType StructureType => StructureType.IGLOO;

    public IglooStructure()
        : base(new StructureSettings())
    {
    }

    public IglooStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_227595_)
    //{
    //    return onTopOfChunkCenter(p_227595_, Heightmap.Types.WORLD_SURFACE_WG, p_227598_-> this.generatePieces(p_227598_, p_227595_));
    //}

    //private void generatePieces(StructurePiecesBuilder p_227600_, Structure.GenerationContext p_227601_)
    //{
    //    ChunkPosition chunkpos = p_227601_.chunkPos();
    //    WorldgenRandom worldgenrandom = p_227601_.random();
    //    BlockPosition blockpos = new BlockPos(chunkpos.getMinBlockX(), 90, chunkpos.getMinBlockZ());
    //    Rotation rotation = Rotation.getRandom(worldgenrandom);
    //    IglooPieces.addPieces(p_227601_.structureTemplateManager(), blockpos, rotation, p_227600_, worldgenrandom);
    //}
}
