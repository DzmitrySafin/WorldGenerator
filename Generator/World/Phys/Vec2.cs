using Generator.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Phys;

//source: net.minecraft.world.phys.Vec2
public class Vec2
{
    public float X { get; private set; }
    public float Y { get; private set; }

    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static readonly Vec2 ZERO = new Vec2(0.0F, 0.0F);
    public static readonly Vec2 ONE = new Vec2(1.0F, 1.0F);
    public static readonly Vec2 UNIT_X = new Vec2(1.0F, 0.0F);
    public static readonly Vec2 NEG_UNIT_X = new Vec2(-1.0F, 0.0F);
    public static readonly Vec2 UNIT_Y = new Vec2(0.0F, 1.0F);
    public static readonly Vec2 NEG_UNIT_Y = new Vec2(0.0F, -1.0F);
    public static readonly Vec2 MAX = new Vec2(float.MaxValue, float.MaxValue);
    public static readonly Vec2 MIN = new Vec2(float.MinValue, float.MinValue);

    public Vec2 Scale(float factor)
    {
        return new Vec2(X * factor, Y * factor);
    }

    public float Dot(Vec2 vec2)
    {
        return X * vec2.X + Y * vec2.Y;
    }

    public Vec2 Add(Vec2 vec2)
    {
        return new Vec2(X + vec2.X, Y + vec2.Y);
    }

    public Vec2 Add(float offset)
    {
        return new Vec2(X + offset, Y + offset);
    }

    public bool Equals(Vec2 vec2)
    {
        return X == vec2.X && Y == vec2.Y;
    }

    public Vec2 Normalized()
    {
        float f = Mth.sqrt(X * X + Y * Y);
        return f < 1.0E-4F ? ZERO : new Vec2(X / f, Y / f);
    }

    public float Length()
    {
        return Mth.sqrt(X * X + Y * Y);
    }

    public float LengthSquared()
    {
        return X * X + Y * Y;
    }

    public float DistanceToSqr(Vec2 vec2)
    {
        float f0 = vec2.X - X;
        float f1 = vec2.Y - Y;
        return f0 * f0 + f1 * f1;
    }

    public Vec2 Negated()
    {
        return new Vec2(-X, -Y);
    }
}
