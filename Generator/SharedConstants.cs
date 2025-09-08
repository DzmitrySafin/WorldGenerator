using Generator.World.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator;

//source: net.minecraft.SharedConstants
public class SharedConstants
{
    static SharedConstants()
    {
        //ResourceLeakDetector.setLevel(NETTY_LEAK_DETECTION);
        //CommandSyntaxException.ENABLE_COMMAND_STACK_TRACES = false;
        //CommandSyntaxException.BUILT_IN_EXCEPTIONS = new BrigadierExceptions();
    }

    [Obsolete]
    public static readonly bool SNAPSHOT = false;
    [Obsolete]
    public static readonly int WORLD_VERSION = 4435;
    [Obsolete]
    public static readonly string SERIES = "main";
    [Obsolete]
    public static readonly string VERSION_STRING = "1.21.6";
    [Obsolete]
    public static readonly int RELEASE_NETWORK_PROTOCOL_VERSION = 771;
    [Obsolete]
    public static readonly int SNAPSHOT_NETWORK_PROTOCOL_VERSION = 256;
    public static readonly int SNBT_NAG_VERSION = 4420;
    //private static readonly int SNAPSHOT_PROTOCOL_BIT = 30;
    public static readonly bool CRASH_EAGERLY = false;
    [Obsolete]
    public static readonly int RESOURCE_PACK_FORMAT = 63;
    [Obsolete]
    public static readonly int DATA_PACK_FORMAT = 80;
    [Obsolete]
    public static readonly int LANGUAGE_FORMAT = 1;
    public static readonly int REPORT_FORMAT_VERSION = 1;
    public static readonly string DATA_VERSION_TAG = "DataVersion";
    public static readonly bool FIX_TNT_DUPE = false;
    public static readonly bool FIX_SAND_DUPE = false;
    public static readonly bool USE_DEBUG_FEATURES = false;
    public static readonly bool DEBUG_OPEN_INCOMPATIBLE_WORLDS = false;
    public static readonly bool DEBUG_ALLOW_LOW_SIM_DISTANCE = false;
    public static readonly bool DEBUG_HOTKEYS = false;
    public static readonly bool DEBUG_UI_NARRATION = false;
    public static readonly bool DEBUG_RENDER = false;
    public static readonly bool DEBUG_SHUFFLE_UI_RENDERING_ORDER = false;
    public static readonly bool DEBUG_RENDER_UI_LAYERING_RECTANGLES = false;
    public static readonly bool DEBUG_PATHFINDING = false;
    public static readonly bool DEBUG_WATER = false;
    public static readonly bool DEBUG_HEIGHTMAP = false;
    public static readonly bool DEBUG_COLLISION = false;
    public static readonly bool DEBUG_SHOW_LOCAL_SERVER_ENTITY_HIT_BOXES = false;
    public static readonly bool DEBUG_SUPPORT_BLOCKS = false;
    public static readonly bool DEBUG_SHAPES = false;
    public static readonly bool DEBUG_NEIGHBORSUPDATE = false;
    public static readonly bool DEBUG_EXPERIMENTAL_REDSTONEWIRE_UPDATE_ORDER = false;
    public static readonly bool DEBUG_STRUCTURES = false;
    public static readonly bool DEBUG_LIGHT = false;
    public static readonly bool DEBUG_SKY_LIGHT_SECTIONS = false;
    public static readonly bool DEBUG_WORLDGENATTEMPT = false;
    public static readonly bool DEBUG_SOLID_FACE = false;
    public static readonly bool DEBUG_CHUNKS = false;
    public static readonly bool DEBUG_GAME_EVENT_LISTENERS = false;
    public static readonly bool DEBUG_DUMP_TEXTURE_ATLAS = false;
    public static readonly bool DEBUG_DUMP_INTERPOLATED_TEXTURE_FRAMES = false;
    public static readonly bool DEBUG_STRUCTURE_EDIT_MODE = false;
    public static readonly bool DEBUG_SAVE_STRUCTURES_AS_SNBT = false;
    public static readonly bool DEBUG_SYNCHRONOUS_GL_LOGS = false;
    public static readonly bool DEBUG_VERBOSE_SERVER_EVENTS = false;
    public static readonly bool DEBUG_NAMED_RUNNABLES = false;
    public static readonly bool DEBUG_GOAL_SELECTOR = false;
    public static readonly bool DEBUG_VILLAGE_SECTIONS = false;
    public static readonly bool DEBUG_BRAIN = false;
    public static readonly bool DEBUG_BEES = false;
    public static readonly bool DEBUG_RAIDS = false;
    public static readonly bool DEBUG_BLOCK_BREAK = false;
    public static readonly bool DEBUG_MONITOR_TICK_TIMES = false;
    public static readonly bool DEBUG_KEEP_JIGSAW_BLOCKS_DURING_STRUCTURE_GEN = false;
    public static readonly bool DEBUG_DONT_SAVE_WORLD = false;
    public static readonly bool DEBUG_LARGE_DRIPSTONE = false;
    public static readonly bool DEBUG_CARVERS = false;
    public static readonly bool DEBUG_ORE_VEINS = false;
    public static readonly bool DEBUG_SCULK_CATALYST = false;
    public static readonly bool DEBUG_BYPASS_REALMS_VERSION_CHECK = false;
    public static readonly bool DEBUG_SOCIAL_INTERACTIONS = false;
    public static readonly bool DEBUG_VALIDATE_RESOURCE_PATH_CASE = false;
    public static readonly bool DEBUG_UNLOCK_ALL_TRADES = false;
    public static readonly bool DEBUG_BREEZE_MOB = false;
    public static readonly bool DEBUG_TRIAL_SPAWNER_DETECTS_SHEEP_AS_PLAYERS = false;
    public static readonly bool DEBUG_VAULT_DETECTS_SHEEP_AS_PLAYERS = false;
    public static readonly bool DEBUG_FORCE_ONBOARDING_SCREEN = false;
    public static readonly bool DEBUG_CURSOR_POS = false;
    public static readonly bool DEBUG_DEFAULT_SKIN_OVERRIDE = false;
    public static readonly bool DEBUG_IGNORE_LOCAL_MOB_CAP = false;
    public static readonly bool DEBUG_DISABLE_LIQUID_SPREADING = false;
    public static readonly bool DEBUG_AQUIFERS = false;
    public static readonly bool DEBUG_JFR_PROFILING_ENABLE_LEVEL_LOADING = false;
    public static readonly bool DEBUG_ENTITY_BLOCK_INTERSECTION = false;
    public static bool debugGenerateSquareTerrainWithoutNoise = false;
    public static bool debugGenerateStripedTerrainWithoutNoise = false;
    public static readonly bool DEBUG_ONLY_GENERATE_HALF_THE_WORLD = false;
    public static readonly bool DEBUG_DISABLE_FLUID_GENERATION = false;
    public static readonly bool DEBUG_DISABLE_AQUIFERS = false;
    public static readonly bool DEBUG_DISABLE_SURFACE = false;
    public static readonly bool DEBUG_DISABLE_CARVERS = false;
    public static readonly bool DEBUG_DISABLE_STRUCTURES = false;
    public static readonly bool DEBUG_DISABLE_FEATURES = false;
    public static readonly bool DEBUG_DISABLE_ORE_VEINS = false;
    public static readonly bool DEBUG_DISABLE_BLENDING = false;
    public static readonly bool DEBUG_DISABLE_BELOW_ZERO_RETROGENERATION = false;
    public static readonly int DEFAULT_MINECRAFT_PORT = 25565;
    public static readonly bool DEBUG_SUBTITLES = false;
    public static readonly int FAKE_MS_LATENCY = 0;
    public static readonly int FAKE_MS_JITTER = 0;
    //public static readonly Level NETTY_LEAK_DETECTION = Level.DISABLED;
    public static readonly bool COMMAND_STACK_TRACES = false;
    public static readonly bool DEBUG_WORLD_RECREATE = false;
    public static readonly bool DEBUG_SHOW_SERVER_DEBUG_VALUES = false;
    public static readonly bool DEBUG_FEATURE_COUNT = false;
    public static readonly bool DEBUG_RESOURCE_GENERATION_OVERRIDE = false;
    public static readonly bool DEBUG_FORCE_TELEMETRY = false;
    public static readonly bool DEBUG_DONT_SEND_TELEMETRY_TO_BACKEND = false;
    public static readonly long MAXIMUM_TICK_TIME_NANOS = TimeSpan.FromMilliseconds(300L).Ticks * TimeSpan.NanosecondsPerTick;
    public static readonly float MAXIMUM_BLOCK_EXPLOSION_RESISTANCE = 3600000.0F;
    public static readonly bool USE_WORKFLOWS_HOOKS = false;
    public static readonly bool USE_DEVONLY = false;
    public static bool CHECK_DATA_FIXER_SCHEMA = true;
    public static bool IS_RUNNING_IN_IDE;
    public static readonly int WORLD_RESOLUTION = 16;
    public static readonly int MAX_CHAT_LENGTH = 256;
    public static readonly int MAX_USER_INPUT_COMMAND_LENGTH = 32500;
    public static readonly int MAX_FUNCTION_COMMAND_LENGTH = 2000000;
    public static readonly int MAX_PLAYER_NAME_LENGTH = 16;
    public static readonly int MAX_CHAINED_NEIGHBOR_UPDATES = 1000000;
    public static readonly int MAX_RENDER_DISTANCE = 32;
    public static readonly char[] ILLEGAL_FILE_CHARACTERS = ['/', '\n', '\r', '\t', '\u0000', '\f', '`', '?', '*', '\\', '<', '>', '|', '"', ':'];
    public static readonly int TICKS_PER_SECOND = 20;
    public static readonly int MILLIS_PER_TICK = 50;
    public static readonly int TICKS_PER_MINUTE = 1200;
    public static readonly int TICKS_PER_GAME_DAY = 24000;
    public static readonly float AVERAGE_GAME_TICKS_PER_RANDOM_TICK_PER_BLOCK = 1365.3334F;
    public static readonly float AVERAGE_RANDOM_TICKS_PER_BLOCK_PER_MINUTE = 0.87890625F;
    public static readonly float AVERAGE_RANDOM_TICKS_PER_BLOCK_PER_GAME_DAY = 17.578125F;
    public static readonly int WORLD_ICON_SIZE = 64;
    private static IWorldVersion? CURRENT_VERSION;

    public static void setVersion(IWorldVersion p_183706_)
    {
        if (CURRENT_VERSION == null)
        {
            CURRENT_VERSION = p_183706_;
        }
        else if (p_183706_ != CURRENT_VERSION)
        {
            throw new Exception("Cannot override the current game version!");
        }
    }

    //public static void tryDetectVersion()
    //{
    //    if (CURRENT_VERSION == null)
    //    {
    //        CURRENT_VERSION = DetectedVersion.tryDetectVersion();
    //    }
    //}

    public static IWorldVersion getCurrentVersion()
    {
        if (CURRENT_VERSION == null)
        {
            throw new Exception("Game version not set");
        }
        else
        {
            return CURRENT_VERSION;
        }
    }

    public static int getProtocolVersion()
    {
        return 771;
    }

    public static bool debugVoidTerrain(ChunkPosition p_183708_)
    {
        int i = p_183708_.GetMinBlockX();
        int j = p_183708_.GetMinBlockZ();
        return !debugGenerateSquareTerrainWithoutNoise ? false : i > 8192 || i < 0 || j > 1024 || j < 0;
    }
}
