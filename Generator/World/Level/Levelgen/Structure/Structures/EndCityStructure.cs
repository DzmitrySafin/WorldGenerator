using Generator.Core;
using Generator.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.EndCityStructure
public class EndCityStructure : Structure
{
    public override StructureType StructureType => StructureType.END_CITY;

    public EndCityStructure()
        : base(new StructureSettings())
    {
    }

    public EndCityStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_227528_)
    //{
    //    Rotation rotation = Rotation.getRandom(p_227528_.random());
    //    BlockPosition blockpos = this.getLowestYIn5by5BoxOffset7Blocks(p_227528_, rotation);
    //    return blockpos.getY() < 60
    //        ? Optional.empty()
    //        : Optional.of(new Structure.GenerationStub(blockpos, p_227538_-> this.generatePieces(p_227538_, blockpos, rotation, p_227528_)));
    //}

    //private void generatePieces(StructurePiecesBuilder p_227530_, BlockPos p_227531_, Rotation p_227532_, Structure.GenerationContext p_227533_)
    //{
    //    List<StructurePiece> list = Lists.newArrayList();
    //    EndCityPieces.startHouseTower(p_227533_.structureTemplateManager(), p_227531_, p_227532_, list, p_227533_.random());
    //    list.forEach(p_227530_::addPiece);
    //}
}
