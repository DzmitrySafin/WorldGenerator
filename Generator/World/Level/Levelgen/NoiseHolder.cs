using Generator.World.Level.Levelgen.Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction.NoiseHolder
public class NoiseHolder
{
    public NoiseParameters NoiseData { get; private set; }

    public NormalNoise? Noise { get; private set; }

    public NoiseHolder(NoiseParameters parameters, NormalNoise? noise)
    {
        NoiseData = parameters;
        Noise = noise;
    }

    public NoiseHolder(NoiseParameters parameters)
        : this(parameters, null)
    {
    }

    public double GetValue(double p_224007_, double p_224008_, double p_224009_)
    {
        return Noise?.GetValue(p_224007_, p_224008_, p_224009_) ?? 0.0;
    }

    public double MaxValue => Noise?.MaxValue ?? 2.0;
}
