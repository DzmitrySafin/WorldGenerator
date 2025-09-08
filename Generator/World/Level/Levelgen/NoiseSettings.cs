using Generator.Core;
using Generator.World.Level.Dimension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.NoiseSettings
public class NoiseSettings
{
    [JsonProperty("min_y")]
    public int MinY { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("size_horizontal")]
    public int SizeHorizontal { get; set; }

    [JsonProperty("size_vertical")]
    public int SizeVertical { get; set; }

    public NoiseSettings()
    {
        // default constructor for JSON deserialization
    }

    public NoiseSettings(int minY, int height, int noiseSizeHorizontal, int noiseSizeVertical)
    {
        MinY = minY;
        Height = height;
        SizeHorizontal = noiseSizeHorizontal;
        SizeVertical = noiseSizeVertical;
    }

    public static NoiseSettings Create(int minY, int height, int noiseSizeHorizontal, int noiseSizeVertical)
    {
        NoiseSettings noisesettings = new NoiseSettings(minY, height, noiseSizeHorizontal, noiseSizeVertical);
        guardY(noisesettings);
        return noisesettings;
    }

    protected static readonly NoiseSettings OVERWORLD_NOISE_SETTINGS = Create(-64, 384, 1, 2);
    protected static readonly NoiseSettings NETHER_NOISE_SETTINGS = Create(0, 128, 1, 2);
    protected static readonly NoiseSettings END_NOISE_SETTINGS = Create(0, 128, 2, 1);
    protected static readonly NoiseSettings CAVES_NOISE_SETTINGS = Create(-64, 192, 1, 2);
    protected static readonly NoiseSettings FLOATING_ISLANDS_NOISE_SETTINGS = Create(0, 256, 2, 1);

    private static void guardY(NoiseSettings settings)
    {
        if (settings.MinY + settings.Height > DimensionType.MAX_Y + 1)
        {
            throw new ArgumentException("min_y + height cannot be higher than: " + (DimensionType.MAX_Y + 1));
        }
        else if (settings.Height % 16 != 0)
        {
            throw new ArgumentException("height has to be a multiple of 16");
        }
        else if (settings.MinY % 16 != 0)
        {
            throw new ArgumentException("min_y has to be a multiple of 16");
        }
    }

    public int GetCellHeight()
    {
        return QuartPosition.ToBlock(SizeVertical);
    }

    public int GetCellWidth()
    {
        return QuartPosition.ToBlock(SizeHorizontal);
    }

    public NoiseSettings ClampToHeightAccessor(ILevelHeightAccessor heightAccessor)
    {
        int i = Math.Max(MinY, heightAccessor.GetMinY());
        int j = Math.Min(MinY + Height, heightAccessor.GetMaxY() + 1) - i;
        return new NoiseSettings(i, j, SizeHorizontal, SizeVertical);
    }
}
