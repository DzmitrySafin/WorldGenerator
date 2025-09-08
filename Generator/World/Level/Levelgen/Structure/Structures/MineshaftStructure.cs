using Generator.Core;
using Generator.Enums;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

public class MineshaftStructure : Structure
{
    public override StructureType StructureType => StructureType.MINESHAFT;

    [JsonProperty("mineshaft_type")]
    public MineshaftType ShaftType { get; set; }

    public MineshaftStructure()
        : base(new StructureSettings())
    {
    }

    public MineshaftStructure(StructureSettings settings, MineshaftType type)
        : base(settings)
    {
        ShaftType = type;
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_227964_)
    //{
    //    p_227964_.random().nextDouble();
    //    ChunkPosition chunkpos = p_227964_.chunkPos();
    //    BlockPosition blockpos = new BlockPosition(chunkpos.GetMiddleBlockX(), 50, chunkpos.GetMinBlockZ());
    //    StructurePiecesBuilder structurepiecesbuilder = new StructurePiecesBuilder();
    //    int i = this.generatePiecesAndAdjust(structurepiecesbuilder, p_227964_);
    //    return Optional.of(new Structure.GenerationStub(blockpos.offset(0, i, 0), Either.right(structurepiecesbuilder)));
    //}

    //private int generatePiecesAndAdjust(StructurePiecesBuilder p_227966_, Structure.GenerationContext p_227967_)
    //{
    //    ChunkPosition chunkpos = p_227967_.chunkPos();
    //    WorldgenRandom worldgenrandom = p_227967_.random();
    //    ChunkGenerator chunkgenerator = p_227967_.chunkGenerator();
    //    MineshaftPieces.MineShaftRoom mineshaftpieces$mineshaftroom = new MineshaftPieces.MineShaftRoom(0, worldgenrandom, chunkpos.GetBlockX(2), chunkpos.GetBlockZ(2), ShaftType);
    //    p_227966_.addPiece(mineshaftpieces$mineshaftroom);
    //    mineshaftpieces$mineshaftroom.addChildren(mineshaftpieces$mineshaftroom, p_227966_, worldgenrandom);
    //    int i = chunkgenerator.getSeaLevel();
    //    if (ShaftType == MineshaftType.MESA)
    //    {
    //        BlockPosition blockpos = p_227966_.getBoundingBox().getCenter();
    //        int j = chunkgenerator.getBaseHeight(
    //            blockpos.getX(), blockpos.getZ(), Heightmap.Types.WORLD_SURFACE_WG, p_227967_.heightAccessor(), p_227967_.randomState()
    //        );
    //        int k = j <= i ? i : Mth.randomBetweenInclusive(worldgenrandom, i, j);
    //        int l = k - blockpos.getY();
    //        p_227966_.offsetPiecesVertically(l);
    //        return l;
    //    }
    //    else
    //    {
    //        return p_227966_.moveBelowSeaLevel(i, chunkgenerator.getMinY(), worldgenrandom, 10);
    //    }
    //}
}
