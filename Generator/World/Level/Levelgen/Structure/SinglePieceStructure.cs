using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.SinglePieceStructure
public abstract class SinglePieceStructure : Structure
{
    //private readonly SinglePieceStructure.PieceConstructor constructor;
    private readonly int width;
    private readonly int depth;

    protected SinglePieceStructure(
        //SinglePieceStructure.PieceConstructor p_226537_,
        int p_226538_,
        int p_226539_,
        StructureSettings p_226540_)
        : base(p_226540_)
    {
        //this.constructor = p_226537_;
        this.width = p_226538_;
        this.depth = p_226539_;
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_226542_)
    //{
    //    return getLowestY(p_226542_, this.width, this.depth) < p_226542_.chunkGenerator().getSeaLevel()
    //        ? Optional.empty()
    //        : onTopOfChunkCenter(p_226542_, Heightmap.Types.WORLD_SURFACE_WG, p_226545_-> this.generatePieces(p_226545_, p_226542_));
    //}

    //private void generatePieces(StructurePiecesBuilder p_226547_, Structure.GenerationContext p_226548_)
    //{
    //    ChunkPos chunkpos = p_226548_.chunkPos();
    //    p_226547_.addPiece(this.constructor.construct(p_226548_.random(), chunkpos.getMinBlockX(), chunkpos.getMinBlockZ()));
    //}

    //@FunctionalInterface
    //protected interface PieceConstructor
    //{
    //    StructurePiece construct(WorldgenRandom p_226550_, int p_226551_, int p_226552_);
    //}
}
