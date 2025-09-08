using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.NoiseRouter
public class NoiseRouter
{
    [JsonProperty("barrier")]
    public required IDensityFunction BarrierNoise { get; set; }

    [JsonProperty("fluid_level_floodedness")]
    public required IDensityFunction FluidLevelFloodednessNoise { get; set; }

    [JsonProperty("fluid_level_spread")]
    public required IDensityFunction FluidLevelSpreadNoise { get; set; }

    [JsonProperty("lava")]
    public required IDensityFunction LavaNoise { get; set; }

    [JsonProperty("temperature")]
    public required IDensityFunction Temperature { get; set; }

    [JsonProperty("vegetation")]
    public required IDensityFunction Vegetation { get; set; }

    [JsonProperty("continents")]
    public required IDensityFunction Continents { get; set; }

    [JsonProperty("erosion")]
    public required IDensityFunction Erosion { get; set; }

    [JsonProperty("depth")]
    public required IDensityFunction Depth { get; set; }

    [JsonProperty("ridges")]
    public required IDensityFunction Ridges { get; set; }

    [JsonProperty("initial_density_without_jaggedness")]
    public required IDensityFunction InitialDensityWithoutJaggedness { get; set; }

    [JsonProperty("final_density")]
    public required IDensityFunction FinalDensity { get; set; }

    [JsonProperty("vein_toggle")]
    public required IDensityFunction VeinToggle { get; set; }

    [JsonProperty("vein_ridged")]
    public required IDensityFunction VeinRidged { get; set; }

    [JsonProperty("vein_gap")]
    public required IDensityFunction VeinGap { get; set; }

    public NoiseRouter MapAll(IDensityVisitor densityVisitor)
    {
        return new NoiseRouter
        {
            BarrierNoise = BarrierNoise.MapAll(densityVisitor),
            FluidLevelFloodednessNoise = FluidLevelFloodednessNoise.MapAll(densityVisitor),
            FluidLevelSpreadNoise = FluidLevelSpreadNoise.MapAll(densityVisitor),
            LavaNoise = LavaNoise.MapAll(densityVisitor),
            Temperature = Temperature.MapAll(densityVisitor),
            Vegetation = Vegetation.MapAll(densityVisitor),
            Continents = Continents.MapAll(densityVisitor),
            Erosion = Erosion.MapAll(densityVisitor),
            Depth = Depth.MapAll(densityVisitor),
            Ridges = Ridges.MapAll(densityVisitor),
            InitialDensityWithoutJaggedness = InitialDensityWithoutJaggedness.MapAll(densityVisitor),
            FinalDensity = FinalDensity.MapAll(densityVisitor),
            VeinToggle = VeinToggle.MapAll(densityVisitor),
            VeinRidged = VeinRidged.MapAll(densityVisitor),
            VeinGap = VeinGap.MapAll(densityVisitor)
        };
    }
}
