using Generator.Enums;
using Generator.Util;
using Generator.World.Phys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.BlockPos
public class BlockPosition : Vec3i
{
    public static readonly int PACKED_HORIZONTAL_LENGTH = 1 + Mth.log2(Mth.smallestEncompassingPowerOfTwo(30000000));
    public static readonly int PACKED_Y_LENGTH = 64 - 2 * PACKED_HORIZONTAL_LENGTH;
    private static readonly long PACKED_X_MASK = (1L << PACKED_HORIZONTAL_LENGTH) - 1L;
    private static readonly long PACKED_Y_MASK = (1L << PACKED_Y_LENGTH) - 1L;
    private static readonly long PACKED_Z_MASK = (1L << PACKED_HORIZONTAL_LENGTH) - 1L;
    private static readonly int Y_OFFSET = 0;
    private static readonly int Z_OFFSET = PACKED_Y_LENGTH;
    private static readonly int X_OFFSET = PACKED_Y_LENGTH + PACKED_HORIZONTAL_LENGTH;
    public static readonly int MAX_HORIZONTAL_COORDINATE = (1 << PACKED_HORIZONTAL_LENGTH) / 2 - 1;

    public BlockPosition(int x, int y, int z)
        : base(x, y, z)
    {
    }

    public BlockPosition(Vec3i vec)
        : this(vec.X, vec.Y, vec.Z)
    {
    }

    public static new readonly BlockPosition ZERO = new BlockPosition(0, 0, 0);

    public static long Offset(long distance, DirectionType directionType)
    {
        Direction direction = Direction.Directions[directionType];
        return Offset(distance, direction.StepX, direction.StepY, direction.StepZ);
    }

    public static long Offset(long distance, int stepX, int stepY, int stepZ)
    {
        return AsLong(GetX(distance) + stepX, GetY(distance) + stepY, GetZ(distance) + stepZ);
    }

    public static int GetX(long distance)
    {
        return (int)(distance << 64 - X_OFFSET - PACKED_HORIZONTAL_LENGTH >> 64 - PACKED_HORIZONTAL_LENGTH);
    }

    public static int GetY(long distance)
    {
        return (int)(distance << 64 - PACKED_Y_LENGTH >> 64 - PACKED_Y_LENGTH);
    }

    public static int GetZ(long distance)
    {
        return (int)(distance << 64 - Z_OFFSET - PACKED_HORIZONTAL_LENGTH >> 64 - PACKED_HORIZONTAL_LENGTH);
    }

    public static BlockPosition Of(long distance)
    {
        return new BlockPosition(GetX(distance), GetY(distance), GetZ(distance));
    }

    public static BlockPosition Containing(double x, double y, double z)
    {
        return new BlockPosition(Mth.floor(x), Mth.floor(y), Mth.floor(z));
    }

    public static BlockPosition Containing(IPosition pos)
    {
        return Containing(pos.X, pos.Y, pos.Z);
    }

    public static BlockPosition Min(BlockPosition blockPos1, BlockPosition blockPos2)
    {
        return new BlockPosition(
            Math.Min(blockPos1.X, blockPos2.X),
            Math.Min(blockPos1.Y, blockPos2.Y),
            Math.Min(blockPos1.Z, blockPos2.Z)
        );
    }

    public static BlockPosition Max(BlockPosition blockPos1, BlockPosition blockPos2)
    {
        return new BlockPosition(
            Math.Max(blockPos1.X, blockPos2.X),
            Math.Max(blockPos1.Y, blockPos2.Y),
            Math.Max(blockPos1.Z, blockPos2.Z)
        );
    }

    public long AsLong()
    {
        return AsLong(X, Y, Z);
    }

    public static long AsLong(int x, int y, int z)
    {
        long i = 0L;
        i |= (x & PACKED_X_MASK) << X_OFFSET;
        i |= (y & PACKED_Y_MASK) << Y_OFFSET;
        return i | (z & PACKED_Z_MASK) << Z_OFFSET;
    }

