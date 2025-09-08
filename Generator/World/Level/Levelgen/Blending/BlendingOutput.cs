using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Blending;

//source: net.minecraft.world.level.levelgen.blending.Blender.BlendingOutput
public class BlendingOutput
{
    public double Alpha { get; private set; }
    public double BlendingOffset { get; private set; }

    public BlendingOutput(double alpha, double blendingOffset)
    {
        Alpha = alpha;
        BlendingOffset = blendingOffset;
    }
}
