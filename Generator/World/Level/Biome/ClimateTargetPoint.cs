using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate.TargetPoint
public class ClimateTargetPoint
{
    public long Temperature { get; private set; }
    public long Humidity { get; private set; }
    public long Continentalness { get; private set; }
    public long Erosion { get; private set; }
    public long Depth { get; private set; }
    public long Weirdness { get; private set; }

    public ClimateTargetPoint(long temperature, long humidity, long continentalness, long erosion, long depth, long weirdness)
    {
        Temperature = temperature;
        Humidity = humidity;
        Continentalness = continentalness;
        Erosion = erosion;
        Depth = depth;
        Weirdness = weirdness;
    }

    protected long[] toParameterArray()
    {
        return [Temperature, Humidity, Continentalness, Erosion, Depth, Weirdness, 0L];
    }
}
