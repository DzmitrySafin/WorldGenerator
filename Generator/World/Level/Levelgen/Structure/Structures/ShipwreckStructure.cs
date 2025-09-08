using Generator.Core;
using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

public class ShipwreckStructure : Structure
{
    public override StructureType StructureType => StructureType.SHIPWRECK;

    [JsonProperty("is_beached")]
    public bool IsBeached { get; set; }

    public ShipwreckStructure()
        : base(new StructureSettings())
    {
    }

    public ShipwreckStructure(StructureSettings settings, bool p_229389_)
        : base(settings)
    {
        IsBeached = p_229389_;
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_229391_)
    //{
    //    Heightmap.Types heightmap$types = this.isBeached ? Heightmap.Types.WORLD_SURFACE_WG : Heightmap.Types.OCEAN_FLOOR_WG;
    //    return onTopOfChunkCenter(p_229391_, heightmap$types, p_229394_-> this.generatePieces(p_229394_, p_229391_));
    //}

    //private void generatePieces(StructurePiecesBuilder p_229396_, Structure.GenerationContext p_229397_)
    //{
    //    Rotation rotation = Rotation.getRandom(p_229397_.random());
    //    BlockPosition blockpos = new BlockPosition(p_229397_.chunkPos().getMinBlockX(), 90, p_229397_.chunkPos().getMinBlockZ());
    //    ShipwreckPieces.ShipwreckPiece shipwreckpieces$shipwreckpiece = ShipwreckPieces.addRandomPiece(p_229397_.structureTemplateManager(), blockpos, rotation, p_229396_, p_229397_.random(), IsBeached);
    //    if (shipwreckpieces$shipwreckpiece.isTooBigToFitInWorldGenRegion()) {
    //        BoundingBox boundingbox = shipwreckpieces$shipwreckpiece.getBoundingBox();
    //        int i;
    //        if (IsBeached)
    //        {
    //            int j = Structure.getLowestY(p_229397_, boundingbox.MinX, boundingbox.getXSpan(), boundingbox.MinZ, boundingbox.getZSpan());
    //            i = shipwreckpieces$shipwreckpiece.calculateBeachedPosition(j, p_229397_.random());
    //        }
    //        else
    //        {
    //            i = Structure.getMeanFirstOccupiedHeight(p_229397_, boundingbox.MinX, boundingbox.getXSpan(), boundingbox.MinZ, boundingbox.getZSpan());
    //        }

    //        shipwreckpieces$shipwreckpiece.adjustPositionHeight(i);
    //    }
    //}
}
