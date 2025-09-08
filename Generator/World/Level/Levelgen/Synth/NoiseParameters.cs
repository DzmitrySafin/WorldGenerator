using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Synth;

//source: net.minecraft.world.level.levelgen.synth.NormalNoise.NoiseParameters
public class NoiseParameters
{
    [JsonProperty("firstOctave")]
    public int FirstOctave { get; set; }

    [JsonProperty("amplitudes")]
    public required List<double> Amplitudes { get; set; }

    public NoiseType NoiseType { get; set; }

    public NoiseParameters()
    {
        // default constructor for JSON deserialization
    }

    [SetsRequiredMembers]
    public NoiseParameters(int octave, double[] amplitudes)
    {
        FirstOctave = octave;
        Amplitudes = [.. amplitudes];
    }

    [SetsRequiredMembers]
    public NoiseParameters(int octave, double amplitude, params double[] amplitudes)
        : this(octave, [.. amplitudes])
    {
        Amplitudes.Insert(0, amplitude);
    }
}
