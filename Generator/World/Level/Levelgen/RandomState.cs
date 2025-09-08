using Generator.Enums;
using Generator.Helpers;
using Generator.Resources;
using Generator.World.Level.Biome;
using Generator.World.Level.Levelgen.Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.RandomState
public class RandomState
{
    public IPositionalRandomFactory RandomFactory { get; }
    private readonly Dictionary<NoiseType, NoiseParameters> noisesMap;
    public NoiseRouter Router { get; private set; }
    public ClimateSampler Sampler { get; private set; }
    //public SurfaceSystem SurfaceSystem { get; private set; }
    public IPositionalRandomFactory AquiferRandom { get; private set; }
    public IPositionalRandomFactory OreRandom { get; private set; }
    private readonly Dictionary<NoiseParameters, NormalNoise> noiseInstances;
    private readonly Dictionary<ResourceLocation, IPositionalRandomFactory> positionalRandoms;

    //public static RandomState Create(HolderGetter.Provider p_255935_, ResourceKey<NoiseGeneratorSettings> p_256314_, long seed)
    //{
    //    return Create(p_255935_.lookupOrThrow(Registries.NOISE_SETTINGS).getOrThrow(p_256314_).value(), p_255935_.lookupOrThrow(Registries.NOISE), seed);
    //}

    public static RandomState Create(NoiseGeneratorSettings generatorSettings, Dictionary<NoiseType, NoiseParameters> noises, long seed)
    {
        return new RandomState(generatorSettings, noises, seed);
    }

    private RandomState(NoiseGeneratorSettings generatorSettings, Dictionary<NoiseType, NoiseParameters> noises, long seed)
    {
        RandomFactory = generatorSettings.GetRandomSource(seed).ForkPositional();
        noisesMap = noises;
        AquiferRandom = RandomFactory.FromHashOf(ResourceLocation.WithDefaultNamespace("aquifer")).ForkPositional();
        OreRandom = RandomFactory.FromHashOf(ResourceLocation.WithDefaultNamespace("ore")).ForkPositional();
        noiseInstances = new Dictionary<NoiseParameters, NormalNoise>();
        positionalRandoms = new Dictionary<ResourceLocation, IPositionalRandomFactory>();
        //SurfaceSystem = new SurfaceSystem(this, p_255668_.defaultBlock(), p_255668_.SeaLevel, random);

        var noiseHelper = new NoiseWiringHelper(this, seed, generatorSettings.UseLegacyRandomSource);
        Router = generatorSettings.NoiseRouter.MapAll(noiseHelper);

        IDensityVisitor densityVisitor = new VisitorWiringHelper();
        Sampler = new ClimateSampler(
            Router.Temperature.MapAll(densityVisitor),
            Router.Vegetation.MapAll(densityVisitor),
            Router.Continents.MapAll(densityVisitor),
            Router.Erosion.MapAll(densityVisitor),
            Router.Depth.MapAll(densityVisitor),
            Router.Ridges.MapAll(densityVisitor),
            generatorSettings.SpawnTargetPoints
        );
    }

    public NormalNoise GetOrCreateNoise(NoiseParameters noiseParameters)
    {
        if (noiseInstances.ContainsKey(noiseParameters))
        {
            return noiseInstances[noiseParameters];
        }

        //IRandomSource noiseRandom = RandomFactory.FromHashOf($"{JTokenHelper.Namespace}:{noiseParameters.NoiseType.GetEnumMemberValue()}");
        //var noise = Noises.instantiate(noisesMap, RandomFactory, noiseParameters);
        NoiseParameters holder = noisesMap[noiseParameters.NoiseType];
        var noise = NormalNoise.Create(RandomFactory.FromHashOf(ResourceLocation.WithDefaultNamespace(holder.NoiseType.GetEnumMemberValue())), holder);

        noiseInstances.Add(noiseParameters, noise);
        return noise;
    }

    public IPositionalRandomFactory GetOrCreateRandomFactory(ResourceLocation location)
    {
        if (positionalRandoms.ContainsKey(location))
        {
            return positionalRandoms[location];
        }

        var factory = RandomFactory.FromHashOf(location).ForkPositional();
        positionalRandoms.Add(location, factory);
        return factory;
    }
}
