using Generator.Enums;
using Generator.Util;
using Generator.World.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.SectionPos
public class SectionPosition : Vec3i
{
    public static readonly int SECTION_BITS = 4;
    public static readonly int SECTION_SIZE = 16;
    public static readonly int SECTION_MASK = 15;
    public static readonly int SECTION_HALF_SIZE = 8;
    public static readonly int SECTION_MAX_INDEX = 15;
    private static readonly int PACKED_X_LENGTH = 22;
    private static readonly int PACKED_Y_LENGTH = 20;
    private static readonly int PACKED_Z_LENGTH = 22;
    private static readonly long PACKED_X_MASK = 4194303L;
    private static readonly long PACKED_Y_MASK = 1048575L;
    private static readonly long PACKED_Z_MASK = 4194303L;
    private static readonly int Y_OFFSET = 0;
    private static readonly int Z_OFFSET = 20;
    private static readonly int X_OFFSET = 42;
    private static readonly int RELATIVE_X_SHIFT = 8;
    private static readonly int RELATIVE_Y_SHIFT = 0;
    private static readonly int RELATIVE_Z_SHIFT = 4;

    public SectionPosition(int x, int y, int z)
        : base(x, y, z)
    {
    }

    public static SectionPosition Of(int x, int y, int z)
    {
        return new SectionPosition(x, y, z);
    }

    public static SectionPosition Of(BlockPosition blockPos)
    {
        return new SectionPosition(BlockToSectionCoord(blockPos.X), BlockToSectionCoord(blockPos.Y), BlockToSectionCoord(blockPos.Z));
    }

    public static SectionPosition Of(ChunkPosition chunkPos, int y)
    {
        return new SectionPosition(chunkPos.X, y, chunkPos.Z);
    }

    //public static SectionPosition Of(EntityAccess p_235862_)
    //{
    //    return Of(p_235862_.blockPosition());
    //}

    public static SectionPosition Of(IPosition pos)
    {
        return new SectionPosition(BlockToSectionCoord(pos.X), BlockToSectionCoord(pos.Y), BlockToSectionCoord(pos.Z));
    }

    public static SectionPosition Of(long coordinate)
    {
        return new SectionPosition(GetX(coordinate), GetY(coordinate), GetZ(coordinate));
    }

    //public static SectionPosition BottomOf(ChunkAccess p_175563_)
    //{
    //    return Of(p_175563_.getPos(), p_175563_.getMinSectionY());
    //}

    public static long Offset(long distance, DirectionType directionType)
    {
        Direction direction = Direction.Directions[directionType];
        return Offset(distance, direction.StepX, direction.StepY, direction.StepZ);
    }

    public static long Offset(long distance, int stepX, int stepY, int stepZ)
    {
        return AsLong(GetX(distance) + stepX, GetY(distance) + stepY, GetZ(distance) + stepZ);
    }

    public static int PosToSectionCoord(double positionCoord)
    {
        return BlockToSectionCoord(Mth.floor(positionCoord));
    }

    public static int BlockToSectionCoord(int blockCoord)
    {
        return blockCoord >> SECTION_BITS;
    }

    public static int BlockToSectionCoord(double blockCoord)
    {
        return Mth.floor(blockCoord) >> SECTION_BITS;
    }

    public static int SectionRelative(int blockCoord)
    {
        return blockCoord & SECTION_MASK;
    }

    public static short SectionRelativePos(BlockPosition blockPos)
    {
        int x = SectionRelative(blockPos.X);
        int y = SectionRelative(blockPos.Y);
        int z = SectionRelative(blockPos.Z);
        return (short)(x << RELATIVE_X_SHIFT | z << RELATIVE_Z_SHIFT | y << RELATIVE_Y_SHIFT);
    }

    public static int SectionRelativeX(short blockCoord)
    {
        return blockCoord >>> RELATIVE_X_SHIFT & SECTION_MASK;
    }

