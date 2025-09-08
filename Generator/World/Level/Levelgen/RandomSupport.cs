using Generator.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Generator.World.Level.Levelgen;

//source: net.minecraft.world.level.levelgen.RandomSupport
public class RandomSupport
{
    public static readonly long GOLDEN_RATIO_64 = -7046029254386353131L;
    public static readonly long SILVER_RATIO_64 = 7640891576956012809L;
    private static readonly HashAlgorithm MD5_128 = MD5.Create();
    private static long SEED_UNIQUIFIER = 8682522807148012L;

    public static long MixStafford13(long seed)
    {
        seed = (seed ^ seed >>> 30) * -4658895280553007687L;
        seed = (seed ^ seed >>> 27) * -7723592293110705685L;
        return seed ^ seed >>> 31;
    }

    public static Seed128bit UpgradeSeedTo128bitUnmixed(long seed)
    {
        long lo = seed ^ 7640891576956012809L;
        long hi = lo + -7046029254386353131L;
        return new Seed128bit(lo, hi);
    }

    public static Seed128bit UpgradeSeedTo128bit(long seed)
    {
        return UpgradeSeedTo128bitUnmixed(seed).Mixed();
    }

    public static Seed128bit SeedFromHashOf(string seedString)
    {
        byte[] abyte = MD5_128.ComputeHash(Encoding.UTF8.GetBytes(seedString));
        long i = Longs.fromBytes(abyte[0], abyte[1], abyte[2], abyte[3], abyte[4], abyte[5], abyte[6], abyte[7]);
        long j = Longs.fromBytes(abyte[8], abyte[9], abyte[10], abyte[11], abyte[12], abyte[13], abyte[14], abyte[15]);
        return new Seed128bit(i, j);
    }

    public static long GenerateUniqueSeed()
    {
        //return SEED_UNIQUIFIER.updateAndGet(p_224601_->p_224601_ * 1181783497276652981L) ^ System.nanoTime();
        return Interlocked.Exchange<long>(ref SEED_UNIQUIFIER, SEED_UNIQUIFIER * 1181783497276652981L) ^ nanoTime();
    }

    private static long nanoTime()
    {
        return Stopwatch.GetTimestamp() * (1000000000L / Stopwatch.Frequency);
    }
}
