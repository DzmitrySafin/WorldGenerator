using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Core;

//source: net.minecraft.core.Position
public interface IPosition
{
    double X { get; }

    double Y { get; }

    double Z { get; }
}