    public static long GetFlatIndex(long index)
    {
        return index & -16L;
    }

    public new BlockPosition Offset(int x, int y, int z)
    {
        return x == 0 && y == 0 && z == 0
            ? this
            : new BlockPosition(X + x, Y + y, Z + z);
    }

    public Vec3 GetCenter()
    {
        return Vec3.AtCenterOf(this);
    }

    public Vec3 GetBottomCenter()
    {
        return Vec3.AtBottomCenterOf(this);
    }

    public new BlockPosition Offset(Vec3i vec)
    {
        return Offset(vec.X, vec.Y, vec.Z);
    }

    public new BlockPosition Subtract(Vec3i vec)
    {
        return Offset(-vec.X, -vec.Y, -vec.Z);
    }

    public new BlockPosition Multiply(int factor)
    {
        if (factor == 1)
        {
            return this;
        }
        else
        {
            return factor == 0 ? ZERO : new BlockPosition(X * factor, Y * factor, Z * factor);
        }
    }

    public new BlockPosition Above(int offset = 1)
    {
        return Relative(DirectionType.UP, offset);
    }

    public new BlockPosition Below(int offset = 1)
    {
        return Relative(DirectionType.DOWN, offset);
    }

    public new BlockPosition North(int offset = 1)
    {
        return Relative(DirectionType.NORTH, offset);
    }

    public new BlockPosition South(int offset = 1)
    {
        return Relative(DirectionType.SOUTH, offset);
    }

    public new BlockPosition West(int offset = 1)
    {
        return Relative(DirectionType.WEST, offset);
    }

    public new BlockPosition East(int offset = 1)
    {
        return Relative(DirectionType.EAST, offset);
    }

    public new BlockPosition Relative(DirectionType directionType, int offset = 1)
    {
        Direction direction = Direction.Directions[directionType];
        return offset == 0
            ? this
            : new BlockPosition(
                X + direction.StepX * offset,
                Y + direction.StepY * offset,
                Z + direction.StepZ * offset
            );
    }

    public new BlockPosition Relative(AxisType axisType, int offset)
    {
        if (offset == 0)
        {
            return this;
        }
        else
        {
            int i = axisType == AxisType.X ? offset : 0;
            int j = axisType == AxisType.Y ? offset : 0;
            int k = axisType == AxisType.Z ? offset : 0;
            return new BlockPosition(X + i, Y + j, Z + k);
        }
    }

    public BlockPosition Rotate(RotationType rotationType)
    {
        switch (rotationType)
        {
            case RotationType.CLOCKWISE_90:
                return new BlockPosition(-Z, Y, X);
            case RotationType.CLOCKWISE_180:
                return new BlockPosition(-X, Y, -Z);
            case RotationType.COUNTERCLOCKWISE_90:
                return new BlockPosition(Z, Y, -X);
            default:
                return this;
        }
    }

    public new BlockPosition Cross(Vec3i vec)
    {
        return new BlockPosition(
            Y * vec.Z - Z * vec.Y,
            Z * vec.X - X * vec.Z,
            X * vec.Y - Y * vec.X
        );
    }

    public BlockPosition AtY(int y)
    {
        return new BlockPosition(X, y, Z);
    }

    public BlockPosition Immutable()
    {
        return this;
    }

    //public BlockPosition.MutableBlockPos mutable()
    //{
    //    return new BlockPosition.MutableBlockPos(X, Y, Z);
    //}

    public Vec3 ClampLocationWithin(Vec3 vec3)
    {
        return new Vec3(
            Mth.clamp(vec3.X, X + 1.0E-5F, X + 1.0 - 1.0E-5F),
            Mth.clamp(vec3.Y, Y + 1.0E-5F, Y + 1.0 - 1.0E-5F),
            Mth.clamp(vec3.Z, Z + 1.0E-5F, Z + 1.0 - 1.0E-5F)
        );
    }
}
