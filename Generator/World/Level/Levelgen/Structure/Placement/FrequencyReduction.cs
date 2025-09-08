using Generator.Enums;
using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Placement;

//source: net.minecraft.world.level.levelgen.structure.placement.StructurePlacement.FrequencyReductionMethod
[JsonConverter(typeof(FrequencyReductionConverter))]
public class FrequencyReduction
{
    public FrequencyReductionType ReductionType { get; set; }

    public Func<long, int, int, int, float, bool> FrequencyReducer => ReductionType == FrequencyReductionType.LEGACY_TYPE_1 ? legacyPillagerOutpostReducer
                                                                    : ReductionType == FrequencyReductionType.LEGACY_TYPE_2 ? legacyArbitrarySaltProbabilityReducer
                                                                    : ReductionType == FrequencyReductionType.LEGACY_TYPE_3 ? legacyProbabilityReducerWithDouble
                                                                    : probabilityReducer;

    private static bool probabilityReducer(long p_227034_, int p_227035_, int p_227036_, int p_227037_, float p_227038_)
    {
        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(0L));
        worldgenrandom.SetLargeFeatureWithSalt(p_227034_, p_227035_, p_227036_, p_227037_);
        return worldgenrandom.NextFloat() < p_227038_;
    }

    private static bool legacyProbabilityReducerWithDouble(long p_227049_, int p_227050_, int p_227051_, int p_227052_, float p_227053_)
    {
        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(0L));
        worldgenrandom.SetLargeFeatureSeed(p_227049_, p_227051_, p_227052_);
        return worldgenrandom.NextDouble() < p_227053_;
    }

    private static bool legacyArbitrarySaltProbabilityReducer(long p_227061_, int p_227062_, int p_227063_, int p_227064_, float p_227065_)
    {
        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(0L));
        worldgenrandom.SetLargeFeatureWithSalt(p_227061_, p_227063_, p_227064_, 10387320);
        return worldgenrandom.NextFloat() < p_227065_;
    }

    private static bool legacyPillagerOutpostReducer(long p_227067_, int p_227068_, int p_227069_, int p_227070_, float p_227071_)
    {
        int i = p_227069_ >> 4;
        int j = p_227070_ >> 4;
        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(0L));
        worldgenrandom.SetSeed(i ^ j << 4 ^ p_227067_);
        worldgenrandom.NextInt();
        return worldgenrandom.NextInt((int)(1.0F / p_227071_)) == 0;
    }

    public bool ShouldGenerate(long p_227120_, int p_227121_, int p_227122_, int p_227123_, float p_227124_)
    {
        return FrequencyReducer(p_227120_, p_227121_, p_227122_, p_227123_, p_227124_);
    }
}
