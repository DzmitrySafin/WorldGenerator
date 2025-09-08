using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Density;

//source: net.minecraft.world.level.levelgen.Beardifier
//source: net.minecraft.world.level.levelgen.DensityFunctions.BeardifierOrMarker
//source: net.minecraft.world.level.levelgen.DensityFunction.SimpleFunction
public class Beardifier : IDensityFunction
{
    public static readonly int BEARD_KERNEL_RADIUS = 12;
    private static readonly int BEARD_KERNEL_SIZE = 24;
    private static readonly float[] BEARD_KERNEL;
    //private readonly ObjectListIterator<Beardifier.Rigid> pieceIterator;
    //private readonly ObjectListIterator<JigsawJunction> junctionIterator;

    // class to be implemented:
    //public record Rigid(BoundingBox box, TerrainAdjustment terrainAdjustment, int groundLevelDelta) { }

    static Beardifier()
    {
        BEARD_KERNEL = new float[13824];
        for (int i = 0; i < BEARD_KERNEL_SIZE; i++)
        {
            for (int j = 0; j < BEARD_KERNEL_SIZE; j++)
            {
                for (int k = 0; k < BEARD_KERNEL_SIZE; k++)
                {
                    BEARD_KERNEL[i * BEARD_KERNEL_SIZE * BEARD_KERNEL_SIZE + j * BEARD_KERNEL_SIZE + k] = (float)computeBeardContribution(j - BEARD_KERNEL_RADIUS, k - BEARD_KERNEL_RADIUS, i - BEARD_KERNEL_RADIUS);
                }
            }
        }
    }

    //public static Beardifier ForStructuresInChunk(StructureManager p_223938_, ChunkPosition chunkPos)
    //{
    //    int i = chunkPos.GetMinBlockX();
    //    int j = chunkPos.GetMinBlockZ();
    //    ObjectList<Beardifier.Rigid> objectlist = new ObjectArrayList<>(10);
    //    ObjectList<JigsawJunction> objectlist1 = new ObjectArrayList<>(32);
    //    p_223938_.startsForStructure(chunkPos, p_223941_->p_223941_.terrainAdaptation() != TerrainAdjustment.NONE).forEach(p_223936_-> {
    //        TerrainAdjustment terrainadjustment = p_223936_.getStructure().terrainAdaptation();

    //        foreach (StructurePiece structurepiece in p_223936_.getPieces())
    //        {
    //            if (structurepiece.isCloseToChunk(chunkPos, 12))
    //            {
    //                if (structurepiece is PoolElementStructurePiece poolelementstructurepiece)
    //                {
    //                    StructureTemplatePool.Projection structuretemplatepool$projection = poolelementstructurepiece.getElement().getProjection();
    //                    if (structuretemplatepool$projection == StructureTemplatePool.Projection.RIGID) {
    //                        objectlist.add(new Beardifier.Rigid(poolelementstructurepiece.getBoundingBox(), terrainadjustment, poolelementstructurepiece.getGroundLevelDelta()));
    //                    }

    //                    for (JigsawJunction jigsawjunction : poolelementstructurepiece.getJunctions())
    //                    {
    //                        int k = jigsawjunction.getSourceX();
    //                        int l = jigsawjunction.getSourceZ();
    //                        if (k > i - 12 && l > j - 12 && k < i + 15 + 12 && l < j + 15 + 12)
    //                        {
    //                            objectlist1.add(jigsawjunction);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    objectlist.add(new Beardifier.Rigid(structurepiece.getBoundingBox(), terrainadjustment, 0));
    //                }
    //            }
    //        }
    //    });
    //    return new Beardifier(objectlist.iterator(), objectlist1.iterator());
    //}

    //public Beardifier(ObjectListIterator<Beardifier.Rigid> p_223917_, ObjectListIterator<JigsawJunction> p_223918_)
    //{
    //    this.pieceIterator = p_223917_;
    //    this.junctionIterator = p_223918_;
    //}

