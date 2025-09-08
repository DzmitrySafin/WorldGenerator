using Generator.Enums;
using Generator.World.Phys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.Direction
public class Direction
{
    public DirectionType DataDirection { get; private set; }

    public int Data3d { get; private set; }
    public int Data2d { get; private set; }
    public int OppositeIndex { get; private set; }
    public string DirectionName { get; private set; }

    public AxisType DataAxis { get; private set; }
    public AxisDirectionType DataAxisDirection { get; private set; }

    private Vec3i normal;
    //private Vec3 normalVec3;
    //private Vector3fc normalVec3f;

    public static readonly List<Direction> Values = new()
    {
        new Direction(DirectionType.DOWN, 0, 1, -1, "down", AxisDirectionType.NEGATIVE, AxisType.Y, new Vec3i(0, -1, 0)),
        new Direction(DirectionType.UP, 1, 0, -1, "up", AxisDirectionType.POSITIVE, AxisType.Y, new Vec3i(0, 1, 0)),
        new Direction(DirectionType.NORTH, 2, 3, 2, "north", AxisDirectionType.NEGATIVE, AxisType.Z, new Vec3i(0, 0, -1)),
        new Direction(DirectionType.SOUTH, 3, 2, 0, "south", AxisDirectionType.POSITIVE, AxisType.Z, new Vec3i(0, 0, 1)),
        new Direction(DirectionType.WEST, 4, 5, 1, "west", AxisDirectionType.NEGATIVE, AxisType.X, new Vec3i(-1, 0, 0)),
        new Direction(DirectionType.EAST, 5, 4, 3, "east", AxisDirectionType.POSITIVE, AxisType.X, new Vec3i(1, 0, 0))
    };

    public static readonly Dictionary<DirectionType, Direction> Directions = Values.ToDictionary(d => d.DataDirection, d => d);

    private Direction(
        DirectionType directionType,
        int data3d,
        int oppositeIndex,
        int data2d,
        string name,
        AxisDirectionType axisDirection,
        AxisType axis,
        Vec3i normal)
    {
        DataDirection = directionType;
        Data3d = data3d;
        Data2d = data2d;
        OppositeIndex = oppositeIndex;
        DirectionName = name;
        DataAxis = axis;
        DataAxisDirection = axisDirection;
        this.normal = normal;
        //normalVec3 = Vec3.AtLowerCornerOf(normal);
        //this.normalVec3f = new Vector3f(normal.X, normal.Y, normal.Z);
    }

    public int StepX => normal.X;
    public int StepY => normal.Y;
    public int StepZ => normal.Z;

    public static DirectionType FromAxisAndDirection(AxisType axis, AxisDirectionType axisDirection)
    {
        return axis switch
        {
            AxisType.X => axisDirection == AxisDirectionType.POSITIVE ? DirectionType.EAST : DirectionType.WEST,
            AxisType.Y => axisDirection == AxisDirectionType.POSITIVE ? DirectionType.UP : DirectionType.DOWN,
            AxisType.Z => axisDirection == AxisDirectionType.POSITIVE ? DirectionType.SOUTH : DirectionType.NORTH,
            _ => throw new NotImplementedException()
        };
    }

    public static float GetYRot(DirectionType direction)
    {
        return direction switch
        {
            DirectionType.NORTH => 180.0F,
            DirectionType.SOUTH => 0.0F,
            DirectionType.WEST => 90.0F,
            DirectionType.EAST => -90.0F,
            _ => throw new ArgumentException("No y-Rot for vertical axis: " + direction)
        };
    }

    public float ToYRot()
    {
        return (Data2d & 3) * 90.0F;
    }