    public static int SectionRelativeY(short blockCoord)
    {
        return blockCoord >>> RELATIVE_Y_SHIFT & SECTION_MASK;
    }

    public static int SectionRelativeZ(short blockCoord)
    {
        return blockCoord >>> RELATIVE_Z_SHIFT & SECTION_MASK;
    }

    public int RelativeToBlockX(short blockCoord)
    {
        return MinBlockX() + SectionRelativeX(blockCoord);
    }

    public int RelativeToBlockY(short blockCoord)
    {
        return MinBlockY() + SectionRelativeY(blockCoord);
    }

    public int RelativeToBlockZ(short blockCoord)
    {
        return MinBlockZ() + SectionRelativeZ(blockCoord);
    }

    public BlockPosition RelativeToBlockPos(short blockCoord)
    {
        return new BlockPosition(RelativeToBlockX(blockCoord), RelativeToBlockY(blockCoord), RelativeToBlockZ(blockCoord));
    }

    public static int SectionToBlockCoord(int sectionCoord)
    {
        return sectionCoord << SECTION_BITS;
    }

    public static int SectionToBlockCoord(int sectionCoord, int blockCoord)
    {
        return SectionToBlockCoord(sectionCoord) + blockCoord;
    }

    public static int GetX(long coordinate)
    {
        //return (int)(coordinate << 0 >> 42);
        return (int)(coordinate << (64 - X_OFFSET - PACKED_X_LENGTH) >> (64 - PACKED_X_LENGTH));
    }

    public static int GetY(long coordinate)
    {
        //return (int)(coordinate << 44 >> 44);
        return (int)(coordinate << (64 - Y_OFFSET - PACKED_Y_LENGTH) >> (64 - PACKED_Y_LENGTH));
    }

    public static int GetZ(long coordinate)
    {
        //return (int)(coordinate << 22 >> 42);
        return (int)(coordinate << (64 - Z_OFFSET - PACKED_Z_LENGTH) >> (64 - PACKED_Z_LENGTH));
    }

    public int MinBlockX()
    {
        return SectionToBlockCoord(X);
    }

    public int MinBlockY()
    {
        return SectionToBlockCoord(Y);
    }

    public int MinBlockZ()
    {
        return SectionToBlockCoord(Z);
    }

    public int MaxBlockX()
    {
        return SectionToBlockCoord(X, SECTION_MAX_INDEX);
    }

    public int MaxBlockY()
    {
        return SectionToBlockCoord(Y, SECTION_MAX_INDEX);
    }

    public int MaxBlockZ()
    {
        return SectionToBlockCoord(Z, SECTION_MAX_INDEX);
    }

    public static long BlockToSection(long blockCoord)
    {
        return AsLong(BlockToSectionCoord(BlockPosition.GetX(blockCoord)), BlockToSectionCoord(BlockPosition.GetY(blockCoord)), BlockToSectionCoord(BlockPosition.GetZ(blockCoord)));
    }

    public static long GetZeroNode(int x, int z)
    {
        return GetZeroNode(AsLong(x, 0, z));
    }

    public static long GetZeroNode(long coordinate)
    {
        return coordinate & -1048576L;
    }

    public static long SectionToChunk(long coordinate)
    {
        return ChunkPosition.AsLong(GetX(coordinate), GetZ(coordinate));
    }

    public BlockPosition Origin()
    {
        return new BlockPosition(SectionToBlockCoord(X), SectionToBlockCoord(Y), SectionToBlockCoord(Z));
    }

    public BlockPosition Center()
    {
        return Origin().Offset(SECTION_HALF_SIZE, SECTION_HALF_SIZE, SECTION_HALF_SIZE);
    }

    public ChunkPosition Chunk()
    {
        return new ChunkPosition(X, Z);
    }

    public static long AsLong(BlockPosition blockPos)
    {
        return AsLong(BlockToSectionCoord(blockPos.X), BlockToSectionCoord(blockPos.Y), BlockToSectionCoord(blockPos.Z));
    }

