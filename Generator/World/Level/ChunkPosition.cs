using Generator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level;

//source: net.minecraft.world.level.ChunkPos
public class ChunkPosition
{
    //private static readonly int SAFETY_MARGIN = 1056;
    public static readonly long INVALID_CHUNK_POS = AsLong(1875066, 1875066);
    //private static readonly int SAFETY_MARGIN_CHUNKS = (32 + ChunkPyramid.GENERATION_PYRAMID.getStepTo(ChunkStatus.FULL).accumulatedDependencies().size() + 1) * 2;
    //public static readonly int MAX_COORDINATE_VALUE = SectionPosition.BlockToSectionCoord(BlockPosition.MAX_HORIZONTAL_COORDINATE) - SAFETY_MARGIN_CHUNKS;
    public static readonly ChunkPosition ZERO = new ChunkPosition(0, 0);
    //private static readonly long COORD_BITS = 32L;
    private static readonly long COORD_MASK = 4294967295L;
    private static readonly int REGION_BITS = 5;
    public static readonly int REGION_SIZE = 32;
    private static readonly int REGION_MASK = 31;
    public static readonly int REGION_MAX_INDEX = 31;
    private static readonly int HASH_A = 1664525;
    private static readonly int HASH_C = 1013904223;
    private static readonly int HASH_Z_XOR = -559038737;

    public int X { get; private set; }
    public int Z { get; private set; }

    public ChunkPosition(int x, int z)
    {
        X = x;
        Z = z;
    }

    public ChunkPosition(BlockPosition blockPos)
        : this(SectionPosition.BlockToSectionCoord(blockPos.X), SectionPosition.BlockToSectionCoord(blockPos.Z))
    {
    }

    public ChunkPosition(long coordinate)
        : this((int)coordinate, (int)coordinate >> 32)
    {
    }

    public static ChunkPosition MinFromRegion(int x, int y)
    {
        return new ChunkPosition(x << REGION_BITS, y << REGION_BITS);
    }

    public static ChunkPosition MaxFromRegion(int x, int y)
    {
        return new ChunkPosition((x << REGION_BITS) + REGION_MAX_INDEX, (y << REGION_BITS) + REGION_MAX_INDEX);
    }

    public long ToLong()
    {
        return AsLong(X, Z);
    }

    public static long AsLong(int x, int z)
    {
        return x & COORD_MASK | (z & COORD_MASK) << 32;
    }

    public static long AsLong(BlockPosition blockPos)
    {
        return AsLong(SectionPosition.BlockToSectionCoord(blockPos.X), SectionPosition.BlockToSectionCoord(blockPos.Z));
    }

    public static int GetX(long coordinate)
    {
        return (int)(coordinate & COORD_MASK);
    }

    public static int GetZ(long coordinate)
    {
        return (int)(coordinate >>> 32 & COORD_MASK);
    }

    public override int GetHashCode()
    {
        int i = HASH_A * X + HASH_C;
        int j = HASH_A * (Z ^ HASH_Z_XOR) + HASH_C;
        return i ^ j;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }
        else
        {
            return obj is ChunkPosition chunkpos && X == chunkpos.X && Z == chunkpos.Z;
        }
    }

    public int GetMiddleBlockX()
    {
        return GetBlockX(8);
    }

    public int GetMiddleBlockZ()
    {
        return GetBlockZ(8);
    }

    public int GetMinBlockX()
    {
        return SectionPosition.SectionToBlockCoord(X);
    }

    public int GetMinBlockZ()
    {
        return SectionPosition.SectionToBlockCoord(Z);
    }

    public int GetMaxBlockX()
    {
        return GetBlockX(15);
    }

    public int GetMaxBlockZ()
    {
        return GetBlockZ(15);
    }

    public int GetRegionX()
    {
        return X >> REGION_BITS;
    }

    public int GetRegionZ()
    {
        return Z >> REGION_BITS;
    }

    public int GetRegionLocalX()
    {
        return X & REGION_MASK;
    }

    public int GetRegionLocalZ()
    {
        return Z & REGION_MASK;
    }

    public BlockPosition GetBlockAt(int x, int y, int z)
    {
        return new BlockPosition(GetBlockX(x), y, GetBlockZ(z));
    }

    public int GetBlockX(int coordinate)
    {
        return SectionPosition.SectionToBlockCoord(X, coordinate);
    }

    public int GetBlockZ(int coordinate)
    {
        return SectionPosition.SectionToBlockCoord(Z, coordinate);
    }

    public BlockPosition GetMiddleBlockPosition(int y)
    {
        return new BlockPosition(GetMiddleBlockX(), y, GetMiddleBlockZ());
    }

    public override string ToString()
    {
        return $"[{X}, {Z}]";
    }

    public BlockPosition GetWorldPosition()
    {
        return new BlockPosition(GetMinBlockX(), 0, GetMinBlockZ());
    }

    public int GetChessboardDistance(ChunkPosition chunkPos)
    {
        return GetChessboardDistance(chunkPos.X, chunkPos.Z);
    }

    public int GetChessboardDistance(int x, int z)
    {
        return Math.Max(Math.Abs(X - x), Math.Abs(Z - z));
    }

    public int DistanceSquared(ChunkPosition chunkPos)
    {
        return DistanceSquared(chunkPos.X, chunkPos.Z);
    }

    public int DistanceSquared(long coordinate)
    {
        return DistanceSquared(GetX(coordinate), GetZ(coordinate));
    }

    private int DistanceSquared(int x, int z)
    {
        int i = x - X;
        int j = z - Z;
        return i * i + j * j;
    }
}
