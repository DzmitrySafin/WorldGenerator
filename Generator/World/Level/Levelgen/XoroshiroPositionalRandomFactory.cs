using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.XoroshiroRandomSource.XoroshiroPositionalRandomFactory
public class XoroshiroPositionalRandomFactory : IPositionalRandomFactory
{
    private readonly long seedLoPart;
    private readonly long seedHiPart;

    public XoroshiroPositionalRandomFactory(long seedLo, long seedHi)
    {
        seedLoPart = seedLo;
        seedHiPart = seedHi;
    }

    public IRandomSource At(int blockX, int blockY, int blockZ)
    {
        long i = Mth.getSeed(blockX, blockY, blockZ);
        long j = i ^ seedLoPart;
        return new XoroshiroRandomSource(j, seedHiPart);
    }

    public IRandomSource FromHashOf(string seedString)
    {
        Seed128bit seed128 = RandomSupport.SeedFromHashOf(seedString);
        return new XoroshiroRandomSource(seed128.Xor(seedLoPart, seedHiPart));
    }

    public IRandomSource FromSeed(long seed)
    {
        return new XoroshiroRandomSource(seed ^ seedLoPart, seed ^ seedHiPart);
    }
}
