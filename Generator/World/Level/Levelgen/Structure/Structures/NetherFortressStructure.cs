using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.NetherFortressStructure
public class NetherFortressStructure : Structure
{
    public override StructureType StructureType => StructureType.FORTRESS;

    public NetherFortressStructure()
        : base(new StructureSettings())
    {
    }

    public NetherFortressStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_228523_)
    //{
    //    ChunkPosition chunkpos = p_228523_.chunkPos();
    //    BlockPosition blockpos = new BlockPosition(chunkpos.GetMinBlockX(), 64, chunkpos.GetMinBlockZ());
    //    return Optional.of(new Structure.GenerationStub(blockpos, p_228526_->generatePieces(p_228526_, p_228523_)));
    //}

    //private static void generatePieces(StructurePiecesBuilder p_228528_, Structure.GenerationContext p_228529_)
    //{
    //    NetherFortressPieces.StartPiece netherfortresspieces$startpiece = new NetherFortressPieces.StartPiece(
    //        p_228529_.random(), p_228529_.chunkPos().getBlockX(2), p_228529_.chunkPos().getBlockZ(2)
    //    );
    //    p_228528_.addPiece(netherfortresspieces$startpiece);
    //    netherfortresspieces$startpiece.addChildren(netherfortresspieces$startpiece, p_228528_, p_228529_.random());
    //    List<StructurePiece> list = netherfortresspieces$startpiece.pendingChildren;

    //    while (!list.isEmpty())
    //    {
    //        int i = p_228529_.random().nextInt(list.size());
    //        StructurePiece structurepiece = list.remove(i);
    //        structurepiece.addChildren(netherfortresspieces$startpiece, p_228528_, p_228529_.random());
    //    }

    //    p_228528_.moveInsideHeights(p_228529_.random(), 48, 70);
    //}
}
