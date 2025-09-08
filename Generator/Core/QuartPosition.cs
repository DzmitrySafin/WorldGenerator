using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.QuartPos
public class QuartPosition
{
    public static readonly int BITS = 2;
    public static readonly int SIZE = 4;
    public static readonly int MASK = 3;
    private static readonly int SECTION_TO_QUARTS_BITS = 2;

    public static int FromBlock(int coordinate)
    {
        return coordinate >> 2;
    }

    public static int QuartLocal(int coordinate)
    {
        return coordinate & MASK;
    }

    public static int ToBlock(int coordinate)
    {
        return coordinate << 2;
    }

    public static int FromSection(int coordinate)
    {
        return coordinate << SECTION_TO_QUARTS_BITS;
    }

    public static int ToSection(int coordinate)
    {
        return coordinate >> SECTION_TO_QUARTS_BITS;
    }
}
