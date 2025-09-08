using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.DesertPyramidStructure
public class DesertPyramidStructure : SinglePieceStructure
{
    public override StructureType StructureType => StructureType.DESERT_PYRAMID;

    public DesertPyramidStructure()
        : base(/*DesertPyramidPiece::new,*/ 21, 21, new StructureSettings())
    {
    }

    public DesertPyramidStructure(StructureSettings settings)
        : base(/*DesertPyramidPiece::new,*/ 21, 21, settings)
    {
    }
}
