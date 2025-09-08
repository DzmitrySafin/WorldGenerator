using Generator.Helpers;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.LegacyRandomSource.LegacyPositionalRandomFactory
public class LegacyPositionalRandomFactory : IPositionalRandomFactory
{
    private readonly long seedValue;

    public LegacyPositionalRandomFactory(long seed)
    {
        seedValue = seed;
    }

    public IRandomSource At(int blockX, int blockY, int blockZ)
    {
        long i = Mth.getSeed(blockX, blockY, blockZ);
        long j = i ^ seedValue;
        return new LegacyRandomSource(j);
    }

    public IRandomSource FromHashOf(string seedString)
    {
        int i = StringHelper.JavaHashCode(seedString);
        return new LegacyRandomSource(i ^ seedValue);
    }

    public IRandomSource FromSeed(long seed)
    {
        return new LegacyRandomSource(seed);
    }
}
