using Generator.World.Level.Levelgen.Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction.Visitor
public interface IDensityVisitor
{
    IDensityFunction Apply(IDensityFunction densityFunction);

    NoiseHolder VisitNoise(NoiseHolder noiseHolder)
    {
        return noiseHolder;
    }
}
