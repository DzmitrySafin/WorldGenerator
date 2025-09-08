using Generator.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Resources;

//source: net.minecraft.resources.ResourceLocation
[JsonConverter(typeof(ResourceLocationConverter))]
public class ResourceLocation : IComparable<ResourceLocation>
{
    public static readonly char NAMESPACE_SEPARATOR = ':';
    public static readonly string DEFAULT_NAMESPACE = "minecraft";
    public static readonly string REALMS_NAMESPACE = "realms";
    public string ResourceNamespace { get; private set; }
    public string ResourcePath { get; private set; }

    private ResourceLocation(string nameSpace, string path)
    {
        Debug.Assert(isValidNamespace(nameSpace));
        Debug.Assert(isValidPath(path));

        ResourceNamespace = nameSpace;
        ResourcePath = path;
    }

    private static ResourceLocation createUntrusted(string nameSpace, string path)
    {
        return new ResourceLocation(assertValidNamespace(nameSpace, path), assertValidPath(nameSpace, path));
    }

    public static ResourceLocation FromNamespaceAndPath(string nameSpace, string path)
    {
        return createUntrusted(nameSpace, path);
    }

    public static ResourceLocation Parse(string path)
    {
        return bySeparator(path, ':');
    }

    public static ResourceLocation WithDefaultNamespace(string path)
    {
        return new ResourceLocation(DEFAULT_NAMESPACE, assertValidPath(DEFAULT_NAMESPACE, path));
    }

    public static ResourceLocation? TryParse(string path)
    {
        return tryBySeparator(path, ':');
    }

    public static ResourceLocation? tryBuild(string nameSpace, string path)
    {
        return isValidNamespace(nameSpace) && isValidPath(path) ? new ResourceLocation(nameSpace, path) : null;
    }

    public static ResourceLocation bySeparator(string path, char separator)
    {
        int i = path.IndexOf(separator);
        if (i >= 0)
        {
            string s = path.Substring(i + 1);
            if (i != 0)
            {
                string s1 = path.Substring(0, i);
                return createUntrusted(s1, s);
            }
            else
            {
                return WithDefaultNamespace(s);
            }
        }
        else
        {
            return WithDefaultNamespace(path);
        }
    }

    public static ResourceLocation? tryBySeparator(string path, char separator)
    {
        int i = path.IndexOf(separator);
        if (i >= 0)
        {
            string s = path.Substring(i + 1);
            if (!isValidPath(s))
            {
                return null;
            }
            else if (i != 0)
            {
                string s1 = path.Substring(0, i);
                return isValidNamespace(s1) ? new ResourceLocation(s1, s) : null;
            }
            else
            {
                return new ResourceLocation(DEFAULT_NAMESPACE, s);
            }
        }
        else
        {
            return isValidPath(path) ? new ResourceLocation(DEFAULT_NAMESPACE, path) : null;
        }
    }

    //public static DataResult<ResourceLocation> read(string p_135838_)
    //{
    //    try
    //    {
    //        return DataResult.success(parse(p_135838_));
    //    }
    //    catch (ResourceLocationException resourcelocationexception)
    //    {
    //        return DataResult.error(()-> "Not a valid resource location: " + p_135838_ + " " + resourcelocationexception.getMessage());
    //    }
    //}

    public ResourceLocation withPath(string path)
    {
        return new ResourceLocation(ResourceNamespace, assertValidPath(ResourceNamespace, path));
    }

    public ResourceLocation withPath(Func<string, string> p_250342_)
    {
        return withPath(p_250342_(ResourcePath));
    }

    public ResourceLocation withPrefix(string pathPrefix)
    {
        return withPath(pathPrefix + ResourcePath);
    }

    public ResourceLocation withSuffix(string pathSuffix)
    {
        return withPath(ResourcePath + pathSuffix);
    }

