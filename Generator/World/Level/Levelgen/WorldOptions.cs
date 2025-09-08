using Generator.Helpers;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.WorldOptions
public class WorldOptions
{
    public long Seed { get; private set; }
    public bool GenerateStructures { get; private set; }
    public bool GenerateBonusChest { get; private set; }
    public string? LegacyCustomOptions { get; private set; }

    public WorldOptions(long p_251567_, bool p_250743_, bool p_250454_)
        : this(p_251567_, p_250743_, p_250454_, null)
    {
    }

    public static WorldOptions defaultWithRandomSeed()
    {
        return new WorldOptions(randomSeed(), true, false);
    }

    public static WorldOptions testWorldWithRandomSeed()
    {
        return new WorldOptions(randomSeed(), false, false);
    }

    private WorldOptions(long p_249191_, bool p_250927_, bool p_249013_, string? p_250735_)
    {
        Seed = p_249191_;
        GenerateStructures = p_250927_;
        GenerateBonusChest = p_249013_;
        LegacyCustomOptions = p_250735_;
    }

    public WorldOptions withBonusChest(bool p_251744_)
    {
        return new WorldOptions(Seed, GenerateStructures, p_251744_,LegacyCustomOptions);
    }

    public WorldOptions withStructures(bool p_251426_)
    {
        return new WorldOptions(Seed, p_251426_, GenerateBonusChest, LegacyCustomOptions);
    }

    public WorldOptions withSeed(long? p_261572_)
    {
        return new WorldOptions(p_261572_ ?? randomSeed(), GenerateStructures, GenerateBonusChest, LegacyCustomOptions);
    }

    public static long? parseSeed(string p_262144_)
    {
        p_262144_ = p_262144_.Trim();
        if (string.IsNullOrEmpty(p_262144_))
        {
            return null;
        }
        else
        {
            try
            {
                return long.Parse(p_262144_);
            }
            catch (Exception)
            {
                return StringHelper.JavaHashCode(p_262144_);
            }
        }
    }

    public static long randomSeed()
    {
        return IRandomSource.Create().NextLong();
    }
}
