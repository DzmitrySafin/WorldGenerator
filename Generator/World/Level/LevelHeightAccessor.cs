using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level;

//source: net.minecraft.world.level.LevelHeightAccessor
public class LevelHeightAccessor : ILevelHeightAccessor
{
    private int height;
    private int minY;

    public LevelHeightAccessor(int minY, int height)
    {
        this.minY = minY;
        this.height = height;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetMinY()
    {
        return minY;
    }

    public static LevelHeightAccessor Create(int minY, int height)
    {
        return new LevelHeightAccessor(minY, height);
    }
}