    public override string ToString()
    {
        return $"{ResourceNamespace}{NAMESPACE_SEPARATOR}{ResourcePath}";
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }
        else
        {
            return !(obj is ResourceLocation resourcelocation)
                ? false
                : ResourceNamespace.Equals(resourcelocation.ResourceNamespace) && ResourcePath.Equals(resourcelocation.ResourcePath);
        }
    }

    public override int GetHashCode()
    {
        return 31 * ResourceNamespace.GetHashCode() + ResourcePath.GetHashCode();
    }

    public int CompareTo(ResourceLocation? other)
    {
        int i = ResourcePath.CompareTo(other?.ResourcePath);
        if (i == 0)
        {
            i = ResourceNamespace.CompareTo(other?.ResourceNamespace);
        }
        return i;
    }

    public string toDebugFileName()
    {
        return ToString().Replace('/', '_').Replace(':', '_');
    }

    public string toLanguageKey()
    {
        return ResourceNamespace + "." + ResourcePath;
    }

    public string toShortLanguageKey()
    {
        return ResourceNamespace.Equals(DEFAULT_NAMESPACE) ? ResourcePath : toLanguageKey();
    }

    public string toLanguageKey(string prefix)
    {
        return prefix + "." + toLanguageKey();
    }

    public string toLanguageKey(string prefix, string suffix)
    {
        return prefix + "." + toLanguageKey() + "." + suffix;
    }

    //private static string readGreedy(StringReader p_330450_)
    //{
    //    int i = p_330450_.getCursor();

    //    while (p_330450_.canRead() && isAllowedInResourceLocation(p_330450_.peek()))
    //    {
    //        p_330450_.skip();
    //    }

    //    return p_330450_.getString().Substring(i, p_330450_.getCursor());
    //}

    //public static ResourceLocation read(StringReader p_135819_) throws CommandSyntaxException
    //{
    //    int i = p_135819_.getCursor();
    //    string s = readGreedy(p_135819_);

    //    try
    //    {
    //        return parse(s);
    //    }
    //    catch (ResourceLocationException resourcelocationexception)
    //    {
    //        p_135819_.setCursor(i);
    //        throw ERROR_INVALID.createWithContext(p_135819_);
    //    }
    //}

    //public static ResourceLocation readNonEmpty(StringReader p_330926_) throws CommandSyntaxException
    //{
    //    int i = p_330926_.getCursor();
    //    string s = readGreedy(p_330926_);
    //    if (s.isEmpty())
    //    {
    //        throw ERROR_INVALID.createWithContext(p_330926_);
    //    }
    //    else
    //    {
    //        try
    //        {
    //            return parse(s);
    //        }
    //        catch (ResourceLocationException resourcelocationexception)
    //        {
    //            p_330926_.setCursor(i);
    //            throw ERROR_INVALID.createWithContext(p_330926_);
    //        }
    //    }
    //}

    public static bool isAllowedInResourceLocation(char ch)
    {
        return ch >= '0' && ch <= '9'
            || ch >= 'a' && ch <= 'z'
            || ch == '_'
            || ch == ':'
            || ch == '/'
            || ch == '.'
            || ch == '-';
    }

    public static bool isValidPath(string path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (!validPathChar(path[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool isValidNamespace(string nameSpace)
    {
        for (int i = 0; i < nameSpace.Length; i++)
        {
            if (!validNamespaceChar(nameSpace[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static string assertValidNamespace(string nameSpace, string path)
    {
        if (!isValidNamespace(nameSpace))
        {
            throw new ArgumentException("Non [a-z0-9_.-] character in namespace of location: " + nameSpace + ":" + path);
        }
        else
        {
            return nameSpace;
        }
    }

    public static bool validPathChar(char ch)
    {
        return ch == '_'
            || ch == '-'
            || ch >= 'a' && ch <= 'z'
            || ch >= '0' && ch <= '9'
            || ch == '/'
            || ch == '.';
    }

    private static bool validNamespaceChar(char ch)
    {
        return ch == '_' || ch == '-' || ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9' || ch == '.';
    }

    private static string assertValidPath(string nameSpace, string path)
    {
        if (!isValidPath(path))
        {
            throw new ArgumentException("Non [a-z0-9/._-] character in path of location: " + nameSpace + ":" + path);
        }
        else
        {
            return path;
        }
    }
}