    public Quaternion GetRotation()
    {
        return DataDirection switch
        {
            DirectionType.DOWN => Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathF.PI),
            DirectionType.UP => Quaternion.Identity,
            DirectionType.NORTH => Quaternion.CreateFromYawPitchRoll(0.0F, MathF.PI / 2, MathF.PI),
            DirectionType.SOUTH => Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathF.PI / 2),
            DirectionType.WEST => Quaternion.CreateFromYawPitchRoll(0.0F, MathF.PI / 2, MathF.PI / 2),
            DirectionType.EAST => Quaternion.CreateFromYawPitchRoll(0.0F, MathF.PI / 2, -MathF.PI / 2),
            _ => throw new NotImplementedException()
        };
    }

    public DirectionType GetClockWise(AxisType axis)
    {
        return axis switch
        {
            AxisType.X => DataDirection != DirectionType.WEST && DataDirection != DirectionType.EAST ? getClockWiseX() : DataDirection,
            AxisType.Y => DataDirection != DirectionType.UP && DataDirection != DirectionType.DOWN ? GetClockWise() : DataDirection,
            AxisType.Z => DataDirection != DirectionType.NORTH && DataDirection != DirectionType.SOUTH ? getClockWiseZ() : DataDirection,
            _ => throw new NotImplementedException()
        };
    }

    public DirectionType GetCounterClockWise(AxisType axis)
    {
        return axis switch
        {
            AxisType.X => DataDirection != DirectionType.WEST && DataDirection != DirectionType.EAST ? getCounterClockWiseX() : DataDirection,
            AxisType.Y => DataDirection != DirectionType.UP && DataDirection != DirectionType.DOWN ? GetCounterClockWise() : DataDirection,
            AxisType.Z => DataDirection != DirectionType.NORTH && DataDirection != DirectionType.SOUTH ? getCounterClockWiseZ() : DataDirection,
            _ => throw new NotImplementedException()
        };
    }

    public DirectionType GetClockWise()
    {
        return DataDirection switch
        {
            DirectionType.NORTH => DirectionType.EAST,
            DirectionType.SOUTH => DirectionType.WEST,
            DirectionType.WEST => DirectionType.NORTH,
            DirectionType.EAST => DirectionType.SOUTH,
            _ => throw new ArgumentException("Unable to get Y-rotated facing of " + DataDirection)
        };
    }

    public DirectionType GetCounterClockWise()
    {
        return DataDirection switch
        {
            DirectionType.NORTH => DirectionType.WEST,
            DirectionType.SOUTH => DirectionType.EAST,
            DirectionType.WEST => DirectionType.SOUTH,
            DirectionType.EAST => DirectionType.NORTH,
            _ => throw new ArgumentException("Unable to get CCW facing of " + DataDirection)
        };
    }

    private DirectionType getClockWiseX()
    {
        return DataDirection switch
        {
            DirectionType.DOWN => DirectionType.SOUTH,
            DirectionType.UP => DirectionType.NORTH,
            DirectionType.NORTH => DirectionType.DOWN,
            DirectionType.SOUTH => DirectionType.UP,
            _ => throw new ArgumentException("Unable to get X-rotated facing of " + DataDirection)
        };
    }

    private DirectionType getCounterClockWiseX()
    {
        return DataDirection switch
        {
            DirectionType.DOWN => DirectionType.NORTH,
            DirectionType.UP => DirectionType.SOUTH,
            DirectionType.NORTH => DirectionType.UP,
            DirectionType.SOUTH => DirectionType.DOWN,
            _ => throw new ArgumentException("Unable to get X-rotated facing of " + DataDirection)
        };
    }

    private DirectionType getClockWiseZ()
    {
        return DataDirection switch
        {
            DirectionType.DOWN => DirectionType.WEST,
            DirectionType.UP => DirectionType.EAST,
            DirectionType.WEST => DirectionType.UP,
            DirectionType.EAST => DirectionType.DOWN,
            _ => throw new ArgumentException("Unable to get Z-rotated facing of " + DataDirection)
        };
    }

    private DirectionType getCounterClockWiseZ()
    {
        return DataDirection switch
        {
            DirectionType.DOWN => DirectionType.EAST,
            DirectionType.UP => DirectionType.WEST,
            DirectionType.WEST => DirectionType.DOWN,
            DirectionType.EAST => DirectionType.UP,
            _ => throw new ArgumentException("Unable to get Z-rotated facing of " + DataDirection)
        };
    }
}
