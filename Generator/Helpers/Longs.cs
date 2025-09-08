using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Generator.Helpers;

//source: com.google.common.primitives.Longs
internal static class Longs
{
    public static int BYTES = 8;
    public static long MAX_POWER_OF_TWO = 4611686018427387904L;

    public static int hashCode(long value)
    {
        return (int)(value ^ value >>> 32);
    }

    public static int compare(long a, long b)
    {
        return a.CompareTo(b);
    }

    public static bool contains(long[] array, long target)
    {
        foreach (long value in array)
        {
            if (value == target)
            {
                return true;
            }
        }

        return false;
    }

    public static int indexOf(long[] array, long target)
    {
        return indexOf(array, target, 0, array.Length);
    }

    private static int indexOf(long[] array, long target, int start, int end)
    {
        for (int i = start; i < end; ++i)
        {
            if (array[i] == target)
            {
                return i;
            }
        }

        return -1;
    }

    public static int indexOf([DisallowNull] long[] array, [DisallowNull] long[] target)
    {
        if (target.Length == 0)
        {
            return 0;
        }
        else
        {
            for (int i = 0; i < array.Length - target.Length + 1; ++i)
            {
                bool skip = false;
                for (int j = 0; j < target.Length; ++j)
                {
                    if (array[i + j] != target[j])
                    {
                        skip = true;
                        break;
                    }
                }
                if (skip)
                {
                    continue;
                }

                return i;
            }

            return -1;
        }
    }

    public static int lastIndexOf(long[] array, long target)
    {
        return lastIndexOf(array, target, 0, array.Length);
    }

    private static int lastIndexOf(long[] array, long target, int start, int end)
    {
        for (int i = end - 1; i >= start; --i)
        {
            if (array[i] == target)
            {
                return i;
            }
        }

        return -1;
    }

    public static long min(params long[] array)
    {
        if (array.Length == 0)
        {
            throw new ArgumentException("Array must not be empty", nameof(array));
        }

        long min = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            if (array[i] < min)
            {
                min = array[i];
            }
        }

