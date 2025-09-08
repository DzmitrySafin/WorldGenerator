using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Enums;

//source: net.minecraft.core.Direction.Axis
[Flags]
public enum AxisType
{
    None = 0,

    X = 1 << 0,
    Y = 1 << 1,
    Z = 1 << 2,

    All = X | Y | Z
}
