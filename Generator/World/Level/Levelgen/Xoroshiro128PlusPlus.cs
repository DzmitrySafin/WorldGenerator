using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.Xoroshiro128PlusPlus
public class Xoroshiro128PlusPlus
{
    private long seedLoPart;
    private long seedHiPart;

    public Xoroshiro128PlusPlus(Seed128bit seed128)
        : this(seed128.SeedLo, seed128.SeedHi)
    {
        //
    }

    public Xoroshiro128PlusPlus(long seedLo, long seedHi)
    {
        seedLoPart = seedLo;
        seedHiPart = seedHi;
        if ((seedLoPart | seedHiPart) == 0L)
        {
            seedLoPart = -7046029254386353131L;
            seedHiPart = 7640891576956012809L;
        }
    }

    public long NextLong()
    {
        long i = seedLoPart;
        long j = seedHiPart;
        long k = long.RotateLeft(i + j, 17) + i;
        j ^= i;
        seedLoPart = long.RotateLeft(i, 49) ^ j ^ j << 21;
        seedHiPart = long.RotateLeft(j, 28);
        return k;
    }
}