    public static long AsLong(int x, int y, int z)
    {
        long i = 0L;
        i |= (x & PACKED_X_MASK) << X_OFFSET;
        i |= (y & PACKED_Y_MASK) << Y_OFFSET;
        return i | (z & PACKED_Z_MASK) << Z_OFFSET;
    }

    public long AsLong()
    {
        return AsLong(X, Y, Z);
    }

    public new SectionPosition Offset(int x, int y, int z)
    {
        return x == 0 && y == 0 && z == 0
            ? this
            : new SectionPosition(X + x, Y + y, Z + z);
    }

    //public Stream<BlockPosition> blocksInside()
    //{
    //    return BlockPosition.betweenClosedStream(MinBlockX(), MinBlockY(), MinBlockZ(), MaxBlockX(), MaxBlockY(), MaxBlockZ());
    //}

    //public static Stream<SectionPosition> cube(SectionPosition sectionPos, int p_123203_)
    //{
    //    int i = sectionPos.X;
    //    int j = sectionPos.Y;
    //    int k = sectionPos.Z;
    //    return betweenClosedStream(i - p_123203_, j - p_123203_, k - p_123203_, i + p_123203_, j + p_123203_, k + p_123203_);
    //}

    //public static Stream<SectionPosition> aroundChunk(ChunkPosition chunkPos, int p_175559_, int p_175560_, int p_175561_)
    //{
    //    int i = chunkPos.X;
    //    int j = chunkPos.Z;
    //    return betweenClosedStream(i - p_175559_, p_175560_, j - p_175559_, i + p_175559_, p_175561_, j + p_175559_);
    //}

    //public static Stream<SectionPosition> betweenClosedStream(final int p_123178_, final int p_123179_, final int p_123180_, final int p_123181_, final int p_123182_, final int p_123183_)
    //{
    //    return StreamSupport.stream(
    //        new AbstractSpliterator<SectionPosition>((p_123181_ - p_123178_ + 1) * (p_123182_ - p_123179_ + 1) * (p_123183_ - p_123180_ + 1), 64)
    //        {
    //            final Cursor3D cursor = new Cursor3D(p_123178_, p_123179_, p_123180_, p_123181_, p_123182_, p_123183_);

    //            @Override
    //            public bool tryAdvance(Consumer<? super SectionPosition> p_123271_)
    //            {
    //                if (this.cursor.advance())
    //                {
    //                    p_123271_.accept(new SectionPosition(this.cursor.nextX(), this.cursor.nextY(), this.cursor.nextZ()));
    //                    return true;
    //                }
    //                else
    //                {
    //                    return false;
    //                }
    //            }
    //        }, false);
    //}

    public static void AroundAndAtBlockPos(BlockPosition blockPos, Action<long> action)
    {
        AroundAndAtBlockPos(blockPos.X, blockPos.Y, blockPos.Z, action);
    }

    public static void AroundAndAtBlockPos(long coordinate, Action<long> action)
    {
        AroundAndAtBlockPos(BlockPosition.GetX(coordinate), BlockPosition.GetY(coordinate), BlockPosition.GetZ(coordinate), action);
    }

    public static void AroundAndAtBlockPos(int x, int y, int z, Action<long> action)
    {
        int i = BlockToSectionCoord(x - 1);
        int j = BlockToSectionCoord(x + 1);
        int k = BlockToSectionCoord(y - 1);
        int l = BlockToSectionCoord(y + 1);
        int i1 = BlockToSectionCoord(z - 1);
        int j1 = BlockToSectionCoord(z + 1);
        if (i == j && k == l && i1 == j1)
        {
            action(AsLong(i, k, i1));
        }
        else
        {
            for (int k1 = i; k1 <= j; k1++)
            {
                for (int l1 = k; l1 <= l; l1++)
                {
                    for (int i2 = i1; i2 <= j1; i2++)
                    {
                        action(AsLong(k1, l1, i2));
                    }
                }
            }
        }
    }
}
