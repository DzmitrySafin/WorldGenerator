using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Dimension;

//source: net.minecraft.world.level.dimension.DimensionType.MonsterSettings
//source: data/minecraft/dimension_type/overworld.json
public class MonsterSettings
{
    [JsonProperty("piglin_safe")]
    public bool PiglinSafe { get; set; }

    [JsonProperty("has_raids")]
    public bool HasRaids { get; set; }

    //[JsonProperty("monster_spawn_light_level")]
    //public IntProvider MonsterSpawnLightLevel { get; set; }

    [JsonProperty("monster_spawn_block_light_limit")]
    public int MonsterSpawnBlockLightLimit { get; set; }
}
