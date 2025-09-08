using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Helpers;

public static class IntegerHelper
{
    public static long JavaUnsignedLong(int n)
    {
        return n < 0 ? (long)Convert.ToUInt64(n + ((long)1 << 32)) : (long)Convert.ToUInt64(n);
    }

    public static int JavaUnsignedInt(byte x)
    {
        return x & 0xFF;
    }
}
