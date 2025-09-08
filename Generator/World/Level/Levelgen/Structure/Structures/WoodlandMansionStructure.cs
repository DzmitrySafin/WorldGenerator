using Generator.Core;
using Generator.Enums;
using Generator.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.WoodlandMansionStructure
public class WoodlandMansionStructure : Structure
{
    public override StructureType StructureType => StructureType.WOODLAND_MANSION;

    public WoodlandMansionStructure()
        : base(new StructureSettings())
    {
    }

    public WoodlandMansionStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_230235_)
    //{
    //    Rotation rotation = Rotation.getRandom(p_230235_.random());
    //    BlockPosition blockpos = this.getLowestYIn5by5BoxOffset7Blocks(p_230235_, rotation);
    //    return blockpos.getY() < 60
    //        ? Optional.empty()
    //        : Optional.of(new Structure.GenerationStub(blockpos, p_230240_-> this.generatePieces(p_230240_, p_230235_, blockpos, rotation)));
    //}

    //private void generatePieces(StructurePiecesBuilder p_230242_, Structure.GenerationContext p_230243_, BlockPos p_230244_, Rotation p_230245_)
    //{
    //    List<WoodlandMansionPieces.WoodlandMansionPiece> list = Lists.newLinkedList();
    //    WoodlandMansionPieces.generateMansion(p_230243_.structureTemplateManager(), p_230244_, p_230245_, list, p_230243_.random());
    //    list.forEach(p_230242_::addPiece);
    //}

    //public void afterPlace(
    //    WorldGenLevel p_230227_,
    //    StructureManager p_230228_,
    //    ChunkGenerator p_230229_,
    //    IRandomSource p_230230_,
    //    BoundingBox p_230231_,
    //    ChunkPosition p_230232_,
    //    PiecesContainer p_230233_
    //)
    //{
    //    BlockPosition.MutableBlockPos blockpos$mutableblockpos = new BlockPosition.MutableBlockPos();
    //    int i = p_230227_.getMinY();
    //    BoundingBox boundingbox = p_230233_.calculateBoundingBox();
    //    int j = boundingbox.MinY;

    //    for (int k = p_230231_.MinX; k <= p_230231_.MaxX; k++)
    //    {
    //        for (int l = p_230231_.MinZ; l <= p_230231_.MaxZ; l++)
    //        {
    //            blockpos$mutableblockpos.set(k, j, l);
    //            if (!p_230227_.isEmptyBlock(blockpos$mutableblockpos)
    //                && boundingbox.isInside(blockpos$mutableblockpos)
    //                && p_230233_.isInsidePiece(blockpos$mutableblockpos))
    //            {
    //                for (int i1 = j - 1; i1 > i; i1--)
    //                {
    //                    blockpos$mutableblockpos.setY(i1);
    //                    if (!p_230227_.isEmptyBlock(blockpos$mutableblockpos) && !p_230227_.getBlockState(blockpos$mutableblockpos).liquid())
    //                    {
    //                        break;
    //                    }

    //                    p_230227_.setBlock(blockpos$mutableblockpos, Blocks.COBBLESTONE.defaultBlockState(), 2);
    //                }
    //            }
    //        }
    //    }
    //}
}