        return min;
    }

    public static long max(params long[] array)
    {
        if (array.Length == 0)
        {
            throw new ArgumentException("Array must not be empty", nameof(array));
        }

        long max = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }

        return max;
    }

    public static long constrainToRange(long value, long min, long max)
    {
        if (min > max)
        {
            throw new ArgumentException(string.Format("min (%s) must be less than or equal to max (%s)", min, max));
        }

        return Math.Min(Math.Max(value, min), max);
    }

    public static long[] concat(params long[][] arrays)
    {
        long length = 0L;

        foreach (long[] array in arrays)
        {
            length += (long)array.Length;
        }

        long[] result = new long[checkNoOverflow(length)];
        int pos = 0;

        foreach (long[] array in arrays)
        {
            Array.Copy(array, 0, result, pos, array.Length);
            pos += array.Length;
        }

        return result;
    }

    private static int checkNoOverflow(long result)
    {
        if (result != (long)((int)result))
        {
            throw new ArgumentException(string.Format("the total number of elements (%s) in the arrays must fit in an int", result));
        }

        return (int)result;
    }

    public static byte[] toByteArray(long value)
    {
        byte[] result = new byte[8];

        for (int i = 7; i >= 0; --i)
        {
            result[i] = (byte)((int)(value & 255L));
            value >>= 8;
        }

        return result;
    }

    public static long fromByteArray(byte[] bytes)
    {
        if (bytes.Length < 8)
        {
            throw new ArgumentException(string.Format("array too small: %s < %s", bytes.Length, 8));
        }

        return fromBytes(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]);
    }

    public static long fromBytes(byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8)
    {
        return ((long)b1 & 255L) << 56 | ((long)b2 & 255L) << 48 | ((long)b3 & 255L) << 40 | ((long)b4 & 255L) << 32 | ((long)b5 & 255L) << 24 | ((long)b6 & 255L) << 16 | ((long)b7 & 255L) << 8 | (long)b8 & 255L;
    }

    public static long? tryParse(string str)
    {
        return tryParse(str, 10);
    }

    public static long? tryParse([DisallowNull] string str, int radix)
    {
        if (str.Length == 0)
        {
            return null;
        }
        else if (radix >= 2 && radix <= 36)
        {
            bool negative = str[0] == '-';
            int index = negative ? 1 : 0;
            if (index == str.Length)
            {
                return null;
            }
            else
            {
                int digit = Longs.AsciiDigits.digit(str[index++]);
                if (digit >= 0 && digit < radix)
                {
                    long accum = (long)(-digit);

                    for (long cap = long.MinValue / (long)radix; index < str.Length; accum -= (long)digit)
                    {
                        digit = Longs.AsciiDigits.digit(str[index++]);
                        if (digit < 0 || digit >= radix || accum < cap)
                        {
                            return null;
                        }

                        accum *= (long)radix;
                        if (accum < long.MinValue + (long)digit)
                        {
                            return null;
                        }
                    }

                    if (negative)
                    {
                        return accum;
                    }
                    else if (accum == long.MinValue)
                    {
                        return null;
                    }
                    else
                    {
                        return -accum;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        else
        {
            throw new ArgumentException("radix must be between MIN_RADIX and MAX_RADIX but was " + radix);
        }
    }

    //public static Converter<string, long> stringConverter()
    //{
    //    return Longs.LongConverter.INSTANCE;
    //}

    public static long[] ensureCapacity(long[] array, int minLength, int padding)
    {
        if (minLength < 0)
        {
            throw new ArgumentException(string.Format("Invalid minLength: %s", minLength));
        }
        if (padding < 0)
        {
            throw new ArgumentException(string.Format("Invalid padding: %s", padding));
        }

        //return array.Length < minLength ? Arrays.copyOf(array, minLength + padding) : array;
        if (array.Length < minLength)
        {
            long[] expanded = new long[minLength + padding];
            array.CopyTo(expanded, 0);
            return expanded;
        }
        else
        {
            return array;
        }
    }

    public static string join([DisallowNull] string separator, params long[] array)
    {
        if (array.Length == 0)
        {
            return "";
        }
        else
        {
            StringBuilder builder = new StringBuilder(array.Length * 10);
            builder.Append(array[0]);

            for (int i = 1; i < array.Length; ++i)
            {
                builder.Append(separator).Append(array[i]);
            }

            return builder.ToString();
        }
    }

    //public static Comparator<long[]> lexicographicalComparator()
    //{
    //    return Longs.LexicographicalComparator.INSTANCE;
    //}

    public static void sortDescending([DisallowNull] long[] array)
    {
        sortDescending(array, 0, array.Length);
    }

    public static void sortDescending([DisallowNull] long[] array, int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex < 0 || fromIndex >= array.Length || toIndex >= array.Length || toIndex < fromIndex)
        {
            throw new ArgumentOutOfRangeException(string.Format("fromIndex (%s) or toIndex (%s) out of bounds for array of length %s", fromIndex, toIndex, array.Length));
        }

        Array.Sort(array, fromIndex, toIndex - fromIndex);
        reverse(array, fromIndex, toIndex);
    }

    public static void reverse([DisallowNull] long[] array)
    {
        reverse(array, 0, array.Length);
    }

    public static void reverse([DisallowNull] long[] array, int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex < 0 || fromIndex >= array.Length || toIndex >= array.Length || toIndex < fromIndex)
        {
            throw new ArgumentOutOfRangeException(string.Format("fromIndex (%s) or toIndex (%s) out of bounds for array of length %s", fromIndex, toIndex, array.Length));
        }

        int i = fromIndex;
        for (int j = toIndex - 1; i < j; --j)
        {
            long tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
            ++i;
        }
    }

    public static void rotate(long[] array, int distance)
    {
        rotate(array, distance, 0, array.Length);
    }

    public static void rotate([DisallowNull] long[] array, int distance, int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex < 0 || fromIndex >= array.Length || toIndex >= array.Length || toIndex < fromIndex)
        {
            throw new ArgumentOutOfRangeException(string.Format("fromIndex (%s) or toIndex (%s) out of bounds for array of length %s", fromIndex, toIndex, array.Length));
        }

        if (array.Length > 1)
        {
            int length = toIndex - fromIndex;
            int m = -distance % length;
            m = m < 0 ? m + length : m;
            int newFirstIndex = m + fromIndex;
            if (newFirstIndex != fromIndex)
            {
                reverse(array, fromIndex, newFirstIndex);
                reverse(array, newFirstIndex, toIndex);
                reverse(array, fromIndex, toIndex);
            }
        }
    }

    //public static long[] toArray(Collection<? extends Number> collection)
    //{
    //    if (collection instanceof LongArrayAsList) {
    //        return ((LongArrayAsList)collection).toLongArray();
    //    }
    //    else
    //    {
    //        object[] boxedArray = collection.toArray();
    //        int len = boxedArray.Length;
    //        long[] array = new long[len];

    //        for (int i = 0; i < len; ++i)
    //        {
    //            array[i] = ((Number)Preconditions.checkNotNull(boxedArray[i])).longValue();
    //        }

    //        return array;
    //    }
    //}

    //public static List<long> asList(params long[] backingArray)
    //{
    //    return (List<long>)(backingArray.Length == 0 ? Collections.emptyList() : new LongArrayAsList(backingArray));
    //}

    static class AsciiDigits
    {
        private static sbyte[] asciiDigits;

        public static int digit(char c)
        {
            return c < 128 ? asciiDigits[c] : -1;
        }

        static AsciiDigits()
        {
            sbyte[] result = new sbyte[128];
            Array.Fill<sbyte>(result, -1);

            for (int i = 0; i < 10; ++i)
            {
                result[48 + i] = (sbyte) i;
            }

            for (int i = 0; i < 26; ++i)
            {
                result[65 + i] = (sbyte) (10 + i);
                result[97 + i] = (sbyte) (10 + i);
            }

            asciiDigits = result;
        }
    }
}
