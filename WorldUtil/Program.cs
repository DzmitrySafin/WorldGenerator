using Generator.Core;
using Generator.Enums;
using Generator.Helpers;
using Generator.Json;
using Generator.World.Level;
using Generator.World.Level.Biome;
using Generator.World.Level.Dimension;
using Generator.World.Level.Levelgen;
using Generator.World.Level.Levelgen.Structure;
using Generator.World.Level.Levelgen.Synth;
using Newtonsoft.Json;
using System.IO.Compression;

const string version = "1.21.6";
const string versionFolder = $"{version}.jar";
string versionFullPath = Path.Combine(Directory.GetCurrentDirectory(), versionFolder);
string dimensionFilename = Path.Combine(versionFullPath, "dimension_type", "overworld.json");
string overworldGeneratorFilename = Path.Combine(versionFullPath, "worldgen", "noise_settings", "overworld.json");

Console.WriteLine($"Minecraft version {version}");
if (Directory.Exists(versionFullPath))
{
    Console.WriteLine("Using files within existing folder");
    Console.WriteLine($"  {versionFullPath}");
    Console.WriteLine("In case this folder content is obsolete or not correct delete entire folder");
    Console.WriteLine("  and provide path to Minecraft \"data\" subfolder which contain JSON files.");
}
else
{
    Dictionary<string, string> arguments = ParseArguments(args);

    if (arguments.ContainsKey("json"))
    {
        ObtainJsonFiles(Path.GetFullPath(arguments["json"]), versionFullPath);
    }
    else
    {
        Console.WriteLine($"Use command line option \"--json\" to provide full path to Minecraft JSON files.");
    }
}

if (!File.Exists(dimensionFilename) || !File.Exists(overworldGeneratorFilename))
{
    Console.WriteLine("Required JSON files are missing.");
    Console.WriteLine($"Provide full path to Minecraft \"data\" subfolder or file {version}.jar");
    return;
}

static Dictionary<string, string> ParseArguments(string[] args)
{
    var arguments = new Dictionary<string, string>();
    for (int i = 0; i < args.Length; i++)
    {
        if (args[i].StartsWith("--") || args[i].StartsWith("/"))
        {
            string key = args[i].TrimStart('-', '/');
            string val = string.Empty;
            if (key.Contains('='))
            {
                string[] parts = key.Split('=', 2);
                key = parts[0];
                val = parts[1];
            }
            else if (i + 1 < args.Length && !args[i + 1].StartsWith("--") && !args[i + 1].StartsWith("/"))
            {
                val = args[i + 1];
                i++;
            }

            arguments[key.ToLower()] = val;
        }
    }

    return arguments;
}

static void ObtainJsonFiles(string path, string destinationDir)
{
    if (path.ToLower().EndsWith(".jar") && File.Exists(path))
    {
        Console.WriteLine("Extracting files from the provided JAR file");
        Console.WriteLine($"  {path}");
        ExtractJar(path, destinationDir);
    }
    else if (Directory.Exists(path))
    {
        if (Directory.Exists(Path.Combine(path, "data")))
        {
            path = Path.Combine(path, "data");
        }

        if (Directory.Exists(Path.Combine(path, "minecraft")))
        {
            path = Path.Combine(path, "minecraft");
        }

        string[] checkFiles =
        [
            Path.Combine(path, "dimension_type", "overworld.json"),
                Path.Combine(path, "worldgen", "noise_settings", "overworld.json")
        ];
        if (checkFiles.All(file => File.Exists(file)))
        {
            Console.WriteLine("Copying files from the provided folder");
            Console.WriteLine($"  {path}");
            CopyDirectory(path, destinationDir);
        }
    }
}

static void CopyDirectory(string sourceDir, string destinationDir)
{
    try
    {
        Directory.CreateDirectory(destinationDir);
        DirectoryInfo dirInfo = new DirectoryInfo(sourceDir);

        foreach (FileInfo file in dirInfo.GetFiles("*.json"))
        {
            file.CopyTo(Path.Combine(destinationDir, file.Name), true);
        }

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            CopyDirectory(dir.FullName, Path.Combine(destinationDir, dir.Name));
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"CopyDirectory error: {ex.Message}");
    }
}

