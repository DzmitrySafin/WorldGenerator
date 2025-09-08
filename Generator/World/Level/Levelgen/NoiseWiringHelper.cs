using Generator.Enums;
using Generator.Helpers;
using Generator.Resources;
using Generator.Util;
using Generator.World.Level.Levelgen.Density;
using Generator.World.Level.Levelgen.Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.RandomState -> RandomState -> NoiseWiringHelper
public class NoiseWiringHelper : IDensityVisitor
{
    private readonly RandomState randomState;
    private readonly long seed;
    private readonly bool useLegacyRandomSource;

    private readonly Dictionary<IDensityFunction, IDensityFunction> wrapped = new Dictionary<IDensityFunction, IDensityFunction>();

    public NoiseWiringHelper(RandomState random, long seed, bool useLegacyRandomSource)
    {
        randomState = random;
        this.seed = seed;
        this.useLegacyRandomSource = useLegacyRandomSource;
    }

    private IRandomSource newLegacyInstance(long p_224592_)
    {
        return new LegacyRandomSource(seed + p_224592_);
    }

    public NoiseHolder VisitNoise(NoiseHolder noiseHolder)
    {
        NoiseParameters holder = noiseHolder.NoiseData;
        if (useLegacyRandomSource)
        {
            if (holder.NoiseType == NoiseType.TEMPERATURE)
            {
                NormalNoise normalNoise3 = NormalNoise.createLegacyNetherBiome(newLegacyInstance(0L), new NoiseParameters(-7, 1.0, 1.0));
                return new NoiseHolder(holder, normalNoise3);
            }

            if (holder.NoiseType == NoiseType.VEGETATION)
            {
                NormalNoise normalNoise2 = NormalNoise.createLegacyNetherBiome(newLegacyInstance(1L), new NoiseParameters(-7, 1.0, 1.0));
                return new NoiseHolder(holder, normalNoise2);
            }

            if (holder.NoiseType == NoiseType.SHIFT)
            {
                NormalNoise normalNoise1 = NormalNoise.Create(randomState.RandomFactory.FromHashOf(ResourceLocation.WithDefaultNamespace(NoiseType.SHIFT.GetEnumMemberValue())), new NoiseParameters(0, 0.0));
                return new NoiseHolder(holder, normalNoise1);
            }
        }

        NormalNoise normalNoise = randomState.GetOrCreateNoise(holder);
        return new NoiseHolder(holder, normalNoise);
    }

    private IDensityFunction wrapNew(IDensityFunction densityFunction)
    {
        if (densityFunction is BlendedNoise blendednoise)
        {
            IRandomSource randomsource = useLegacyRandomSource ? newLegacyInstance(0L) : randomState.RandomFactory.FromHashOf(ResourceLocation.WithDefaultNamespace("terrain"));
            return blendednoise.WithNewRandom(randomsource);
        }
        else
        {
            return densityFunction is EndIslandDensityFunction
                ? new EndIslandDensityFunction(seed)
                : densityFunction;
        }
    }

    public IDensityFunction Apply(IDensityFunction densityFunction)
    {
        if (wrapped.ContainsKey(densityFunction))
        {
            return wrapped[densityFunction];
        }

        var density = wrapNew(densityFunction);
        wrapped.Add(densityFunction, density);
        return density;
    }
}
