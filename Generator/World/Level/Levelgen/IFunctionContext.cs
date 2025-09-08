using Generator.World.Level.Levelgen.Blending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction.FunctionContext
public interface IFunctionContext
{
    int BlockX { get; protected set; }

    int BlockY { get; protected set; }

    int BlockZ { get; protected set; }

    Blender GetBlender()
    {
        return Blender.EMPTY;
    }
}
