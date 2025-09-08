using Generator.Core;
using Generator.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.NetherFossilStructure
public class NetherFossilStructure : Structure
{
    public override StructureType StructureType => StructureType.NETHER_FOSSIL;

    //[JsonProperty("height")]
    //public HeightProvider Height { get; set; }

    public NetherFossilStructure()
        : base(new StructureSettings())
    {
    }

    public NetherFossilStructure(StructureSettings settings/*, HeightProvider p_228574_*/)
        : base(settings)
    {
        //Height = p_228574_;
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_228576_)
    //{
    //    WorldgenRandom worldgenrandom = p_228576_.random();
    //    int i = p_228576_.chunkPos().getMinBlockX() + worldgenrandom.NextInt(16);
    //    int j = p_228576_.chunkPos().getMinBlockZ() + worldgenrandom.NextInt(16);
    //    int k = p_228576_.chunkGenerator().getSeaLevel();
    //    WorldGenerationContext worldgenerationcontext = new WorldGenerationContext(p_228576_.chunkGenerator(), p_228576_.heightAccessor());
    //    int l = this.height.sample(worldgenrandom, worldgenerationcontext);
    //    NoiseColumn noisecolumn = p_228576_.chunkGenerator().getBaseColumn(i, j, p_228576_.heightAccessor(), p_228576_.randomState());
    //    BlockPosition.MutableBlockPos blockpos$mutableblockpos = new BlockPosition.MutableBlockPos(i, l, j);

    //    while (l > k)
    //    {
    //        BlockState blockstate = noisecolumn.getBlock(l);
    //        BlockState blockstate1 = noisecolumn.getBlock(--l);
    //        if (blockstate.isAir()
    //            && (
    //                blockstate1.is (Blocks.SOUL_SAND)
    //                    || blockstate1.isFaceSturdy(EmptyBlockGetter.INSTANCE, blockpos$mutableblockpos.setY(l), DirectionType.UP)
    //            ))
    //        {
    //            break;
    //        }
    //    }

    //    if (l <= k)
    //    {
    //        return Optional.empty();
    //    }
    //    else
    //    {
    //        BlockPosition blockpos = new BlockPosition(i, l, j);
    //        return Optional.of(
    //            new Structure.GenerationStub(blockpos, p_228581_->NetherFossilPieces.addPieces(p_228576_.structureTemplateManager(), p_228581_, worldgenrandom, blockpos))
    //        );
    //    }
    //}
}
