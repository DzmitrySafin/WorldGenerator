using Generator.Core;
using Generator.Enums;
using Generator.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.JigsawStructure
public class JigsawStructure : Structure
{
    //public static readonly DimensionPadding DEFAULT_DIMENSION_PADDING = DimensionPadding.ZERO;
    //public static readonly LiquidSettings DEFAULT_LIQUID_SETTINGS = LiquidSettings.APPLY_WATERLOGGING;
    public static readonly int MAX_TOTAL_STRUCTURE_RANGE = 128;
    public static readonly int MIN_DEPTH = 0;
    public static readonly int MAX_DEPTH = 20;

    //[JsonProperty("start_pool")]
    //public Holder<StructureTemplatePool> StartPool { get; set; }

    [JsonProperty("start_jigsaw_name")]
    public ResourceLocation? StartJigsawName { get; set; }

    [JsonProperty("size")]
    public int MaxDepth { get; set; }

    //[JsonProperty("start_height")]
    //public HeightProvider StartHeight { get; set; }

    [JsonProperty("use_expansion_hack")]
    public bool UseExpansionHack { get; set; }

    //[JsonProperty("project_start_to_heightmap")]
    //public Optional<Heightmap.Types> ProjectStartToHeightmap { get; set; }

    [JsonProperty("max_distance_from_center")]
    public int MaxDistanceFromCenter { get; set; }

    //[JsonProperty("pool_aliases")]
    //public List<PoolAliasBinding> PoolAliases { get; set; }

    //[JsonProperty("dimension_padding")]
    //public DimensionPadding DimensionPadding { get; set; }

    //[JsonProperty("liquid_settings")]
    //public LiquidSettings LiquidSettings { get; set; }

    public override StructureType StructureType => StructureType.JIGSAW;

    public JigsawStructure()
        : base(new StructureSettings())
    {
        // default constructor for JSON deserialization
    }

    //private static DataResult<JigsawStructure> verifyRange(JigsawStructure p_286886_)
    //{
    //    int i = p_286886_.TerrainAdaptation switch
    //    {
    //        TerrainAdjustmentType.NONE => 0,
    //        TerrainAdjustmentType.BURY => 12,
    //        TerrainAdjustmentType.BEARD_THIN => 12,
    //        TerrainAdjustmentType.BEARD_BOX => 12,
    //        TerrainAdjustmentType.ENCAPSULATE => 12
    //    };

    //    return p_286886_.MaxDistanceFromCenter + i > 128
    //        ? DataResult.error(()-> "Structure size including terrain adaptation must not exceed 128")
    //        : DataResult.success(p_286886_);
    //}

    public JigsawStructure(
        StructureSettings p_227627_,
        //Holder<StructureTemplatePool> p_227628_,
        ResourceLocation? p_227629_,
        int p_227630_,
        //HeightProvider p_227631_,
        bool p_227632_,
        //Optional<Heightmap.Types> p_227633_,
        int p_227634_
        //List<PoolAliasBinding> p_312703_,
        //DimensionPadding p_344382_,
        //LiquidSettings p_344801_
    )
        : base(p_227627_)
    {
        //this.startPool = p_227628_;
        StartJigsawName = p_227629_;
        MaxDepth = p_227630_;
        //this.startHeight = p_227631_;
        UseExpansionHack = p_227632_;
        //this.projectStartToHeightmap = p_227633_;
        MaxDistanceFromCenter = p_227634_;
        //this.poolAliases = p_312703_;
        //this.dimensionPadding = p_344382_;
        //this.liquidSettings = p_344801_;
    }

    public JigsawStructure(
        StructureSettings p_227620_,
        //Holder<StructureTemplatePool> p_227621_,
        int p_227622_,
        //HeightProvider p_227623_,
        bool p_227624_
        //Heightmap.Types p_227625_
    )
        : this(
              p_227620_,
              //p_227621_,
              null,
              p_227622_,
              //p_227623_,
              p_227624_,
              //Optional.of(p_227625_),
              80
              //List.of(),
              //DEFAULT_DIMENSION_PADDING,
              //DEFAULT_LIQUID_SETTINGS
          )
    {
    }

    //public JigsawStructure(
    //    StructureSettings p_227614_,
    //    //Holder<StructureTemplatePool> p_227615_,
    //    int p_227616_,
    //    //HeightProvider p_227617_,
    //    bool p_227618_)
    //    : this(
    //          p_227614_,
    //          //p_227615_,
    //          null,
    //          p_227616_,
    //          //p_227617_,
    //          p_227618_,
    //          //Optional.empty(),
    //          80
    //          //List.of(),
    //          //DEFAULT_DIMENSION_PADDING,
    //          //DEFAULT_LIQUID_SETTINGS
    //      )
    //{
    //}

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_227636_)
    //{
    //    ChunkPosition chunkpos = p_227636_.chunkPos();
    //    int i = this.startHeight.sample(p_227636_.random(), new WorldGenerationContext(p_227636_.chunkGenerator(), p_227636_.heightAccessor()));
    //    BlockPosition blockpos = new BlockPosition(chunkpos.GetMinBlockX(), i, chunkpos.GetMinBlockZ());
    //    return JigsawPlacement.addPieces(
    //        p_227636_,
    //        this.startPool,
    //        this.startJigsawName,
    //        this.maxDepth,
    //        blockpos,
    //        this.useExpansionHack,
    //        this.projectStartToHeightmap,
    //        this.maxDistanceFromCenter,
    //        PoolAliasLookup.create(this.poolAliases, blockpos, p_227636_.seed()),
    //        this.dimensionPadding,
    //        this.liquidSettings
    //    );
    //}
}
