using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Helpers;

internal static class StringHelper
{
    public static int JavaHashCode(string s)
    {
        int hash = 0;

        for (int i = 0; i < s.Length; i++)
        {
            hash = 31 * hash + s[i];
        }

        return hash;
    }
}