static void ExtractJar(string jarPath, string destinationDir)
{
    try
    {
        Directory.CreateDirectory(destinationDir);
        using (ZipArchive archive = ZipFile.OpenRead(jarPath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.StartsWith("data/minecraft/") && !string.IsNullOrEmpty(entry.Name) && entry.Name.EndsWith(".json"))
                {
                    string destinationPath = Path.Combine(destinationDir, entry.FullName.Substring("data/minecraft/".Length));
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                    entry.ExtractToFile(destinationPath, true);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ExtractJar error: {ex.Message}");
    }
}

BaseConverter.DataFolderPath = Path.GetFullPath(versionFullPath);
JsonSerializer jsonSerializer = JsonSerializer.Create(BaseConverter.SerializerSettings);

DimensionType overworldDimensionSettings;
if (!File.Exists(dimensionFilename))
    throw new FileNotFoundException(dimensionFilename);
using (StreamReader sr = new StreamReader(dimensionFilename))
{
    using (JsonReader jr = new JsonTextReader(sr))
    {
        jr.SupportMultipleContent = true;
        overworldDimensionSettings = jsonSerializer.Deserialize<DimensionType>(jr)!;
        //overworldDimensionSettings.MonsterSettings = jsonSerializer.Deserialize<MonsterSettings>(jr)!;
    }
}

Dictionary<NoiseType, NoiseParameters> noisesMap = [];
foreach (NoiseType enumVal in Enum.GetValues<NoiseType>())
{
    if (enumVal == NoiseType.NONE) continue;

    string fileName = Path.Combine(versionFullPath, "worldgen", "noise", enumVal.GetEnumMemberValue() + ".json");
    using (StreamReader sr = new StreamReader(fileName))
    {
        using (JsonReader jr = new JsonTextReader(sr))
        {
            NoiseParameters parameters = jsonSerializer.Deserialize<NoiseParameters>(jr)!;
            parameters.NoiseType = enumVal;
            noisesMap.Add(enumVal, parameters);
        }
    }
}

Dictionary<BiomeType, Biome> biomesMap = [];
foreach (BiomeType enumVal in Enum.GetValues<BiomeType>())
{
    if (enumVal == BiomeType.NONE) continue;

    string fileName = Path.Combine(versionFullPath, "worldgen", "biome", enumVal.GetEnumMemberValue() + ".json");
    using (StreamReader sr = new StreamReader(fileName))
    {
        using (JsonReader jr = new JsonTextReader(sr))
        {
            biomesMap.Add(enumVal, jsonSerializer.Deserialize<Biome>(jr)!);
        }
    }
}

NoiseGeneratorSettings overworldGeneratorSettings;
if (!File.Exists(overworldGeneratorFilename))
    throw new FileNotFoundException(overworldGeneratorFilename);
using (StreamReader sr = new StreamReader(overworldGeneratorFilename))
{
    using (JsonReader jr = new JsonTextReader(sr))
    {
        overworldGeneratorSettings = jsonSerializer.Deserialize<NoiseGeneratorSettings>(jr)!;
    }
}

List<StructureSet> structureSets = [];
foreach (string fileName in Directory.GetFiles(Path.Combine(versionFullPath, "worldgen", "structure_set")))
{
    using (StreamReader sr = new StreamReader(fileName))
    {
        using (JsonReader jr = new JsonTextReader(sr))
        {
            structureSets.Add(jsonSerializer.Deserialize<StructureSet>(jr)!);
        }
    }
}

List<Tuple<string, int, int>> testSeeds = [
    new Tuple<string, int, int>("-772558388899695565", -1, 19),
    new Tuple<string, int, int>("1680386384435844663", -2, 4),
    new Tuple<string, int, int>("1950159916917733735", 0, 0),
    new Tuple<string, int, int>("5526243186401289214", -38, 22),
    new Tuple<string, int, int>("4427088792625311137", 4, -16),
    new Tuple<string, int, int>("7288047926724376303", -17, -46),
    new Tuple<string, int, int>("-9198202330763801722", -4, 0),
    new Tuple<string, int, int>("-2891044094412941414", -6, -17)
];
foreach (Tuple<string, int, int> tuple in testSeeds)
{
    var randomState = RandomState.Create(overworldGeneratorSettings, noisesMap, WorldOptions.parseSeed(tuple.Item1) ?? 0L);
    BlockPosition spawnBlock = randomState.Sampler.FindSpawnPosition();
    ChunkPosition spawnChunk = new ChunkPosition(spawnBlock);
    Console.WriteLine($"{tuple.Item1, 20}: Spawn chunk [{spawnChunk.X, 3}; {spawnChunk.Z, 3}]: {(spawnChunk.X == tuple.Item2 && spawnChunk.Z == tuple.Item3 ? "OK" : "Failure")}");
}

Console.WriteLine("Work complete!");
