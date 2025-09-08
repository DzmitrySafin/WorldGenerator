using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Blending;

//source: net.minecraft.world.level.levelgen.blending.BlendingData.Packed
public class BlendingDataPacked
{
    [JsonProperty("min_section")]
    public int MinSection { get; set; }

    [JsonProperty("max_section")]
    public int MaxSection { get; set; }

    [JsonProperty("heights")]
    public double[]? Heights { get; set; }
}
