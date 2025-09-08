using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Biome;

//source: net.minecraft.world.level.biome.Climate.Parameter
[JsonConverter(typeof(ClimateParameterConverter))]
public class ClimateParameter
{
    public long MinValue { get; private set; }
    public long MaxValue { get; private set; }

    public ClimateParameter(float min, float max)
    {
        MinValue = Climate.QuantizeCoord(min);
        MaxValue = Climate.QuantizeCoord(max);
    }

    public static ClimateParameter Point(float p_186821_)
    {
        return Span(p_186821_, p_186821_);
    }

    public static ClimateParameter Span(float p_186823_, float p_186824_)
    {
        if (p_186823_ > p_186824_)
        {
            throw new ArgumentException("min > max: " + p_186823_ + " " + p_186824_);
        }
        else
        {
            return new ClimateParameter(Climate.QuantizeCoord(p_186823_), Climate.QuantizeCoord(p_186824_));
        }
    }

    public static ClimateParameter Span(ClimateParameter p_186830_, ClimateParameter p_186831_)
    {
        if (p_186830_.MinValue > p_186831_.MaxValue)
        {
            throw new ArgumentException("min > max: " + p_186830_ + " " + p_186831_);
        }
        else
        {
            return new ClimateParameter(p_186830_.MinValue, p_186831_.MaxValue);
        }
    }

    public override string ToString()
    {
        return MinValue == MaxValue
            ? string.Format(CultureInfo.InvariantCulture, "%d", MinValue)
            : string.Format(CultureInfo.InvariantCulture, "[%d-%d]", MinValue, MaxValue);
    }

    public long Distance(long p_186826_)
    {
        long i = p_186826_ - MaxValue;
        long j = MinValue - p_186826_;
        return i > 0L ? i : Math.Max(j, 0L);
    }

    public long Distance(ClimateParameter p_186828_)
    {
        long i = p_186828_.MinValue - MaxValue;
        long j = MinValue - p_186828_.MaxValue;
        return i > 0L ? i : Math.Max(j, 0L);
    }

    public ClimateParameter Span([AllowNull] ClimateParameter p_186837_)
    {
        return p_186837_ == null
            ? this
            : new ClimateParameter(Math.Min(MinValue, p_186837_.MinValue), Math.Max(MaxValue, p_186837_.MaxValue));
    }
}
