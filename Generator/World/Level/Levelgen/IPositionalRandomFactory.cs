using Generator.Core;
using Generator.Resources;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.PositionalRandomFactory
public interface IPositionalRandomFactory
{
    IRandomSource At(BlockPosition blockPos)
    {
        return At(blockPos.X, blockPos.Y, blockPos.Z);
    }

    IRandomSource FromHashOf(ResourceLocation p_224541_)
    {
        return FromHashOf(p_224541_.ToString());
    }

    IRandomSource FromHashOf(string seedString);

    IRandomSource FromSeed(long seed);

    IRandomSource At(int blockX, int blockY, int blockZ);
}
