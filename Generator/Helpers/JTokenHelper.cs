using Newtonsoft.Json.Linq;

namespace Generator.Helpers;

public static class JTokenHelper
{
    public const string Namespace = "minecraft";
    public const string NamespaceTag = "#minecraft";
    public static int NamespaceIndex { get; } = Namespace.Length + 1;
    public static int NamespaceTagIndex { get; } = NamespaceTag.Length + 1;

    public static string StringTrimNamespace(this JToken token)
    {
        if (token.Type != JTokenType.String)
            throw new NotSupportedException("Expect string for JToken");

        string value = token.Value<string>()!;
        if (!value.StartsWith(Namespace))
            throw new NotSupportedException($"Expect namespace \"{Namespace}\" in JToken value");

        return value.Substring(NamespaceIndex);
    }

    public static string StringTrimNamespaceTag(this JToken token)
    {
        if (token.Type != JTokenType.String)
            throw new NotSupportedException("Expect string for JToken");

        string value = token.Value<string>()!;
        if (!value.StartsWith(NamespaceTag))
            throw new NotSupportedException($"Expect namespace \"{NamespaceTag}\" in JToken value");

        return value.Substring(NamespaceTagIndex);
    }

    public static void AssertChildExists(this JToken token, string name)
    {
        if (token[name] == null)
            throw new NotSupportedException($"Expect child token \"{name}\" in JToken");
    }
}
