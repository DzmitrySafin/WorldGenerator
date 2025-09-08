using Generator.Core;
using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Density;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate
public class Climate
{
    //private static readonly bool DEBUG_SLOW_BIOME_SEARCH = false;
    private static readonly float QUANTIZATION_FACTOR = 10000.0F;
    protected static readonly int PARAMETER_COUNT = 7;

    public static ClimateTargetPoint Target(float p_186782_, float p_186783_, float p_186784_, float p_186785_, float p_186786_, float p_186787_)
    {
        return new ClimateTargetPoint(QuantizeCoord(p_186782_), QuantizeCoord(p_186783_), QuantizeCoord(p_186784_), QuantizeCoord(p_186785_), QuantizeCoord(p_186786_), QuantizeCoord(p_186787_));
    }

    public static ClimateParameterPoint Parameters(float temperature, float humidity, float continentalness, float erosion, float depth, float weirdness, float offset)
    {
        return new ClimateParameterPoint(
            ClimateParameter.Point(temperature),
            ClimateParameter.Point(humidity),
            ClimateParameter.Point(continentalness),
            ClimateParameter.Point(erosion),
            ClimateParameter.Point(depth),
            ClimateParameter.Point(weirdness),
            QuantizeCoord(offset)
        );
    }

    public static ClimateParameterPoint Parameters(
        ClimateParameter temperature,
        ClimateParameter humidity,
        ClimateParameter continentalness,
        ClimateParameter erosion,
        ClimateParameter depth,
        ClimateParameter weirdness,
        float offset
    )
    {
        return new ClimateParameterPoint(temperature, humidity, continentalness, erosion, depth, weirdness, QuantizeCoord(offset));
    }

    public static long QuantizeCoord(float p_186780_)
    {
        return (long)(p_186780_ * QUANTIZATION_FACTOR);
    }

    public static float UnquantizeCoord(long p_186797_)
    {
        return p_186797_ / QUANTIZATION_FACTOR;
    }

    public static ClimateSampler Empty()
    {
        IDensityFunction densityfunction = ConstantFunction.ZERO;
        return new ClimateSampler(densityfunction, densityfunction, densityfunction, densityfunction, densityfunction, densityfunction, []);
    }

    public static BlockPosition FindSpawnPosition(List<ClimateParameterPoint> p_207843_, ClimateSampler sampler)
    {
        return (new ClimateSpawnFinder(p_207843_, sampler)).SpawnResult.location;
    }
}
