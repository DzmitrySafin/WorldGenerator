using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.BuriedTreasureStructure
public class BuriedTreasureStructure : Structure
{
    public override StructureType StructureType => StructureType.BURIED_TREASURE;

    public BuriedTreasureStructure()
        : base(new StructureSettings())
    {
    }

    public BuriedTreasureStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_227387_)
    //{
    //    return onTopOfChunkCenter(p_227387_, Heightmap.Types.OCEAN_FLOOR_WG, p_227390_->generatePieces(p_227390_, p_227387_));
    //}

    //private static void generatePieces(StructurePiecesBuilder p_227392_, Structure.GenerationContext p_227393_)
    //{
    //    BlockPosition blockpos = new BlockPosition(p_227393_.chunkPos().getBlockX(9), 90, p_227393_.chunkPos().getBlockZ(9));
    //    p_227392_.addPiece(new BuriedTreasurePieces.BuriedTreasurePiece(blockpos));
    //}
}
