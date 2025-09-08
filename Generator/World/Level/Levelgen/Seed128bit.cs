using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.RandomSupport.Seed128bit
public class Seed128bit
{
    public long SeedLo { get; private set; }
    public long SeedHi { get; private set; }

    public Seed128bit(long seedLo, long seedHi)
    {
        SeedLo = seedLo;
        SeedHi = seedHi;
    }

    public Seed128bit Xor(long seedLo, long seedHi)
    {
        return new Seed128bit(SeedLo ^ seedLo, SeedHi ^ seedHi);
    }

    public Seed128bit Xor(Seed128bit seed128)
    {
        return Xor(seed128.SeedLo, seed128.SeedHi);
    }

    public Seed128bit Mixed()
    {
        return new Seed128bit(RandomSupport.MixStafford13(SeedLo), RandomSupport.MixStafford13(SeedHi));
    }
}
