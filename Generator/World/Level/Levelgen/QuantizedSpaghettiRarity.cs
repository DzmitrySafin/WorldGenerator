using Generator.Enums;
using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.NoiseRouterData.QuantizedSpaghettiRarity
[JsonConverter(typeof(SpaghettiRarityConverter))]
public class QuantizedSpaghettiRarity
{
    public RarityValueType RarityType { get; set; }

    public double MaxRarity => RarityType == RarityValueType.TYPE1 ? 2.0 : 3.0;

    public Func<double, double> GetSphaghettiRarity => RarityType == RarityValueType.TYPE1 ? getSpaghettiRarity3D : getSphaghettiRarity2D;

    private static double getSphaghettiRarity2D(double p_209564_)
    {
        if (p_209564_ < -0.75)
        {
            return 0.5;
        }
        else if (p_209564_ < -0.5)
        {
            return 0.75;
        }
        else if (p_209564_ < 0.5)
        {
            return 1.0;
        }
        else
        {
            return p_209564_ < 0.75 ? 2.0 : 3.0;
        }
    }

    private static double getSpaghettiRarity3D(double p_209566_)
    {
        if (p_209566_ < -0.5)
        {
            return 0.75;
        }
        else if (p_209566_ < 0.0)
        {
            return 1.0;
        }
        else
        {
            return p_209566_ < 0.5 ? 1.5 : 2.0;
        }
    }
}
