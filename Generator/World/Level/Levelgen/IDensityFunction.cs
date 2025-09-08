using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.DensityFunction
public interface IDensityFunction
{
    double Compute(IFunctionContext context);

    void FillArray(double[] array, IFunctionContextProvider contextProvider);

    IDensityFunction MapAll(IDensityVisitor densityVisitor);

    double MinValue { get; }

    double MaxValue { get; }

    //IDensityFunction clamp(double p_208221_, double p_208222_)
    //{
    //    return new DensityFunctions.Clamp(this, p_208221_, p_208222_);
    //}

    //IDensityFunction abs()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.ABS);
    //}

    //IDensityFunction square()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.SQUARE);
    //}

    //IDensityFunction cube()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.CUBE);
    //}

    //IDensityFunction halfNegative()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.HALF_NEGATIVE);
    //}

    //IDensityFunction quarterNegative()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.QUARTER_NEGATIVE);
    //}

    //IDensityFunction squeeze()
    //{
    //    return DensityFunctions.map(this, DensityFunctions.Mapped.Type.SQUEEZE);
    //}
}
