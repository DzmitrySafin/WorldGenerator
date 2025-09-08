using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate.ParameterPoint
public class ClimateParameterPoint
{
    [JsonProperty("temperature")]
    public ClimateParameter Temperature { get; private set; }

    [JsonProperty("humidity")]
    public ClimateParameter Humidity { get; private set; }

    [JsonProperty("continentalness")]
    public ClimateParameter Continentalness { get; private set; }

    [JsonProperty("erosion")]
    public ClimateParameter Erosion { get; private set; }

    [JsonProperty("depth")]
    public ClimateParameter Depth { get; private set; }

    [JsonProperty("weirdness")]
    public ClimateParameter Weirdness { get; private set; }

    [JsonProperty("offset")]
    public long Offset { get; private set; }

    public ClimateParameterPoint(
        ClimateParameter temperature,
        ClimateParameter humidity,
        ClimateParameter continentalness,
        ClimateParameter erosion,
        ClimateParameter depth,
        ClimateParameter weirdness,
        long offset
    )
    {
        Temperature = temperature;
        Humidity = humidity;
        Continentalness = continentalness;
        Erosion = erosion;
        Depth = depth;
        Weirdness = weirdness;
        Offset = offset;
    }

    public long Fitness(ClimateTargetPoint p_186883_)
    {
        return Mth.square(Temperature.Distance(p_186883_.Temperature))
            + Mth.square(Humidity.Distance(p_186883_.Humidity))
            + Mth.square(Continentalness.Distance(p_186883_.Continentalness))
            + Mth.square(Erosion.Distance(p_186883_.Erosion))
            + Mth.square(Depth.Distance(p_186883_.Depth))
            + Mth.square(Weirdness.Distance(p_186883_.Weirdness))
            + Mth.square(Offset);
    }

    protected List<ClimateParameter> parameterSpace()
    {
        return ImmutableList.Create(
            Temperature,
            Humidity,
            Continentalness,
            Erosion,
            Depth,
            Weirdness,
            new ClimateParameter(Offset, Offset)
        ).ToList();
    }
}
