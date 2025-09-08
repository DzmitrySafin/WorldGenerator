using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.JungleTempleStructure
public class JungleTempleStructure : SinglePieceStructure
{
    public override StructureType StructureType => StructureType.JUNGLE_TEMPLE;

    public JungleTempleStructure()
        : base(/*JungleTemplePiece::new,*/ 12, 15, new StructureSettings())
    {
    }

    public JungleTempleStructure(StructureSettings p_226540_)
        : base(/*JungleTemplePiece::new,*/ 12, 15, p_226540_)
    {
    }
}
