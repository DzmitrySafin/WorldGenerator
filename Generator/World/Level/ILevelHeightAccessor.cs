using Generator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level;

//source: net.minecraft.world.level.LevelHeightAccessor
public interface ILevelHeightAccessor
{
    int GetHeight();

    int GetMinY();

    int GetMaxY()
    {
        return GetMinY() + GetHeight() - 1;
    }

    int GetSectionsCount()
    {
        return GetMaxSectionY() - GetMinSectionY() + 1;
    }

    int GetMinSectionY()
    {
        return SectionPosition.BlockToSectionCoord(GetMinY());
    }

    int GetMaxSectionY()
    {
        return SectionPosition.BlockToSectionCoord(GetMaxY());
    }

    bool IsInsideBuildHeight(int y)
    {
        return y >= GetMinY() && y <= GetMaxY();
    }

    bool IsOutsideBuildHeight(BlockPosition blockPos)
    {
        return IsOutsideBuildHeight(blockPos.Y);
    }

    bool IsOutsideBuildHeight(int y)
    {
        return y < GetMinY() || y > GetMaxY();
    }

    int GetSectionIndex(int blockY)
    {
        return GetSectionIndexFromSectionY(SectionPosition.BlockToSectionCoord(blockY));
    }

    int GetSectionIndexFromSectionY(int sectionY)
    {
        return sectionY - GetMinSectionY();
    }

    int GetSectionYFromSectionIndex(int sectionIndex)
    {
        return sectionIndex + GetMinSectionY();
    }
}
