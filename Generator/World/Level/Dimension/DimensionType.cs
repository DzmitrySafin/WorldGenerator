using Generator.Core;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Dimension;

//source: net.minecraft.world.level.dimension.DimensionType
//source: data/minecraft/dimension_type/overworld.json
public class DimensionType
{
    public static readonly int BITS_FOR_Y = BlockPosition.PACKED_Y_LENGTH;
    public static readonly int MIN_HEIGHT = 16;
    public static readonly int Y_SIZE = (1 << BITS_FOR_Y) - 32;
    public static readonly int MAX_Y = (Y_SIZE >> 1) - 1;
    public static readonly int MIN_Y = MAX_Y - Y_SIZE + 1;
    public static readonly int WAY_ABOVE_MAX_Y = MAX_Y << 4;
    public static readonly int WAY_BELOW_MIN_Y = MIN_Y << 4;
    public static readonly int MOON_PHASES = 8;
    public static readonly float[] MOON_BRIGHTNESS_PER_PHASE = [1.0F, 0.75F, 0.5F, 0.25F, 0.0F, 0.25F, 0.5F, 0.75F];

    [JsonProperty("fixed_time")]
    public long? FixedTime { get; set; }

    [JsonProperty("has_skylight")]
    public bool HasSkylight { get; set; }

    [JsonProperty("has_ceiling")]
    public bool HasCeiling { get; set; }

    [JsonProperty("ultrawarm")]
    public bool UltraWarm { get; set; }

    [JsonProperty("natural")]
    public bool Natural { get; set; }

    [JsonProperty("coordinate_scale")]
    public double CoordinateScale { get; set; }

    [JsonProperty("bed_works")]
    public bool BedWorks { get; set; }

    [JsonProperty("respawn_anchor_works")]
    public bool RespawnAnchorWorks { get; set; }

    [JsonProperty("min_y")]
    public int MinY { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("logical_height")]
    public int LogicalHeight { get; set; }

    //[JsonProperty("infiniburn")]
    //TagKey<Block> Infiniburn { get; set; }

    //[JsonProperty("effects")]
    //ResourceLocation EffectsLocation { get; set; }

    [JsonProperty("ambient_light")]
    public float AmbientLight { get; set; }

    [JsonProperty("cloud_height")]
    public int CloudHeight { get; set; }

    public required MonsterSettings MonsterSettings { get; set; }

    public static double GetTeleportationScale(DimensionType p_63909_, DimensionType p_63910_)
    {
        double d0 = p_63909_.CoordinateScale;
        double d1 = p_63910_.CoordinateScale;
        return d0 / d1;
    }

    //public static Path GetStorageFolder(ResourceKey<Level> p_196976_, Path p_196977_)
    //{
    //    if (p_196976_ == Level.OVERWORLD)
    //    {
    //        return p_196977_;
    //    }
    //    else if (p_196976_ == Level.END)
    //    {
    //        return p_196977_.resolve("DIM1");
    //    }
    //    else
    //    {
    //        return p_196976_ == Level.NETHER
    //            ? p_196977_.resolve("DIM-1")
    //            : p_196977_.resolve("dimensions").resolve(p_196976_.location().getNamespace()).resolve(p_196976_.location().getPath());
    //    }
    //}

    public bool HasFixedTime()
    {
        return FixedTime.HasValue;
    }

    public float TimeOfDay(long p_63905_)
    {
        double d0 = Mth.frac(FixedTime ?? p_63905_ / 24000.0 - 0.25);
        double d1 = 0.5 - Math.Cos(d0 * Math.PI) / 2.0;
        return (float)(d0 * 2.0 + d1) / 3.0F;
    }

    public int MoonPhase(long p_63937_)
    {
        return (int)(p_63937_ / 24000L % 8L + 8L) % 8;
    }
}