    public double Compute(IFunctionContext context)
    {
        int i = context.BlockX;
        int j = context.BlockY;
        int k = context.BlockZ;
        double d0 = 0.0;

        //while (this.pieceIterator.hasNext())
        //{
        //    Beardifier.Rigid beardifier$rigid = this.pieceIterator.next();
        //    BoundingBox boundingbox = beardifier$rigid.box();
        //    int l = beardifier$rigid.groundLevelDelta();
        //    int i1 = Math.Max(0, Math.Max(boundingbox.minX() - i, i - boundingbox.maxX()));
        //    int j1 = Math.Max(0, Math.Max(boundingbox.minZ() - k, k - boundingbox.maxZ()));
        //    int k1 = boundingbox.minY() + l;
        //    int l1 = j - k1;

        //    int i2 = beardifier$rigid.terrainAdjustment() switch
        //    {
        //        case NONE => 0,
        //        case BURY, BEARD_THIN => l1,
        //        case BEARD_BOX => Math.Max(0, Math.Max(k1 - j, j - boundingbox.maxY())),
        //        case ENCAPSULATE => Math.Max(0, Math.Max(boundingbox.minY() - j, j - boundingbox.maxY()))
        //    };

        //    d0 += beardifier$rigid.terrainAdjustment() switch
        //    {
        //        case NONE => 0.0,
        //        case BURY => getBuryContribution(i1, i2 / 2.0, j1),
        //        case BEARD_THIN, BEARD_BOX => getBeardContribution(i1, i2, j1, l1) * 0.8,
        //        case ENCAPSULATE => getBuryContribution(i1 / 2.0, i2 / 2.0, j1 / 2.0) * 0.8
        //    };
        //}

        //this.pieceIterator.back(int.MaxValue);

        //while (this.junctionIterator.hasNext())
        //{
        //    JigsawJunction jigsawjunction = this.junctionIterator.next();
        //    int j2 = i - jigsawjunction.getSourceX();
        //    int k2 = j - jigsawjunction.getSourceGroundY();
        //    int l2 = k - jigsawjunction.getSourceZ();
        //    d0 += getBeardContribution(j2, k2, l2, k2) * 0.4;
        //}

        //this.junctionIterator.back(int.MaxValue);
        return d0;
    }

    public void FillArray(double[] array, IFunctionContextProvider contextProvider)
    {
        contextProvider.FillAllDirectly(array, this);
    }

    public IDensityFunction MapAll(IDensityVisitor densityVisitor)
    {
        return densityVisitor.Apply(this);
    }

    public double MaxValue => double.PositiveInfinity;

    public double MinValue => double.NegativeInfinity;

    private static double getBuryContribution(double p_328731_, double p_336073_, double p_329819_)
    {
        double d0 = Mth.length(p_328731_, p_336073_, p_329819_);
        return Mth.clampedMap(d0, 0.0, 6.0, 1.0, 0.0);
    }

    private static double getBeardContribution(int p_223926_, int p_223927_, int p_223928_, int p_223929_)
    {
        int i = p_223926_ + BEARD_KERNEL_RADIUS;
        int j = p_223927_ + BEARD_KERNEL_RADIUS;
        int k = p_223928_ + BEARD_KERNEL_RADIUS;
        if (isInKernelRange(i) && isInKernelRange(j) && isInKernelRange(k))
        {
            double d0 = p_223929_ + 0.5;
            double d1 = Mth.lengthSquared(p_223926_, d0, p_223928_);
            double d2 = -d0 * Mth.fastInvSqrt(d1 / 2.0) / 2.0;
            return d2 * BEARD_KERNEL[k * BEARD_KERNEL_SIZE * BEARD_KERNEL_SIZE + i * BEARD_KERNEL_SIZE + j];
        }
        else
        {
            return 0.0;
        }
    }

    private static bool isInKernelRange(int p_223920_)
    {
        return p_223920_ >= 0 && p_223920_ < BEARD_KERNEL_SIZE;
    }

    private static double computeBeardContribution(int p_158092_, int p_158093_, int p_158094_)
    {
        return computeBeardContribution(p_158092_, p_158093_ + 0.5, p_158094_);
    }

    private static double computeBeardContribution(int p_223922_, double p_223923_, int p_223924_)
    {
        double d0 = Mth.lengthSquared(p_223922_, p_223923_, p_223924_);
        return Math.Pow(Math.E, -d0 / 16.0);
    }
}
