using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Biome
public class Biome
{
    //private static readonly PerlinSimplexNoise TEMPERATURE_NOISE = new PerlinSimplexNoise(new WorldgenRandom(new LegacyRandomSource(1234L)), ImmutableList.of(0));
    //static readonly PerlinSimplexNoise FROZEN_TEMPERATURE_NOISE = new PerlinSimplexNoise(new WorldgenRandom(new LegacyRandomSource(3456L)), ImmutableList.of(-2, -1, 0));
    //[Obsolete]
    //public static readonly PerlinSimplexNoise BIOME_INFO_NOISE = new PerlinSimplexNoise(new WorldgenRandom(new LegacyRandomSource(2345L)), ImmutableList.of(0));
    private static readonly int TEMPERATURE_CACHE_SIZE = 1024;
    //private readonly Biome.ClimateSettings climateSettings;
    //private readonly BiomeGenerationSettings generationSettings;
    //private readonly MobSpawnSettings mobSettings;
    //private readonly BiomeSpecialEffects specialEffects;
    private readonly Dictionary<long, float> temperatureCache = new Dictionary<long, float>(); // { { 1024, 0.25F } };
    //private readonly ThreadLocal<Long2FloatLinkedOpenHashMap> temperatureCache = ThreadLocal.withInitial(()->Util.make(()-> {
    //    Long2FloatLinkedOpenHashMap long2floatlinkedopenhashmap = new Long2FloatLinkedOpenHashMap(1024, 0.25F) {
    //        @Override protected void rehash(int p_47580_) { }
    //    };
    //    long2floatlinkedopenhashmap.defaultReturnValue(Float.NaN);
    //    return long2floatlinkedopenhashmap;
    //}));

    //Biome(Biome.ClimateSettings p_220530_, BiomeSpecialEffects p_220531_, BiomeGenerationSettings p_220532_, MobSpawnSettings p_220533_)
    //{
    //    this.climateSettings = p_220530_;
    //    this.generationSettings = p_220532_;
    //    this.mobSettings = p_220533_;
    //    this.specialEffects = p_220531_;
    //}

    //public int getSkyColor()
    //{
    //    return this.specialEffects.getSkyColor();
    //}

    //public MobSpawnSettings getMobSettings()
    //{
    //    return this.mobSettings;
    //}

    //public bool hasPrecipitation()
    //{
    //    return this.climateSettings.hasPrecipitation();
    //}
}
