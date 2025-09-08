using Generator.Core;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate.SpawnFinder
public class ClimateSpawnFinder
{
    private static readonly long MAX_RADIUS = 2048L;
    public ClimateSpawnFinder.Result SpawnResult { get; private set; }

    public ClimateSpawnFinder(List<ClimateParameterPoint> p_207872_, ClimateSampler sampler)
    {
        SpawnResult = getSpawnPositionAndFitness(p_207872_, sampler, 0, 0);
        radialSearch(p_207872_, sampler, 2048.0F, 512.0F);
        radialSearch(p_207872_, sampler, 512.0F, 32.0F);
    }

    private void radialSearch(List<ClimateParameterPoint> p_207875_, ClimateSampler sampler, float p_207877_, float p_207878_)
    {
        float f = 0.0F;
        float f1 = p_207878_;
        BlockPosition blockpos = SpawnResult.location;

        while (f1 <= p_207877_)
        {
            int i = blockpos.X + (int)(Math.Sin(f) * f1);
            int j = blockpos.Z + (int)(Math.Cos(f) * f1);
            ClimateSpawnFinder.Result spawnResult = getSpawnPositionAndFitness(p_207875_, sampler, i, j);
            if (spawnResult.fitness < SpawnResult.fitness)
            {
                SpawnResult = spawnResult;
            }

            f += p_207878_ / f1;
            if (f > Math.PI * 2)
            {
                f = 0.0F;
                f1 += p_207878_;
            }
        }
    }

    private static ClimateSpawnFinder.Result getSpawnPositionAndFitness(List<ClimateParameterPoint> p_207880_, ClimateSampler sampler, int p_207882_, int p_207883_)
    {
        ClimateTargetPoint targetPoint1 = sampler.Sample(QuartPosition.FromBlock(p_207882_), 0, QuartPosition.FromBlock(p_207883_));
        ClimateTargetPoint targetPoint2 = new ClimateTargetPoint(
            targetPoint1.Temperature,
            targetPoint1.Humidity,
            targetPoint1.Continentalness,
            targetPoint1.Erosion,
            0L,
            targetPoint1.Weirdness
        );
        long i = long.MaxValue;

        foreach (ClimateParameterPoint parameterPoint in p_207880_)
        {
            i = Math.Min(i, parameterPoint.Fitness(targetPoint2));
        }

        long k = Mth.square(p_207882_) + Mth.square(p_207883_);
        long j = i * Mth.square(MAX_RADIUS) + k;
        return new ClimateSpawnFinder.Result(new BlockPosition(p_207882_, 0, p_207883_), j);
    }

    public record Result(BlockPosition location, long fitness)
    {
    }
}
