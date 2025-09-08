using Generator.Core;
using Generator.Enums;
using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Phys;

//source: net.minecraft.world.phys.Vec3
public class Vec3 : IPosition
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Z { get; private set; }

    public Vec3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static readonly Vec3 ZERO = new Vec3(0.0, 0.0, 0.0);

    public static Vec3 FromRGB24(int rgb)
    {
        double d0 = (rgb >> 16 & 0xFF) / 255.0;
        double d1 = (rgb >> 8 & 0xFF) / 255.0;
        double d2 = (rgb & 0xFF) / 255.0;
        return new Vec3(d0, d1, d2);
    }

    public static Vec3 AtLowerCornerOf(Vec3i vec)
    {
        return new Vec3(vec.X, vec.Y, vec.Z);
    }

    public static Vec3 AtLowerCornerWithOffset(Vec3i vec, double x, double y, double z)
    {
        return new Vec3(vec.X + x, vec.Y + y, vec.Z + z);
    }

    public static Vec3 AtCenterOf(Vec3i vec)
    {
        return AtLowerCornerWithOffset(vec, 0.5, 0.5, 0.5);
    }

    public static Vec3 AtBottomCenterOf(Vec3i vec)
    {
        return AtLowerCornerWithOffset(vec, 0.5, 0.0, 0.5);
    }

    public static Vec3 UpFromBottomCenterOf(Vec3i vec, double y)
    {
        return AtLowerCornerWithOffset(vec, 0.5, y, 0.5);
    }

    //public Vec3(Vector3f vec3f)
    //    : this(vec3f.X, vec3f.Y, vec3f.Z)
    //{
    //}

    public Vec3(Vec3i vec)
        : this(vec.X, vec.Y, vec.Z)
    {
    }

    public Vec3 VectorTo(Vec3 vec3)
    {
        return new Vec3(vec3.X - X, vec3.Y - Y, vec3.Z - Z);
    }

    public Vec3 Normalize()
    {
        double d0 = Math.Sqrt(X * X + Y * Y + Z * Z);
        return d0 < 1.0E-5F ? ZERO : new Vec3(X / d0, Y / d0, Z / d0);
    }

    public double Dot(Vec3 vec3)
    {
        return X * vec3.X + Y * vec3.Y + Z * vec3.Z;
    }

    public Vec3 Cross(Vec3 vec3)
    {
        return new Vec3(
            Y * vec3.Z - Z * vec3.Y,
            Z * vec3.X - X * vec3.Z,
            X * vec3.Y - Y * vec3.X
        );
    }

    public Vec3 Subtract(Vec3 vec3)
    {
        return Subtract(vec3.X, vec3.Y, vec3.Z);
    }

    public Vec3 Subtract(double offset)
    {
        return Subtract(offset, offset, offset);
    }

    public Vec3 Subtract(double x, double y, double z)
    {
        return Add(-x, -y, -z);
    }

    public Vec3 Add(double offset)
    {
        return Add(offset, offset, offset);
    }

    public Vec3 Add(Vec3 vec3)
    {
        return Add(vec3.X, vec3.Y, vec3.Z);
    }

    public Vec3 Add(double x, double y, double z)
    {
        return new Vec3(X + x, Y + y, Z + z);
    }

    public bool CloserThan(IPosition pos, double distance)
    {
        return DistanceToSqr(pos.X, pos.Y, pos.Z) < distance * distance;
    }

    public double DistanceTo(Vec3 vec3)
    {
        double d0 = vec3.X - X;
        double d1 = vec3.Y - Y;
        double d2 = vec3.Z - Z;
        return Math.Sqrt(d0 * d0 + d1 * d1 + d2 * d2);
    }

    public double DistanceToSqr(Vec3 vec3)
    {
        double d0 = vec3.X - X;
        double d1 = vec3.Y - Y;
        double d2 = vec3.Z - Z;
        return d0 * d0 + d1 * d1 + d2 * d2;
    }

    public double DistanceToSqr(double x, double y, double z)
    {
        double d0 = x - X;
        double d1 = y - Y;
        double d2 = z - Z;
        return d0 * d0 + d1 * d1 + d2 * d2;
    }

    public bool CloserThan(Vec3 vec3, double distance, double y)
    {
        double d0 = vec3.X - X;
        double d1 = vec3.Y - Y;
        double d2 = vec3.Z - Z;
        return Mth.lengthSquared(d0, d2) < Mth.square(distance) && Math.Abs(d1) < y;
    }

    public Vec3 Scale(double factor)
    {
        return Multiply(factor, factor, factor);
    }

    public Vec3 Reverse()
    {
        return Scale(-1.0);
    }

    public Vec3 Multiply(Vec3 vec3)
    {
        return Multiply(vec3.X, vec3.Y, vec3.Z);
    }

    public Vec3 Multiply(double x, double y, double z)
    {
        return new Vec3(X * x, Y * y, Z * z);
    }

    public Vec3 Horizontal()
    {
        return new Vec3(X, 0.0, Z);
    }

    public Vec3 OffsetRandom(IRandomSource randomSource, float factor)
    {
        return Add((randomSource.NextFloat() - 0.5F) * factor, (randomSource.NextFloat() - 0.5F) * factor, (randomSource.NextFloat() - 0.5F) * factor);
    }

    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public double LengthSqr()
    {
        return X * X + Y * Y + Z * Z;
    }

    public double HorizontalDistance()
    {
        return Math.Sqrt(X * X + Z * Z);
    }

    public double HorizontalDistanceSqr()
    {
        return X * X + Z * Z;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }
        else if (obj is not Vec3 vec3)
        {
            return false;
        }
        else if (X.CompareTo(vec3.X) != 0)
        {
            return false;
        }
        else
        {
            return Y.CompareTo(vec3.Y) != 0 ? false : Z.CompareTo(vec3.Z) == 0;
        }
    }

    public override int GetHashCode()
    {
        long j = BitConverter.DoubleToInt64Bits(X); // Double.doubleToLongBits(X);
        int i = (int)(j ^ j >>> 32);
        j = BitConverter.DoubleToInt64Bits(Y); // Double.doubleToLongBits(Y);
        i = 31 * i + (int)(j ^ j >>> 32);
        j = BitConverter.DoubleToInt64Bits(Z); // Double.doubleToLongBits(Z);
        return 31 * i + (int)(j ^ j >>> 32);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public Vec3 Lerp(Vec3 vec3, double value)
    {
        return new Vec3(
            Mth.lerp(value, X, vec3.X),
            Mth.lerp(value, Y, vec3.Y),
            Mth.lerp(value, Z, vec3.Z)
        );
    }

    public Vec3 XRot(float angle)
    {
        float f0 = Mth.cos(angle);
        float f1 = Mth.sin(angle);
        double d0 = X;
        double d1 = Y * f0 + Z * f1;
        double d2 = Z * f0 - Y * f1;
        return new Vec3(d0, d1, d2);
    }

    public Vec3 YRot(float angle)
    {
        float f0 = Mth.cos(angle);
        float f1 = Mth.sin(angle);
        double d0 = X * f0 + Z * f1;
        double d1 = Y;
        double d2 = Z * f0 - X * f1;
        return new Vec3(d0, d1, d2);
    }

    public Vec3 ZRot(float angle)
    {
        float f0 = Mth.cos(angle);
        float f1 = Mth.sin(angle);
        double d0 = X * f0 + Y * f1;
        double d1 = Y * f0 - X * f1;
        double d2 = Z;
        return new Vec3(d0, d1, d2);
    }

    public Vec3 RotateClockwise90()
    {
        return new Vec3(-Z, Y, X);
    }

    public static Vec3 DirectionFromRotation(Vec2 vec2)
    {
        return DirectionFromRotation(vec2.X, vec2.Y);
    }

    public static Vec3 DirectionFromRotation(float x, float y)
    {
        float f0 = Mth.cos(-y * (float)(Math.PI / 180.0) - (float)Math.PI);
        float f1 = Mth.sin(-y * (float)(Math.PI / 180.0) - (float)Math.PI);
        float f2 = -Mth.cos(-x * (float)(Math.PI / 180.0));
        float f3 = Mth.sin(-x * (float)(Math.PI / 180.0));
        return new Vec3(f1 * f2, f3, f0 * f2);
    }

    public Vec3 Align(AxisType axisTypes)
    {
        double d0 = (axisTypes | AxisType.X) == AxisType.None ? X : Mth.floor(X);
        double d1 = (axisTypes | AxisType.Y) == AxisType.None ? Y : Mth.floor(Y);
        double d2 = (axisTypes | AxisType.Z) == AxisType.None ? Z : Mth.floor(Z);
        return new Vec3(d0, d1, d2);
    }

    //public double get(AxisType axisType)
    //{
    //    return axis.choose(X, Y, Z);
    //}

    public Vec3 With(AxisType axisType, double offset)
    {
        double d0 = axisType == AxisType.X ? offset : X;
        double d1 = axisType == AxisType.Y ? offset : Y;
        double d2 = axisType == AxisType.Z ? offset : Z;
        return new Vec3(d0, d1, d2);
    }

    //public Vec3 Relative(DirectionType directionType, double offset)
    //{
    //    Direction direction = Direction.Directions[directionType];
    //    Vec3i vec3i = direction.GetUnitVec3i();
    //    return new Vec3(X + offset * vec3i.X, Y + offset * vec3i.Y, Z + offset * vec3i.Z);
    //}

    //public Vector3f ToVector3f()
    //{
    //    return new Vector3f((float)X, (float)Y, (float)Z);
    //}

    public Vec3 ProjectedOn(Vec3 vec3)
    {
        return vec3.LengthSqr() == 0.0 ? vec3 : vec3.Scale(Dot(vec3)).Scale(1.0 / vec3.LengthSqr());
    }
}
