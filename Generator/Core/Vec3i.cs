using Generator.Enums;
using Generator.Json;
using Generator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.Vec3i
[JsonConverter(typeof(Vec3iConverter))]
public class Vec3i : IComparable<Vec3i>
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public Vec3i(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static readonly Vec3i ZERO = new Vec3i(0, 0, 0);

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }
        else if (!(obj is Vec3i vec3i))
        {
            return false;
        }
        else if (X != vec3i.X)
        {
            return false;
        }
        else
        {
            return Y != vec3i.Y ? false : Z == vec3i.Z;
        }
    }

    public override int GetHashCode()
    {
        return (Y + Z * 31) * 31 + X;
    }

    public int CompareTo(Vec3i? other)
    {
        if (other == null)
        {
            return 1;
        }
        else if (Y == other.Y)
        {
            return Z == other.Z ? X - other.X : Z - other.Z;
        }
        else
        {
            return Y - other.Y;
        }
    }

    protected Vec3i SetX(int x)
    {
        X = x;
        return this;
    }

    protected Vec3i SetY(int y)
    {
        Y = y;
        return this;
    }

    protected Vec3i SetZ(int z)
    {
        Z = z;
        return this;
    }

    public Vec3i Offset(int x, int y, int z)
    {
        return x == 0 && y == 0 && z == 0
            ? this
            : new Vec3i(X + x, Y + y, Z + z);
    }

    public Vec3i Offset(Vec3i vec)
    {
        return Offset(vec.X, vec.Y, vec.Z);
    }

    public Vec3i Subtract(Vec3i vec)
    {
        return Offset(-vec.X, -vec.Y, -vec.Z);
    }

    public Vec3i Multiply(int factor)
    {
        if (factor == 1)
        {
            return this;
        }
        else
        {
            return factor == 0 ? ZERO : new Vec3i(X * factor, Y * factor, Z * factor);
        }
    }

    public Vec3i Above(int offset = 1)
    {
        return Relative(DirectionType.UP, offset);
    }

    public Vec3i Below(int offset = 1)
    {
        return Relative(DirectionType.DOWN, offset);
    }

    public Vec3i North(int offset = 1)
    {
        return Relative(DirectionType.NORTH, offset);
    }

    public Vec3i South(int offset = 1)
    {
        return Relative(DirectionType.SOUTH, offset);
    }

    public Vec3i West(int offset = 1)
    {
        return Relative(DirectionType.WEST, offset);
    }

    public Vec3i East(int offset = 1)
    {
        return Relative(DirectionType.EAST, offset);
    }

    public Vec3i Relative(DirectionType directionType, int offset = 1)
    {
        Direction direction = Direction.Directions[directionType];
        return offset == 0
            ? this
            : new Vec3i(
                X + direction.StepX * offset,
                Y + direction.StepY * offset,
                Z + direction.StepZ * offset
            );
    }

    public Vec3i Relative(AxisType axisType, int offset)
    {
        if (offset == 0)
        {
            return this;
        }
        else
        {
            int x = axisType == AxisType.X ? offset : 0;
            int y = axisType == AxisType.Y ? offset : 0;
            int z = axisType == AxisType.Z ? offset : 0;
            return new Vec3i(X + x, Y + y, Z + z);
        }
    }

    public Vec3i Cross(Vec3i vec)
    {
        return new Vec3i(
            Y * vec.Z - Z * vec.Y,
            Z * vec.X - X * vec.Z,
            X * vec.Y - Y * vec.X
        );
    }

    public bool CloserThan(Vec3i vec, double distance)
    {
        return DistSqr(vec) < Mth.square(distance);
    }

    public bool CloserToCenterThan(IPosition pos, double distance)
    {
        return DistToCenterSqr(pos) < Mth.square(distance);
    }

    public double DistSqr(Vec3i vec)
    {
        return DistToLowCornerSqr(vec.X, vec.Y, vec.Z);
    }

    public double DistToCenterSqr(IPosition pos)
    {
        return DistToCenterSqr(pos.X, pos.Y, pos.Z);
    }

    public double DistToCenterSqr(double x, double y, double z)
    {
        double d0 = X + 0.5 - x;
        double d1 = Y + 0.5 - y;
        double d2 = Z + 0.5 - z;
        return d0 * d0 + d1 * d1 + d2 * d2;
    }

    public double DistToLowCornerSqr(double x, double y, double z)
    {
        double d0 = X - x;
        double d1 = Y - y;
        double d2 = Z - z;
        return d0 * d0 + d1 * d1 + d2 * d2;
    }

    public int DistManhattan(Vec3i vec)
    {
        float f0 = Math.Abs(vec.X - X);
        float f1 = Math.Abs(vec.Y - Y);
        float f2 = Math.Abs(vec.Z - Z);
        return (int)(f0 + f1 + f2);
    }

    public int DistChessboard(Vec3i vec)
    {
        int x = Math.Abs(X - vec.X);
        int y = Math.Abs(Y - vec.Y);
        int z = Math.Abs(Z - vec.Z);
        return Math.Max(Math.Max(x, y), z);
    }

    //public int get(AxisType axisType)
    //{
    //    return axis.choose(X, Y, Z);
    //}

    //public override string ToString()
    //{
    //    return MoreObjects.toStringHelper(this).add("x", X).add("y", Y).add("z", Z).ToString();
    //}

    public string ToShortString()
    {
        return $"{X}, {Y}, {Z}";
    }
}
