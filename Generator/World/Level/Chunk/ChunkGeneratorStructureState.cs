using Generator.Core;
using Generator.Util;
using Generator.World.Level.Biome;
using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Structure;
using Generator.World.Level.Levelgen.Structure.Placement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Chunk;

//source: net.minecraft.world.level.chunk.ChunkGeneratorStructureState
public class ChunkGeneratorStructureState
{
    public RandomState RandomState { get; private set; }
    private readonly BiomeSource biomeSource;
    public long LevelSeed { get; private set; }
    private readonly long concentricRingsSeed;
    private readonly Dictionary<Structure, List<StructurePlacement>> placementsForStructure = new Dictionary<Structure, List<StructurePlacement>>();
    private readonly Dictionary<ConcentricRingsStructurePlacement, List<ChunkPosition>> ringPositions = new Dictionary<ConcentricRingsStructurePlacement, List<ChunkPosition>>();
    private bool hasGeneratedPositions;
    public List<StructureSet> PossibleStructureSets { get; private set; }

    //public static ChunkGeneratorStructureState createForFlat(RandomState p_256240_, long p_256404_, BiomeSource p_256274_, Stream<Holder<StructureSet>> p_256348_)
    //{
    //    List<Holder<StructureSet>> list = p_256348_.filter(p_255616_->hasBiomesForStructureSet(p_255616_.value(), p_256274_)).toList();
    //    return new ChunkGeneratorStructureState(p_256240_, p_256274_, p_256404_, 0L, list);
    //}

    //public static ChunkGeneratorStructureState createForNormal(RandomState p_256197_, long p_255806_, BiomeSource p_256653_, HolderLookup<StructureSet> p_256659_)
    //{
    //    List<Holder<StructureSet>> list = p_256659_.listElements()
    //        .filter(p_256144_->hasBiomesForStructureSet(p_256144_.value(), p_256653_))
    //        .collect(Collectors.toUnmodifiableList());
    //    return new ChunkGeneratorStructureState(p_256197_, p_256653_, p_255806_, p_255806_, list);
    //}

    //private static bool hasBiomesForStructureSet(StructureSet p_255766_, BiomeSource p_256424_)
    //{
    //    Stream<Holder<Biome>> stream = p_255766_.structures().stream().flatMap(p_255738_-> {
    //        Structure structure = p_255738_.structure().value();
    //        return structure.biomes().stream();
    //    });
    //    return stream.anyMatch(p_256424_.possibleBiomes()::contains);
    //}

    private ChunkGeneratorStructureState(RandomState p_256401_, BiomeSource p_255742_, long p_256615_, long p_255979_, List<StructureSet> p_256237_)
    {
        RandomState = p_256401_;
        LevelSeed = p_256615_;
        this.biomeSource = p_255742_;
        this.concentricRingsSeed = p_255979_;
        PossibleStructureSets = p_256237_;
    }

    private void generatePositions()
    {
        HashSet<Biome.Biome> set = biomeSource.PossibleBiomes;

        foreach (StructureSet structureSet in PossibleStructureSets)
        {
            bool flag = false;
            foreach (StructureSelectionEntry selectionEntry in structureSet.Structures)
            {
                Structure structure = selectionEntry.Structure;
                if (structure.Biomes.Any(set.Contains))
                {
                    if (placementsForStructure.ContainsKey(structure))
                    {
                        placementsForStructure[structure].Add(structureSet.Placement);
                    }
                    else
                    {
                        placementsForStructure.Add(structure, new List<StructurePlacement>() { structureSet.Placement });
                    }
                    flag = true;
                }
            }

            if (flag && structureSet.Placement is ConcentricRingsStructurePlacement concentricRings)
            {
                var positions = generateRingPositions(structureSet, concentricRings);
                if (ringPositions.ContainsKey(concentricRings))
                {
                    ringPositions[concentricRings] = positions;
                }
                else
                {
                    ringPositions.Add(concentricRings, positions);
                }
            }
        }
    }

    private List<ChunkPosition> generateRingPositions(StructureSet p_255966_, ConcentricRingsStructurePlacement p_255744_)
    {
        if (p_255744_.Count == 0)
        {
            return new List<ChunkPosition>();
        }
        else
        {
            int i = p_255744_.Distance;
            int j = p_255744_.Count;
            List<ChunkPosition> list = new List<ChunkPosition>(j);
            int k = p_255744_.Spread;
            List<Biome.Biome> holderset = p_255744_.PreferredBiomes;
            IRandomSource randomsource = IRandomSource.Create();
            randomsource.SetSeed(this.concentricRingsSeed);
            double d0 = randomsource.NextDouble() * Math.PI * 2.0;
            int l = 0;
            int i1 = 0;

            for (int j1 = 0; j1 < j; j1++)
            {
                double d1 = 4 * i + i * i1 * 6 + (randomsource.NextDouble() - 0.5) * (i * 2.5);
                int k1 = (int)Math.Round(Math.Cos(d0) * d1);
                int l1 = (int)Math.Round(Math.Sin(d0) * d1);
                IRandomSource randomsource1 = randomsource.Fork();

                Tuple<BlockPosition, Biome.Biome>? pair = biomeSource.findBiomeHorizontal(
                    SectionPosition.SectionToBlockCoord(k1, 8),
                    0,
                    SectionPosition.SectionToBlockCoord(l1, 8),
                    112,
                    holderset.Contains,
                    randomsource1,
                    RandomState.Sampler
                );
                if (pair != null)
                {
                    BlockPosition blockpos = pair.Item1;
                    list.Add(new ChunkPosition(SectionPosition.BlockToSectionCoord(blockpos.X), SectionPosition.BlockToSectionCoord(blockpos.Z)));
                }
                else
                {
                    list.Add(new ChunkPosition(k1, l1));
                }

                d0 += (Math.PI * 2) / k;
                if (++l == k)
                {
                    i1++;
                    l = 0;
                    k += 2 * k / (i1 + 1);
                    k = Math.Min(k, j - j1);
                    d0 += randomsource.NextDouble() * Math.PI * 2.0;
                }
            }

            return list;
        }
    }

    public void ensureStructuresGenerated()
    {
        if (!hasGeneratedPositions)
        {
            generatePositions();
            hasGeneratedPositions = true;
        }
    }

    public List<ChunkPosition> GetRingPositionsFor(ConcentricRingsStructurePlacement p_256667_)
    {
        ensureStructuresGenerated();

        if (ringPositions.ContainsKey(p_256667_))
        {
            return ringPositions[p_256667_];
        }

        return new List<ChunkPosition>();
        //CompletableFuture<List<ChunkPosition>> completablefuture = this.ringPositions.get(p_256667_);
        //return completablefuture != null ? completablefuture.join() : null;
    }

    public List<StructurePlacement> GetPlacementsForStructure(Structure p_256494_)
    {
        ensureStructuresGenerated();

        if (placementsForStructure.ContainsKey(p_256494_))
        {
            return placementsForStructure[p_256494_];
        }

        return new List<StructurePlacement>();
    }

    public bool HasStructureChunkInRange(StructureSet p_256489_, int p_256593_, int p_256115_, int p_256619_)
    {
        StructurePlacement structureplacement = p_256489_.Placement;

        for (int i = p_256593_ - p_256619_; i <= p_256593_ + p_256619_; i++)
        {
            for (int j = p_256115_ - p_256619_; j <= p_256115_ + p_256619_; j++)
            {
                if (structureplacement.isStructureChunk(this, i, j))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
