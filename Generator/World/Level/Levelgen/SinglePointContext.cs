using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction.SinglePointContext
public class SinglePointContext : IFunctionContext
{
    public int BlockX { get; set; }
    public int BlockY { get; set; }
    public int BlockZ { get; set; }

    public SinglePointContext(int x, int y, int z)
    {
        BlockX = x;
        BlockY = y;
        BlockZ = z;
    }
}
