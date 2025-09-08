using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.BiomeResolver
public interface IBiomeResolver
{
    Biome GetNoiseBiome(int p_204221_, int p_204222_, int p_204223_, ClimateSampler p_204224_);
}
