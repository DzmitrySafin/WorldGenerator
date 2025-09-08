using Generator.Core;
using Generator.World.Level.Levelgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate.Sampler
public class ClimateSampler
{
    public IDensityFunction Temperature { get; private set; }
    public IDensityFunction Humidity { get; private set; }
    public IDensityFunction Continentalness { get; private set; }
    public IDensityFunction Erosion { get; private set; }
    public IDensityFunction Depth { get; private set; }
    public IDensityFunction Weirdness { get; private set; }
    public List<ClimateParameterPoint> SpawnTarget { get; private set; }

    public ClimateSampler(
        IDensityFunction temperature,
        IDensityFunction humidity,
        IDensityFunction continentalness,
        IDensityFunction erosion,
        IDensityFunction depth,
        IDensityFunction weirdness,
        List<ClimateParameterPoint> spawnTarget
    )
    {
        Temperature = temperature;
        Humidity = humidity;
        Continentalness = continentalness;
        Erosion = erosion;
        Depth = depth;
        Weirdness = weirdness;
        SpawnTarget = spawnTarget;
    }

    public ClimateTargetPoint Sample(int p_186975_, int p_186976_, int p_186977_)
    {
        int i = QuartPosition.ToBlock(p_186975_);
        int j = QuartPosition.ToBlock(p_186976_);
        int k = QuartPosition.ToBlock(p_186977_);
        IFunctionContext singlePoint = new SinglePointContext(i, j, k);

        return Climate.Target(
            (float)Temperature.Compute(singlePoint),
            (float)Humidity.Compute(singlePoint),
            (float)Continentalness.Compute(singlePoint),
            (float)Erosion.Compute(singlePoint),
            (float)Depth.Compute(singlePoint),
            (float)Weirdness.Compute(singlePoint)
        );
    }

    public BlockPosition FindSpawnPosition()
    {
        return !SpawnTarget.Any() ? BlockPosition.ZERO : Climate.FindSpawnPosition(SpawnTarget, this);
    }
}
