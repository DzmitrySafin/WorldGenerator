using Generator.Enums;
using Generator.Util;
using System.Reflection;
using System.Runtime.Serialization;

namespace Generator.Helpers;

public static class EnumHelper
{
    public static string GetEnumMemberValue<T>(this T enumVal) where T : struct, Enum
    {
        MemberInfo[] memberInfo = typeof(T).GetMember(enumVal.ToString());
        EnumMemberAttribute? attribute = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>(false);

        return attribute?.Value ?? enumVal.ToString();
    }

    public static T GetEnumFromMemberValue<T>(string enumMember) where T : struct, Enum
    {
        foreach (T enumVal in Enum.GetValues(typeof(T)))
        {
            if (enumVal.GetEnumMemberValue() == enumMember)
            {
                return enumVal;
            }
        }

        return default;
    }

    public static int Evaluate(this RandomSpreadType spreadType, IRandomSource randomSource, int bound)
    {
        return spreadType switch
        {
            RandomSpreadType.LINEAR => randomSource.NextInt(bound),
            RandomSpreadType.TRIANGULAR => (randomSource.NextInt(bound) + randomSource.NextInt(bound)) / 2,
            _ => throw new NotImplementedException()
        };
    }
}
