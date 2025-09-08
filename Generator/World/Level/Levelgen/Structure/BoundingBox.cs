using Generator.Core;
using Generator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen.Structure;

//source: net.minecraft.world.level.levelgen.structure.BoundingBox
public class BoundingBox
{
    public int MinX { get; private set; }
    public int MinY { get; private set; }
    public int MinZ { get; private set; }
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }
    public int MaxZ { get; private set; }

    public BoundingBox(BlockPosition p_162364_)
        : this(p_162364_.X, p_162364_.Y, p_162364_.Z, p_162364_.X, p_162364_.Y, p_162364_.Z)
    {
    }

    public BoundingBox(int p_71001_, int p_71002_, int p_71003_, int p_71004_, int p_71005_, int p_71006_)
    {
        MinX = p_71001_;
        MinY = p_71002_;
        MinZ = p_71003_;
        MaxX = p_71004_;
        MaxY = p_71005_;
        MaxZ = p_71006_;
        if (p_71004_ < p_71001_ || p_71005_ < p_71002_ || p_71006_ < p_71003_)
        {
            //Util.logAndPauseIfInIde("Invalid bounding box data, inverted bounds for: " + this);
            MinX = Math.Min(p_71001_, p_71004_);
            MinY = Math.Min(p_71002_, p_71005_);
            MinZ = Math.Min(p_71003_, p_71006_);
            MaxX = Math.Max(p_71001_, p_71004_);
            MaxY = Math.Max(p_71002_, p_71005_);
            MaxZ = Math.Max(p_71003_, p_71006_);
        }
    }

    public static BoundingBox fromCorners(Vec3i p_162376_, Vec3i p_162377_)
    {
        return new BoundingBox(
                Math.Min(p_162376_.X, p_162377_.X),
                Math.Min(p_162376_.Y, p_162377_.Y),
                Math.Min(p_162376_.Z, p_162377_.Z),
                Math.Max(p_162376_.X, p_162377_.X),
                Math.Max(p_162376_.Y, p_162377_.Y),
                Math.Max(p_162376_.Z, p_162377_.Z)
        );
    }

    public static BoundingBox infinite()
    {
        return new BoundingBox(int.MinValue, int.MinValue, int.MinValue, int.MaxValue, int.MaxValue, int.MaxValue);
    }

    public static BoundingBox orientBox(int p_71032_, int p_71033_, int p_71034_, int p_71035_, int p_71036_, int p_71037_, int p_71038_, int p_71039_, int p_71040_, DirectionType p_71041_)
    {
        switch (p_71041_)
        {
            case DirectionType.SOUTH:
            default:
                return new BoundingBox(
                    p_71032_ + p_71035_,
                    p_71033_ + p_71036_,
                    p_71034_ + p_71037_,
                    p_71032_ + p_71038_ - 1 + p_71035_,
                    p_71033_ + p_71039_ - 1 + p_71036_,
                    p_71034_ + p_71040_ - 1 + p_71037_
                );
            case DirectionType.NORTH:
                return new BoundingBox(
                    p_71032_ + p_71035_,
                    p_71033_ + p_71036_,
                    p_71034_ - p_71040_ + 1 + p_71037_,
                    p_71032_ + p_71038_ - 1 + p_71035_,
                    p_71033_ + p_71039_ - 1 + p_71036_,
                    p_71034_ + p_71037_
                );
            case DirectionType.WEST:
                return new BoundingBox(
                    p_71032_ - p_71040_ + 1 + p_71037_,
                    p_71033_ + p_71036_,
                    p_71034_ + p_71035_,
                    p_71032_ + p_71037_,
                    p_71033_ + p_71039_ - 1 + p_71036_,
                    p_71034_ + p_71038_ - 1 + p_71035_
                );
            case DirectionType.EAST:
                return new BoundingBox(
                    p_71032_ + p_71037_,
                    p_71033_ + p_71036_,
                    p_71034_ + p_71035_,
                    p_71032_ + p_71040_ - 1 + p_71037_,
                    p_71033_ + p_71039_ - 1 + p_71036_,
                    p_71034_ + p_71038_ - 1 + p_71035_
                );
        }
    }

    //public Stream<ChunkPosition> intersectingChunks()
    //{
    //    int i = SectionPosition.BlockToSectionCoord(MinX);
    //    int j = SectionPosition.BlockToSectionCoord(MinZ);
    //    int k = SectionPosition.BlockToSectionCoord(MaxX);
    //    int l = SectionPosition.BlockToSectionCoord(MaxZ);
    //    return ChunkPosition.RangeClosed(new ChunkPosition(i, j), new ChunkPosition(k, l));
    //}

    public bool intersects(BoundingBox p_71050_)
    {
        return MaxX >= p_71050_.MinX
            && MinX <= p_71050_.MaxX
            && MaxZ >= p_71050_.MinZ
            && MinZ <= p_71050_.MaxZ
            && MaxY >= p_71050_.MinY
            && MinY <= p_71050_.MaxY;
    }

    public bool intersects(int p_71020_, int p_71021_, int p_71022_, int p_71023_)
    {
        return MaxX >= p_71020_ && MinX <= p_71022_ && MaxZ >= p_71021_ && MinZ <= p_71023_;
    }

    public static BoundingBox? encapsulatingPositions(IEnumerable<BlockPosition> p_162379_)
    {
        if (!p_162379_.Any())
        {
            return null;
        }

        BoundingBox boundingbox = new BoundingBox(p_162379_.First());
        foreach (BlockPosition pos in p_162379_.Skip(1))
        {
            boundingbox.encapsulate(pos);
        }
        return boundingbox;
    }

    public static BoundingBox? encapsulatingBoxes(IEnumerable<BoundingBox> p_162389_)
    {
        if (!p_162389_.Any())
        {
            return null;
        }

        BoundingBox boundingbox = p_162389_.First();
        BoundingBox boundingbox1 = new BoundingBox(boundingbox.MinX, boundingbox.MinY, boundingbox.MinZ, boundingbox.MaxX, boundingbox.MaxY, boundingbox.MaxZ);
        foreach (BoundingBox box in p_162389_.Skip(1))
        {
            boundingbox1.encapsulate(box);
        }
        return boundingbox1;
    }

    [Obsolete]
    public BoundingBox encapsulate(BoundingBox p_162387_)
    {
        MinX = Math.Min(MinX, p_162387_.MinX);
        MinY = Math.Min(MinY, p_162387_.MinY);
        MinZ = Math.Min(MinZ, p_162387_.MinZ);
        MaxX = Math.Max(MaxX, p_162387_.MaxX);
        MaxY = Math.Max(MaxY, p_162387_.MaxY);
        MaxZ = Math.Max(MaxZ, p_162387_.MaxZ);
        return this;
    }

    [Obsolete]
    public BoundingBox encapsulate(BlockPosition p_162372_)
    {
        MinX = Math.Min(MinX, p_162372_.X);
        MinY = Math.Min(MinY, p_162372_.Y);
        MinZ = Math.Min(MinZ, p_162372_.Z);
        MaxX = Math.Max(MaxX, p_162372_.X);
        MaxY = Math.Max(MaxY, p_162372_.Y);
        MaxZ = Math.Max(MaxZ, p_162372_.Z);
        return this;
    }

    [Obsolete]
    public BoundingBox move(int p_162368_, int p_162369_, int p_162370_)
    {
        MinX += p_162368_;
        MinY += p_162369_;
        MinZ += p_162370_;
        MaxX += p_162368_;
        MaxY += p_162369_;
        MaxZ += p_162370_;
        return this;
    }

    [Obsolete]
    public BoundingBox move(Vec3i p_162374_)
    {
        return this.move(p_162374_.X, p_162374_.Y, p_162374_.Z);
    }

    public BoundingBox moved(int p_71046_, int p_71047_, int p_71048_)
    {
        return new BoundingBox(
            MinX + p_71046_,
            MinY + p_71047_,
            MinZ + p_71048_,
            MaxX + p_71046_,
            MaxY + p_71047_,
            MaxZ + p_71048_
        );
    }

    public BoundingBox inflatedBy(int p_191962_)
    {
        return this.inflatedBy(p_191962_, p_191962_, p_191962_);
    }

    public BoundingBox inflatedBy(int p_332684_, int p_332721_, int p_329326_)
    {
        return new BoundingBox(
            MinX - p_332684_,
            MinY - p_332721_,
            MinZ - p_329326_,
            MaxX + p_332684_,
            MaxY + p_332721_,
            MaxZ + p_329326_
        );
    }

    public bool isInside(Vec3i p_71052_)
    {
        return this.isInside(p_71052_.X, p_71052_.Y, p_71052_.Z);
    }

    public bool isInside(int p_261671_, int p_261537_, int p_261678_)
    {
        return p_261671_ >= MinX
            && p_261671_ <= MaxX
            && p_261678_ >= MinZ
            && p_261678_ <= MaxZ
            && p_261537_ >= MinY
            && p_261537_ <= MaxY;
    }

    public Vec3i getLength()
    {
        return new Vec3i(MaxX - MinX, MaxY - MinY, MaxZ - MinZ);
    }

    public int getXSpan()
    {
        return MaxX - MinX + 1;
    }

    public int getYSpan()
    {
        return MaxY - MinY + 1;
    }

    public int getZSpan()
    {
        return MaxZ - MinZ + 1;
    }

    public BlockPosition getCenter()
    {
        return new BlockPosition(
            MinX + (MaxX - MinX + 1) / 2,
            MinY + (MaxY - MinY + 1) / 2,
            MinZ + (MaxZ - MinZ + 1) / 2
        );
    }

    //public void forAllCorners(Action<BlockPosition> p_162381_)
    //{
    //    BlockPosition.MutableBlockPos blockpos = new BlockPosition.MutableBlockPos();
    //    p_162381_(blockpos.set(MaxX, MaxY, MaxZ));
    //    p_162381_(blockpos.set(MinX, MaxY, MaxZ));
    //    p_162381_(blockpos.set(MaxX, MinY, MaxZ));
    //    p_162381_(blockpos.set(MinX, MinY, MaxZ));
    //    p_162381_(blockpos.set(MaxX, MaxY, MinZ));
    //    p_162381_(blockpos.set(MinX, MaxY, MinZ));
    //    p_162381_(blockpos.set(MaxX, MinY, MinZ));
    //    p_162381_(blockpos.set(MinX, MinY, MinZ));
    //}

    //public override string ToString()
    //{
    //    return MoreObjects.toStringHelper(this)
    //        .add("minX", MinX)
    //        .add("minY", MinY)
    //        .add("minZ", MinZ)
    //        .add("maxX", MaxX)
    //        .add("maxY", MaxY)
    //        .add("maxZ", MaxZ)
    //        .toString();
    //}

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }
        else
        {
            return !(obj is BoundingBox boundingbox)
                ? false
                : MinX == boundingbox.MinX
                    && MinY == boundingbox.MinY
                    && MinZ == boundingbox.MinZ
                    && MaxX == boundingbox.MaxX
                    && MaxY == boundingbox.MaxY
                    && MaxZ == boundingbox.MaxZ;
        }
    }

    public override int GetHashCode()
    {
        int result = 1;
        foreach (int element in new int[] { MinX, MinY, MinZ, MaxX, MaxY, MaxZ })
        {
            result = 31 * result + element.GetHashCode();
        }

        return result;
    }
}
