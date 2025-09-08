using Generator.World.Level.Levelgen.Density;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.RandomState -> RandomState -> DensityFunction.Visitor
public class VisitorWiringHelper : IDensityVisitor
{
    private readonly Dictionary<IDensityFunction, IDensityFunction> wrapped = new Dictionary<IDensityFunction, IDensityFunction>();

    private IDensityFunction wrapNew(IDensityFunction densityFunction)
    {
        //if (densityFunction is DensityFunctions.HolderHolder densityfunctions$holderholder)
        //{
        //    return densityfunctions$holderholder.function().value();
        //}

        return densityFunction is MarkerFunction markerFunction ? markerFunction.InputFunction : densityFunction;
    }

    public IDensityFunction Apply(IDensityFunction densityFunction)
    {
        if (wrapped.ContainsKey(densityFunction))
        {
            return wrapped[densityFunction];
        }

        var density = wrapNew(densityFunction);
        wrapped.Add(densityFunction, density);
        return density;
    }
}
