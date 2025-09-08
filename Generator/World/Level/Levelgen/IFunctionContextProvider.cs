using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction.ContextProvider
public interface IFunctionContextProvider
{
    IFunctionContext ForIndex(int index);

    void FillAllDirectly(double[] p_208236_, IDensityFunction densityFunction);
}
