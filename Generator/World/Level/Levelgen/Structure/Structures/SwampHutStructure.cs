using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.SwampHutStructure
public class SwampHutStructure : Structure
{
    public override StructureType StructureType => StructureType.SWAMP_HUT;

    public SwampHutStructure()
        : base(new StructureSettings())
    {
    }

    public SwampHutStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_229976_)
    //{
    //    return onTopOfChunkCenter(p_229976_, Heightmap.Types.WORLD_SURFACE_WG, p_229979_->generatePieces(p_229979_, p_229976_));
    //}

    //private static void generatePieces(StructurePiecesBuilder p_229981_, Structure.GenerationContext p_229982_)
    //{
    //    p_229981_.addPiece(new SwampHutPiece(p_229982_.random(), p_229982_.chunkPos().getMinBlockX(), p_229982_.chunkPos().getMinBlockZ()));
    //}
}
