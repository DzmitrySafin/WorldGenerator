using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure.Structures;

//source: net.minecraft.world.level.levelgen.structure.structures.OceanMonumentStructure
public class OceanMonumentStructure : Structure
{
    public override StructureType StructureType => StructureType.OCEAN_MONUMENT;

    public OceanMonumentStructure()
        : base(new StructureSettings())
    {
    }

    public OceanMonumentStructure(StructureSettings settings)
        : base(settings)
    {
    }

    //public Optional<Structure.GenerationStub> findGenerationPoint(Structure.GenerationContext p_228964_)
    //{
    //    int i = p_228964_.chunkPos().getBlockX(9);
    //    int j = p_228964_.chunkPos().getBlockZ(9);

    //    for (Holder<Biome.Biome> holder : p_228964_.biomeSource().getBiomesWithin(i, p_228964_.chunkGenerator().getSeaLevel(), j, 29, p_228964_.randomState().sampler()))
    //    {
    //        if (!holder.is (BiomeTags.REQUIRED_OCEAN_MONUMENT_SURROUNDING))
    //        {
    //            return Optional.empty();
    //        }
    //    }

    //    return onTopOfChunkCenter(p_228964_, Heightmap.Types.OCEAN_FLOOR_WG, p_228967_->generatePieces(p_228967_, p_228964_));
    //}

    //private static StructurePiece createTopPiece(ChunkPosition p_228961_, WorldgenRandom p_228962_)
    //{
    //    int i = p_228961_.GetMinBlockX() - 29;
    //    int j = p_228961_.GetMinBlockZ() - 29;
    //    Direction direction = Direction.Plane.HORIZONTAL.getRandomDirection(p_228962_);
    //    return new OceanMonumentPieces.MonumentBuilding(p_228962_, i, j, direction);
    //}

    //private static void generatePieces(StructurePiecesBuilder p_228969_, Structure.GenerationContext p_228970_)
    //{
    //    p_228969_.addPiece(createTopPiece(p_228970_.chunkPos(), p_228970_.random()));
    //}

    //public static PiecesContainer regeneratePiecesAfterLoad(ChunkPosition p_228957_, long p_228958_, PiecesContainer p_228959_)
    //{
    //    if (p_228959_.isEmpty())
    //    {
    //        return p_228959_;
    //    }
    //    else
    //    {
    //        WorldgenRandom worldgenrandom = new WorldgenRandom(new LegacyRandomSource(RandomSupport.GenerateUniqueSeed()));
    //        worldgenrandom.setLargeFeatureSeed(p_228958_, p_228957_.X, p_228957_.Z);
    //        StructurePiece structurepiece = p_228959_.pieces().get(0);
    //        BoundingBox boundingbox = structurepiece.getBoundingBox();
    //        int i = boundingbox.MinX;
    //        int j = boundingbox.MinZ;
    //        Direction direction = Direction.Plane.HORIZONTAL.getRandomDirection(worldgenrandom);
    //        Direction direction1 = Objects.requireNonNullElse(structurepiece.getOrientation(), direction);
    //        StructurePiece structurepiece1 = new OceanMonumentPieces.MonumentBuilding(worldgenrandom, i, j, direction1);
    //        StructurePiecesBuilder structurepiecesbuilder = new StructurePiecesBuilder();
    //        structurepiecesbuilder.addPiece(structurepiece1);
    //        return structurepiecesbuilder.build();
    //    }
    //}
}
