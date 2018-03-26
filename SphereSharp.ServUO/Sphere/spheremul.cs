using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public static partial class _Global
    {
        public const SKILL_TYPE SKILL_QTY = (SKILL_TYPE)50;
    }

    public enum CREID_TYPE     // enum the creature art work. (dont allow any others !) also know as "model number"

    {

        CREID_INVALID = 0,



        CREID_OGRE = 0x0001,

        CREID_ETTIN = 0x0002,

        CREID_ZOMBIE = 0x0003,

        CREID_GARGOYLE = 0x0004,

        CREID_EAGLE = 0x0005,

        CREID_BIRD = 0x0006,

        CREID_ORC_LORD = 0x0007,

        CREID_CORPSER = 0x0008,

        CREID_DAEMON = 0x0009,

        CREID_DAEMON_SWORD = 0x000A,



        CREID_DRAGON_GREY = 0x000c,

        CREID_AIR_ELEM = 0x000d,

        CREID_EARTH_ELEM = 0x000e,

        CREID_FIRE_ELEM = 0x000f,

        CREID_WATER_ELEM = 0x0010,

        CREID_ORC = 0x0011,

        CREID_ETTIN_AXE = 0x0012,



        CREID_GIANT_SNAKE = 0x0015,

        CREID_GAZER = 0x0016,



        CREID_LICH = 0x0018,



        CREID_SPECTRE = 0x001a,



        CREID_GIANT_SPIDER = 0x001c,

        CREID_GORILLA = 0x001d,

        CREID_HARPY = 0x001e,

        CREID_HEADLESS = 0x001f,



        CREID_LIZMAN = 0x0021,

        CREID_LIZMAN_SPEAR = 0x0023,

        CREID_LIZMAN_MACE = 0x0024,



        CREID_MONGBAT = 0x0027,



        CREID_ORC_CLUB = 0x0029,

        CREID_RATMAN = 0x002a,



        CREID_RATMAN_CLUB = 0x002c,

        CREID_RATMAN_SWORD = 0x002d,



        CREID_REAPER = 0x002f,  // tree

        CREID_SCORP = 0x0030,   // giant scorp.



        CREID_SKELETON = 0x0032,

        CREID_SLIME = 0x0033,

        CREID_Snake = 0x0034,

        CREID_TROLL_SWORD = 0x0035,

        CREID_TROLL = 0x0036,

        CREID_TROLL_MACE = 0x0037,

        CREID_SKEL_AXE = 0x0038,

        CREID_SKEL_SW_SH = 0x0039,  // sword and sheild

        CREID_WISP = 0x003a,

        CREID_DRAGON_RED = 0x003b,

        CREID_DRAKE_GREY = 0x003c,

        CREID_DRAKE_RED = 0x003d,



        CREID_Tera_Warrior = 0x0046,    // T2A 0x46 = Terathen Warrior

        CREID_Tera_Drone = 0x0047,  // T2A 0x47 = Terathen Drone

        CREID_Tera_Matriarch = 0x0048,  // T2A 0x48 = Terathen Matriarch



        CREID_Titan = 0x004b,   // T2A 0x4b = Titan

        CREID_Cyclops = 0x004c, // T2A 0x4c = Cyclops

        CREID_Giant_Toad = 0x0050,  // T2A 0x50 = Giant Toad

        CREID_Bull_Frog = 0x0051,   // T2A 0x51 = Bull Frog



        CREID_Ophid_Mage = 0x0055,  // T2A 0x55 = Ophidian Mage

        CREID_Ophid_Warrior = 0x0056,   // T2A 0x56 = Ophidian Warrior

        CREID_Ophid_Queen = 0x0057, // T2A 0x57 = Ophidian Queen

        CREID_SEA_Creature = 0x005f,    // T2A 0x5f = (Unknown Sea Creature)



        CREID_SEA_SERP = 0x0096,

        CREID_Dolphin = 0x0097,



        // Animals (Low detail critters)



        CREID_HORSE1 = 0x00C8,  // white = 200 decinal

        CREID_Cat = 0x00c9,

        CREID_Alligator = 0x00CA,

        CREID_Pig = 0x00CB,

        CREID_HORSE4 = 0x00CC,  // brown

        CREID_Rabbit = 0x00CD,

        CREID_LavaLizard = 0x00ce,  // T2A = Lava Lizard

        CREID_Sheep = 0x00CF,   // un-sheered.

        CREID_Chicken = 0x00D0,

        CREID_Goat = 0x00d1,

        CREID_Ostard_Desert = 0x00d2,   // T2A = Desert Ostard (ridable)

        CREID_BrownBear = 0x00D3,

        CREID_GrizzlyBear = 0x00D4,

        CREID_PolarBear = 0x00D5,

        CREID_Panther = 0x00d6,

        CREID_GiantRat = 0x00D7,

        CREID_Cow_BW = 0x00d8,

        CREID_Dog = 0x00D9,

        CREID_Ostard_Frenz = 0x00da,    // T2A = Frenzied Ostard (ridable)

        CREID_Ostard_Forest = 0x00db,   // T2A = Forest Ostard (ridable)

        CREID_Llama = 0x00dc,

        CREID_Walrus = 0x00dd,

        CREID_Sheep_Sheered = 0x00df,

        CREID_Wolf = 0x00e1,

        CREID_HORSE2 = 0x00E2,

        CREID_HORSE3 = 0x00E4,

        CREID_Cow2 = 0x00e7,

        CREID_Bull_Brown = 0x00e8,  // light brown

        CREID_Bull2 = 0x00e9,   // dark brown

        CREID_Hart = 0x00EA,    // Male deer.

        CREID_Deer = 0x00ED,

        CREID_Rat = 0x00ee,



        CREID_Boar = 0x0122,    // large pig

        CREID_HORSE_PACK = 0x0123,  // Pack horse with saddle bags

        CREID_LLAMA_PACK = 0x0124,  // Pack llama with saddle bags



        // all below here are humanish or clothing.

        CREID_MAN = 0x0190, // 400 decimal

        CREID_WOMAN = 0x0191,

        CREID_GHOSTMAN = 0x0192,    // Ghost robe is not automatic !

        CREID_GHOSTWOMAN = 0x0193,

        CREID_EQUIP,



        // removed from T2A

        CREID_CHILD_MB = 0x01a4, // Male Kid (Blond Hair)

        CREID_CHILD_MD = 0x01a5, // Male Kid (Dark Hair)

        CREID_CHILD_FB = 0x01a6, // Female Kid (Blond Hair) (toddler)

        CREID_CHILD_FD = 0x01a7, // Female Kid (Dark Hair)



        CREID_VORTEX = 0x023d,  // T2A = vortex

        CREID_BLADES = 0x023e,  // blade spirits (in human range? not sure why)



        CREID_EQUIP_GM_ROBE = 0x03db,



        // LBR IDs

        CREID_DREAD_SPIDER = 0x000b,

        CREID_FROST_SPIDER = 0x0014,

        CREID_BLACKGATE_DAEMON = 0x0026,

        CREID_BALRON = 0x0028,

        CREID_ICE_FIEND = 0x002b,

        CREID_WYVERN = 0x003e,

        CREID_STONE_GARGOYLE = 0x0043,

        CREID_IMP = 0x004a,

        CREID_TITAN_LBR = 0x004c,   // This one overrides the cyclops from before, I guess

        CREID_KRAKEN = 0x004d,

        CREID_CENTAUR = 0x0065,

        CREID_EXODUS = 0x0066,

        CREID_SERPENTINE_DRAGON = 0x0067,

        CREID_SKELETAL_DRAGON = 0x0068,

        CREID_DARK_STEED = 0x0072,

        CREID_ETHEREAL_HORSE = 0x0073,

        CREID_NIGHTMARE = 0x0074,

        CREID_SILVER_STEED = 0x0075,

        CREID_BRITANNIAN_WARHORSE = 0x0076,

        CREID_MAGECOUNCIL_WARHORSE = 0x0077,

        CREID_MINAX_WARHORSE = 0x0078,

        CREID_SHADOWLORD_WARHORSE = 0x0079,

        CREID_UNICORN = 0x007a,

        CREID_ETHEREAL_WARRIOR = 0x007b,

        CREID_EVIL_MAGE = 0x007c,

        CREID_EVIL_MAGE_LORD = 0x007d,

        CREID_PIXIE = 0x0080,

        CREID_FIRE_GARGOYLE = 0x0082,

        CREID_KIRIN = 0x0084,

        CREID_SEAHORSE = 0x0090,

        CREID_SHADOWLORD = 0x0092,

        CREID_SKELETAL_MAGE = 0x0094,

        CREID_SUCCUBUS = 0x0095,

        CREID_TERETHAN_AVENGER = 0x0098,

        CREID_SHADE = 0x0099,

        CREID_MUMMY = 0x009a,

        CREID_BLACK_SPIDER = 0x009d,

        CREID_ETHEREAL_LLAMA = 0x00aa,

        CREID_ETHEREAL_OSTARD = 0x00ab,

        CREID_SPIDER_LORD = 0x00ad,

        CREID_ABYSS_LORD = 0x00ae,

        CREID_FOREST_LORD = 0x00af,

        CREID_FOREST_QUEEN = 0x00b0,

        CREID_ORC_MAGE = 0x00b5,

        CREID_ORC_BOMBER = 0x00b6,

        CREID_SAVAGE_MALE = 0x00b9,

        CREID_SAVAGE_FEMALE = 0x00ba,

        CREID_RIDGEBACK1 = 0x00bb,

        CREID_RIDGEBACK2 = 0x00bc,

        CREID_ORC_BRUTE = 0x00bd,

        CREID_FIRE_STEED = 0x00be,

        CREID_IRON_GOLEM = 0x02f0,

        CREID_ENSLAVED_GARGOYLE = 0x02f1,

        CREID_ENFORCER_GARGOYLE = 0x02f2,

        CREID_DESTROYER_GARGOYLE = 0x02f3,

        CREID_EXODUS_ENFORCER = 0x02f4,

        CREID_EXODUS_MINION = 0x02f5,

        CREID_GARGOYLE_SHOPKEEPER = 0x02f6,

        CREID_EXODUS_MINION_LORD = 0x02fb,

        CREID_JUKA_WARRIOR = 0x02fc,

        CREID_JUKA_MAGE = 0x02fd,

        CREID_JUKA_LORD = 0x02fe,

        CREID_BLACKTHORN_BETRAYER = 0x02ff,

        CREID_BLACKTHORN_JUGGERNAUT = 0x0300,

        CREID_BLACKTHORN = 0x0301,

        CREID_MEER_MAGE = 0x0302,

        CREID_MEER_WARRIOR = 0x0303,

        CREID_MEER_ETERNAL = 0x0304,

        CREID_MEER_CAPTAIN = 0x0305,

        CREID_DAWN = 0x0306,

        CREID_PLAGUE_BEAST = 0x0307,

        CREID_SMALL_HORDE_DAEMON = 0x0308,

        CREID_DOPPLEGANGER = 0x0309,

        CREID_SWARM = 0x030a,

        CREID_BOGLING = 0x030b,

        CREID_BOG_THING = 0x030c,

        CREID_FIRE_ANT_WORKER = 0x030d,

        CREID_FIRE_ANT_WARRIOR = 0x030e,

        CREID_FIRE_ANT_QUEEN = 0x030f,

        CREID_ARCANE_DAEMON = 0x0310,

        CREID_FOUR_ARMED_DAEMON = 0x0311,

        CREID_ANT_LION = 0x0313,

        CREID_SPHINX = 0x0314,

        CREID_QUAGMIRE = 0x0315,

        CREID_BEETLE = 0x0317,

        CREID_CHAOS_DAEMON = 0x0318,

        CREID_SKELETAL_MOUNT = 0x0319,

        CREID_SWAMP_DRAGON1 = 0x031a,

        CREID_MEDIUM_HORDE_DAEMON = 0x031b,

        CREID_LARGE_HORDE_DAEMON = 0x031c,

        CREID_REPTILE_LORD = 0x031d,

        CREID_ANCIENT_WYRM = 0x031e,

        CREID_SWAMP_DRAGON2 = 0x031f,

        CREID_MULTICOLORED_HORDE_DAEMON = 0x03e7,



        CREID_QTY = 0x0400, // Max number of base character types, based on art work.



        // re-use artwork to make other types on NPC's

        NPCID_SCRIPT = 0x401,



#if _0

	NPCID_Man_Guard		= 0x438,

	NPCID_Woman_Guard	= 0x439,



	NPCID_Cougar		= 0x600,

	NPCID_Lion,

	NPCID_Bird,

	NPCID_JungleBird,

	NPCID_Parrot,

	NPCID_Raven,



	NPCID_Ogre_Lord,

	NPCID_Ogre_Mage,



	NPCID_Energy_Vortex	= 0x1063,	// Normal (non-T2A) = vortex

#endif



        NPCID_SCRIPT2 = 0x4000, // Safe area for server specific NPC defintions.

        NPCID_Qty = 0x8000,     // Spawn types start here.



        SPAWNTYPE_START = 0x8001,



#if _0

	SPAWNTYPE_UNDEAD = 0x8001, // undead, grave yard.

	SPAWNTYPE_ELEMS	 = 0x8002, // elementals

	SPAWNTYPE_SEWER	 = 0x8003, // dungeon/sewer vermin. rats, snakes, spiders, ratmen

	SPAWNTYPE_WOODS	 = 0x8004, // woodsy.

	SPAWNTYPE_WATER	 = 0x8005, // water

	SPAWNTYPE_DESERT = 0x8006, // desert creatures, scorps,

	SPAWNTYPE_DOMESTIC= 0x8007, // domestic

	SPAWNTYPE_ORC	 = 0x8008, // Orc Camp, Orcs and Wargs.

	SPAWNTYPE_LIZARDMAN= 0x8009, // Lizardman Camp

	SPAWNTYPE_GIANTS = 0x800a, // Giant kin, Ogre, Trolls. Ettins

	SPAWNTYPE_MAGIC  = 0x800b, // Magic forest. Reapers, corpsers,

	SPAWNTYPE_FLY	= 0x800c,	// Flying creatures.

	SPAWNTYPE_DRAGONS,

#endif



    };

    public enum ITEMID_TYPE    // InsideUO is great for this stuff.

    {

        ITEMID_NOTHING = 0x0000,    // Used for lightning.

        ITEMID_NODRAW = 1,

        ITEMID_ANKH_S,

        ITEMID_ANKH_N,

        ITEMID_ANKH_W,

        ITEMID_ANKH_E,



        ITEMID_STONE_WALL = 0x0080,



        ITEMID_DOOR_SECRET_1 = 0x00E8,

        ITEMID_DOOR_SECRET_2 = 0x0314,

        ITEMID_DOOR_SECRET_3 = 0x0324,

        ITEMID_DOOR_SECRET_4 = 0x0334,

        ITEMID_DOOR_SECRET_5 = 0x0344,

        ITEMID_DOOR_SECRET_6 = 0x0354,



        ITEMID_DOOR_METAL_S = 0x0675,   // 1

        ITEMID_DOOR_METAL_S_2 = 0x0677,

        ITEMID_DOOR_METAL_S_3 = 0x067D,

        ITEMID_DOOR_BARRED = 0x0685,    // 2

        ITEMID_DOOR_RATTAN = 0x0695,    // 3

        ITEMID_DOOR_WOODEN_1 = 0x06A5,  // 4

        ITEMID_DOOR_WOODEN_1_o = 0x06A6,    // 4

        ITEMID_DOOR_WOODEN_1_2 = 0x06A7,

        ITEMID_DOOR_WOODEN_2 = 0x06B5,  // 5

        ITEMID_DOOR_METAL_L = 0x06C5,   // 6

        ITEMID_DOOR_WOODEN_3 = 0x06D5,  // 7

        ITEMID_DOOR_WOODEN_4 = 0x06E5,  // 8

        ITEMID_DOOR_HI = 0x06f4,



        ITEMID_DOOR_IRONGATE_1 = 0x0824,

        ITEMID_DOOR_WOODENGATE_1 = 0x0839,

        ITEMID_DOOR_IRONGATE_2 = 0x084C,

        ITEMID_DOOR_WOODENGATE_2 = 0x0866,



        ITEMID_ROCK_1_LO = 0x8e0,

        ITEMID_ROCK_1_HI = 0x8ea,



        ITEMID_BEEHIVE = 0x091a,



        ITEMID_FOOD_BACON = 0x0976,

        ITEMID_FOOD_FISH_RAW = 0x97a,

        ITEMID_FOOD_FISH = 0x097b,



        ITEMID_RBASKET = 0x0990,    // 0x0E78,



        ITEMID_BOOZE_LIQU_B1 = 0x099b,

        ITEMID_BOOZE_LIQU_B2 = 0x099c,

        ITEMID_BOOZE_LIQU_B3 = 0x099d,

        ITEMID_BOOZE_LIQU_B4 = 0x099e,

        ITEMID_BOOZE_ALE_B1 = 0x099f,

        ITEMID_BOOZE_ALE_B2 = 0x09a0,

        ITEMID_BOOZE_ALE_B3 = 0x09a1,

        ITEMID_BOOZE_ALE_B4 = 0x09a2,



        ITEMID_PITCHER = 0x09a7,    // empty.

        ITEMID_BOX_METAL = 0x09a8,

        ITEMID_CRATE7 = 0x09a9,

        ITEMID_BOX_WOOD1 = 0x09aa,

        ITEMID_CHEST_SILVER2 = 0x09ab,

        ITEMID_BASKET = 0x09ac,

        ITEMID_POUCH2 = 0x09b0,

        ITEMID_BASKET2 = 0x09b1,

        ITEMID_BANK_BOX = 0x09b2, // another pack really but used as bank box.



        ITEMID_FOOD_EGGS_RAW = 0x09b5,

        ITEMID_FOOD_EGGS = 0x09b6,

        ITEMID_FOOD_BIRD1 = 0x09b7,

        ITEMID_FOOD_BIRD2 = 0x09b8,

        ITEMID_FOOD_BIRD1_RAW = 0x09b9,

        ITEMID_FOOD_BIRD2_RAW = 0x09ba,



        ITEMID_FOOD_SAUSAGE = 0x09c0,

        ITEMID_BOOZE_WINE_B1 = 0x09c4,

        ITEMID_BOOZE_WINE_B2 = 0x09c5,

        ITEMID_BOOZE_WINE_B3 = 0x09c6,

        ITEMID_BOOZE_WINE_B4 = 0x09c7,



        ITEMID_FOOD_HAM = 0x09C9,



        ITEMID_FISH_1 = 0x09CC,

        ITEMID_FISH_2 = 0x09CD,

        ITEMID_FISH_3 = 0x09CE,

        ITEMID_FISH_4 = 0x09CF,



        ITEMID_FRUIT_APPLE = 0x09d0,

        ITEMID_FRUIT_PEACH1 = 0x09d2,

        ITEMID_FRUIT_GRAPE = 0x09d7,



        ITEMID_FOOD_CAKE = 0x09e9,



        ITEMID_JAR_HONEY = 0x09EC,

        ITEMID_BOOZE_ALE_M1 = 0x09ee,

        ITEMID_BOOZE_ALE_M2 = 0x09ef,



        ITEMID_FOOD_MEAT_RAW = 0x09f1,

        ITEMID_FOOD_MEAT = 0x09f2,



        ITEMID_LANTERN = 0x0A25,



        ITEMID_BEDROLL_O_EW = 0x0a55,

        ITEMID_BEDROLL_O_NS,

        ITEMID_BEDROLL_C,

        ITEMID_BEDROLL_C_NS,

        ITEMID_BEDROLL_C_EW = 0x0a59,



        ITEMID_BED1_1 = 0x0a5a,

        // some things in here are not bed but sheets and blankets.

        ITEMID_BED1_X = 0x0a91,



        ITEMID_BOOKSHELF1 = 0x0a97, // book shelf

        ITEMID_BOOKSHELF2 = 0x0a98, // book shelf

        ITEMID_BOOKSHELF3 = 0x0a99, // book shelf

        ITEMID_BOOKSHELF4 = 0x0a9a, // book shelf

        ITEMID_BOOKSHELF5 = 0x0a9b, // book shelf

        ITEMID_BOOKSHELF6 = 0x0a9c, // book shelf

        ITEMID_BOOKSHELF7 = 0x0a9d, // book shelf

        ITEMID_BOOKSHELF8 = 0x0a9e, // book shelf



        ITEMID_WATER_TROUGH_1 = 0x0B41,

        ITEMID_WATER_TROUGH_2 = 0x0B44,



        ITEMID_PLANT_COTTON1 = 0x0c4f,// old

        ITEMID_PLANT_COTTON2 = 0x0c50,

        ITEMID_PLANT_COTTON3 = 0x0c51,

        ITEMID_PLANT_COTTON4 = 0x0c52,

        ITEMID_PLANT_COTTON5 = 0x0c53,

        ITEMID_PLANT_COTTON6 = 0x0c54,// young

        ITEMID_PLANT_WHEAT1 = 0x0c55,

        ITEMID_PLANT_WHEAT2 = 0x0c56,

        ITEMID_PLANT_WHEAT3 = 0x0c57,

        ITEMID_PLANT_WHEAT4 = 0x0c58,

        ITEMID_PLANT_WHEAT5 = 0x0c59,

        ITEMID_PLANT_WHEAT6 = 0x0c5a,

        ITEMID_PLANT_WHEAT7 = 0x0c5b,



        ITEMID_PLANT_VINE1 = 0x0c5e,

        ITEMID_PLANT_VINE2 = 0x0c5f,

        ITEMID_PLANT_VINE3 = 0x0c60,



        ITEMID_PLANT_TURNIP1 = 0x0c61,

        ITEMID_PLANT_TURNIP2 = 0x0c62,

        ITEMID_PLANT_TURNIP3 = 0x0c63,

        ITEMID_SPROUT_NORMAL = 0x0c68,

        ITEMID_PLANT_ONION = 0x0c6f,

        ITEMID_PLANT_CARROT = 0x0c76,

        ITEMID_PLANT_CORN1 = 0x0c7d,

        ITEMID_PLANT_CORN2 = 0x0c7e,



        ITEMID_FRUIT_WATERMELLON1 = 0x0c5c,

        ITEMID_FRUIT_WATERMELLON2 = 0x0c5d,

        ITEMID_FRUIT_GOURD1 = 0x0c64,

        ITEMID_FRUIT_GOURD2 = 0x0c65,

        ITEMID_FRUIT_GOURD3 = 0x0c66,

        ITEMID_FRUIT_GOURD4 = 0x0c67,

        ITEMID_SPROUT_NORMAL2 = 0x0c69,

        ITEMID_FRUIT_ONIONS1 = 0x0c6d,

        ITEMID_FRUIT_ONIONS2 = 0x0c6e,

        ITEMID_FRUIT_PUMPKIN1 = 0x0c6a,

        ITEMID_FRUIT_PUMPKIN2 = 0x0c6b,

        ITEMID_FRUIT_PUMPKIN3 = 0x0c6c,

        ITEMID_FRUIT_LETTUCE1 = 0x0c70,

        ITEMID_FRUIT_LETTUCE2 = 0x0c71,

        ITEMID_FRUIT_SQUASH1 = 0x0c72,

        ITEMID_FRUIT_SQUASH2 = 0x0c73,

        ITEMID_FRUIT_HONEYDEW_MELLON1 = 0x0c74,

        ITEMID_FRUIT_HONEYDEW_MELLON2 = 0x0c75,

        ITEMID_FRUIT_CARROT1 = 0x0c77,

        ITEMID_FRUIT_CARROT2 = 0x0c78,

        ITEMID_FRUIT_CANTALOPE1 = 0x0c79,

        ITEMID_FRUIT_CANTALOPE2 = 0x0c7a,

        ITEMID_FRUIT_CABBAGE1 = 0x0c7b,

        ITEMID_FRUIT_CABBAGE2 = 0x0c7c,

        ITEMID_FRUIT_CORN1 = 0x0c7f,

        ITEMID_FRUIT_CORN2 = 0x0c80,

        ITEMID_FRUIT_CORN3 = 0x0c81,

        ITEMID_FRUIT_CORN4 = 0x0c82,



        ITEMID_TREE_COCONUT = 0x0c95,

        ITEMID_TREE_DATE = 0x0c96,



        ITEMID_TREE_BANANA1 = 0x0ca8,

        ITEMID_TREE_BANANA2 = 0x0caa,

        ITEMID_TREE_BANANA3 = 0x0cab,



        ITEMID_TREE_LO = 0x0cca,

        ITEMID_TREE_HI = 0x0ce8,



        ITEMID_PLANT_GRAPE = 0x0d1a,    // fruit

        ITEMID_PLANT_GRAPE1 = 0x0d1b,

        ITEMID_PLANT_GRAPE2 = 0x0d1c,

        ITEMID_PLANT_GRAPE3 = 0x0d1d,

        ITEMID_PLANT_GRAPE4 = 0x0d1e,

        ITEMID_PLANT_GRAPE5 = 0x0d1f,

        ITEMID_PLANT_GRAPE6 = 0x0d20,

        ITEMID_PLANT_GRAPE7 = 0x0d21,

        ITEMID_PLANT_GRAPE8 = 0x0d22,

        ITEMID_PLANT_GRAPE9 = 0x0d23,

        ITEMID_PLANT_GRAPE10 = 0x0d24,



        ITEMID_FRUIT_TURNIP1 = 0x0d39,

        ITEMID_FRUIT_TURNIP2 = 0x0d3a,



        ITEMID_TREE2_LO = 0xd41,

        ITEMID_TREE2_HI = 0xd44,



        ITEMID_TREE3_LO = 0x0d57,

        ITEMID_TREE3_HI = 0x0d5b,



        ITEMID_TREE4_LO = 0x0d6e,

        ITEMID_TREE4_HI = 0x0d72,



        ITEMID_TREE5_LO = 0x0d84,

        ITEMID_TREE5_HI = 0x0d86,



        ITEMID_TREE_APPLE_BARK1 = 0x0d94,

        ITEMID_TREE_APPLE_EMPTY1 = 0x0d95,

        ITEMID_TREE_APPLE_FULL1 = 0x0d96,

        ITEMID_TREE_APPLE_FALL1 = 0x0d97,

        ITEMID_TREE_APPLE_BARK2 = 0x0d98,

        ITEMID_TREE_APPLE_EMPTY2 = 0x0d99,

        ITEMID_TREE_APPLE_FULL2 = 0x0d9a,

        ITEMID_TREE_APPLE_FALL2 = 0x0d9b,

        ITEMID_TREE_PEACH_BARK1 = 0x0d9c,

        ITEMID_TREE_PEACH_EMPTY1 = 0x0d9d,

        ITEMID_TREE_PEACH_FULL1 = 0x0d9e,

        ITEMID_TREE_PEACH_FALL1 = 0x0d9f,

        ITEMID_TREE_PEACH_BARK2 = 0x0da0,

        ITEMID_TREE_PEACH_EMPTY2 = 0x0da1,

        ITEMID_TREE_PEACH_FULL2 = 0x0da2,

        ITEMID_TREE_PEACH_FALL2 = 0x0da3,

        ITEMID_TREE_PEAR_BARK1 = 0x0da4,

        ITEMID_TREE_PEAR_EMPTY1 = 0x0da5,

        ITEMID_TREE_PEAR_FULL1 = 0x0da6,

        ITEMID_TREE_PEAR_FALL1 = 0x0da7,

        ITEMID_TREE_PEAR_BARK2 = 0x0da8,

        ITEMID_TREE_PEAR_EMPTY2 = 0x0da9,

        ITEMID_TREE_PEAR_FULL2 = 0x0daa,

        ITEMID_TREE_PEAR_FALL2 = 0x0dab,



        ITEMID_PLANT_WHEAT8 = 0x0dae,

        ITEMID_PLANT_WHEAT9 = 0x0daf,



        ITEMID_SIGN_BRASS_1 = 0x0bd1,

        ITEMID_SIGN_BRASS_2 = 0x0bd2,



        ITEMID_BED2_1 = 0x0db0,

        ITEMID_BED2_5 = 0x0db5,



        ITEMID_FISH_POLE1 = 0x0dbf,

        ITEMID_FISH_POLE2 = 0x0dc0,



        ITEMID_MOONGATE_RED = 0x0dda,



        ITEMID_FRUIT_COTTON = 0x0def,

        ITEMID_SCISSORS1 = 0x0dfc,

        ITEMID_SCISSORS2 = 0x0dfd,



        ITEMID_KINDLING1 = 0x0de1,

        ITEMID_KINDLING2 = 0x0de2,

        ITEMID_CAMPFIRE = 0x0de3,

        ITEMID_EMBERS = 0x0de9,



        ITEMID_WAND = 0x0df2,



        ITEMID_COTTON_RAW = 0x0def,

        ITEMID_WOOL = 0x0df8,

        ITEMID_COTTON = 0x0df9,

        ITEMID_FEATHERS2a = 0x0dfa,

        ITEMID_FEATHERS2b = 0x0dfb,



        ITEMID_GAME_BACKGAM = 0x0e1c,

        ITEMID_YARN1 = 0x0e1d,

        ITEMID_YARN2 = 0x0e1e,

        ITEMID_YARN3 = 0x0e1f,



        ITEMID_BANDAGES_BLOODY1 = 0x0e20,

        ITEMID_BANDAGES1 = 0x0e21,  // clean

        ITEMID_BANDAGES_BLOODY2 = 0x0e22,



        ITEMID_EMPTY_VIAL = 0x0e24,

        ITEMID_BOTTLE1_1 = 0x0e25,

        ITEMID_BOTTLE1_DYE = 0x0e27,

        ITEMID_BOTTLE1_8 = 0x0e2c,



        ITEMID_CRYSTAL_BALL1 = 0x0e2d,

        ITEMID_CRYSTAL_BALL4 = 0x0e30,



        ITEMID_SCROLL2_BLANK = 0x0e34,

        ITEMID_SCROLL2_B1 = 0x0e35,

        ITEMID_SCROLL2_B6 = 0x0e3a,

        ITEMID_SPELLBOOK2 = 0x0E3b, // ??? looks like a spellbook but doesn open corectly !



        ITEMID_CRATE1 = 0x0e3c, // n/s

        ITEMID_CRATE2 = 0x0e3d, // e/w

        ITEMID_CRATE3 = 0x0e3e,

        ITEMID_CRATE4 = 0x0e3f,



        ITEMID_CHEST_METAL2_1 = 0x0e40,

        ITEMID_CHEST_METAL2_2 = 0x0e41, // 2 tone chest.

        ITEMID_CHEST3 = 0x0e42,

        ITEMID_CHEST4 = 0x0e43,



        ITEMID_Cannon_Ball = 0x0e73,

        ITEMID_Cannon_Balls = 0x0e74,

        ITEMID_BACKPACK = 0x0E75,   // containers.

        ITEMID_BAG = 0x0E76,

        ITEMID_BARREL = 0x0E77,

        ITEMID_BASIN = 0x0e78,

        ITEMID_POUCH = 0x0E79,

        ITEMID_SBASKET = 0x0E7A,    // picknick basket

        ITEMID_CHEST_SILVER = 0x0E7C,   // all grey. BANK BOX

        ITEMID_BOX_WOOD2 = 0x0E7D,

        ITEMID_CRATE5 = 0x0E7E,

        ITEMID_KEG = 0x0E7F,

        ITEMID_BRASSBOX = 0x0E80,



        ITEMID_HERD_CROOK1 = 0x0E81,    // Shepherds Crook

        ITEMID_HERD_CROOK2 = 0x0e82,

        ITEMID_Pickaxe1 = 0x0e85,

        ITEMID_Pickaxe2 = 0x0e86,

        ITEMID_Pitchfork = 0x0e87,



        ITEMID_Cannon_N_1 = 0x0e8b,

        ITEMID_Cannon_N_3 = 0x0e8d,

        ITEMID_Cannon_W_1 = 0x0e8e,

        ITEMID_Cannon_W_3 = 0x0e90,

        ITEMID_Cannon_S_1 = 0x0e91,

        ITEMID_Cannon_S_3 = 0x0e93,

        ITEMID_Cannon_E_1 = 0x0e94,

        ITEMID_Cannon_E_3 = 0x0e96,



        ITEMID_MORTAR = 0x0e9b,



        ITEMID_MUSIC_DRUM = 0x0e9c,

        ITEMID_MUSIC_TAMB1,

        ITEMID_MUSIC_TAMB2,



        ITEMID_BBOARD_MSG = 0x0eb0, // a message on the bboard



        ITEMID_MUSIC_HARP_S = 0x0eb1,

        ITEMID_MUSIC_HARP_L,

        ITEMID_MUSIC_LUTE1,

        ITEMID_MUSIC_LUTE2,



        ITEMID_SKELETON_1 = 0x0eca,

        ITEMID_SKELETON_2,

        ITEMID_SKELETON_3,

        ITEMID_SKELETON_4,

        ITEMID_SKELETON_5,

        ITEMID_SKELETON_6,

        ITEMID_SKELETON_7,

        ITEMID_SKELETON_8,

        ITEMID_SKELETON_9,



        ITEMID_GUILDSTONE1 = 0x0EDD,

        ITEMID_GUILDSTONE2 = 0x0EDE,



        ITEMID_WEB1_1 = 0x0ee3,

        ITEMID_WEB1_4 = 0x0ee6,



        ITEMID_BANDAGES2 = 0x0ee9,  // clean

        ITEMID_COPPER_C1 = 0x0EEA,

        ITEMID_GOLD_C1 = 0x0EED,    // big pile

        ITEMID_SILVER_C1 = 0x0ef0,

        ITEMID_SILVER_C3 = 0x0ef2,



        ITEMID_SCROLL1_BLANK = 0x0ef3,

        ITEMID_SCROLL1_B1 = 0x0ef4,

        ITEMID_SCROLL1_B6 = 0x0ef9,

        ITEMID_SPELLBOOK = 0x0EFA,



        ITEMID_BOTTLE2_1 = 0x0efb,

        ITEMID_BOTTLE2_DYE = 0x0eff,

        ITEMID_BOTTLE2_10 = 0x0f04,



        ITEMID_POTION_BLACK = 0x0f06,

        ITEMID_POTION_ORANGE,

        ITEMID_POTION_BLUE,

        ITEMID_POTION_WHITE,

        ITEMID_POTION_GREEN,

        ITEMID_POTION_RED,

        ITEMID_POTION_YELLOW,

        ITEMID_POTION_PURPLE = 0x0f0d,

        ITEMID_EMPTY_BOTTLE = 0x0f0e,



        ITEMID_GEM1 = 0x0f0f,

        ITEMID_GEMS = 0x0F20,

        ITEMID_GEML = 0x0F30,



        ITEMID_HAY = 0x0f36,    // sheif of hay.



        ITEMID_Shovel1 = 0x0f39,

        ITEMID_Shovel2 = 0x0f3a,



        ITEMID_FRUIT_HAY1 = 0x0f36,

        ITEMID_Arrow = 0x0f3f,  // Need these to use a bow.

        ITEMID_ArrowX = 0x0f42,

        ITEMID_DAGGER = 0x0F51,



        ITEMID_MOONGATE_BLUE = 0x0f6c,

        ITEMID_REAG_1 = 0x0f78, // batwing



        ITEMID_REAG_BP = 0x0f7a,    // black pearl.

        ITEMID_REAG_BM = 0x0f7b,    //'Blood Moss'

        ITEMID_REAG_GA = 0x0f84,    //'Garlic'

        ITEMID_REAG_GI = 0x0f85,    //'Ginseng'

        ITEMID_REAG_MR = 0x0f86,    //'Mandrake Root'

        ITEMID_REAG_NS = 0x0f88,    //'Nightshade'

        ITEMID_REAG_SA = 0x0f8c,    //'Sulfurous Ash'

        ITEMID_REAG_SS = 0x0f8d,    //'Spider's Silk'



        ITEMID_REAG_26 = 0x0f91,    // Worms heart



        ITEMID_CLOTH_BOLT1 = 0x0f95,

        ITEMID_CLOTH_BOLT8 = 0x0f9c,

        ITEMID_SEWINGKIT = 0x0f9d,

        ITEMID_SCISSORS3 = 0x0f9e,

        ITEMID_SCISSORS4 = 0x0f9f,

        ITEMID_THREAD1 = 0x0fa0,

        ITEMID_THREAD2 = 0x0fa1,



        ITEMID_GAME_BOARD = 0x0fa6,

        ITEMID_GAME_DICE = 0x0fa7,



        ITEMID_DYE = 0x0FA9,

        ITEMID_DYEVAT = 0x0FAB,

        ITEMID_GAME_BACKGAM_2 = 0x0fad,

        ITEMID_BARREL_2 = 0x0FAE,

        ITEMID_ANVIL1 = 0x0FAF,

        ITEMID_ANVIL2 = 0x0FB0,

        ITEMID_FORGE_1 = 0x0FB1,



        ITEMID_BOOK1 = 0x0fbd,

        ITEMID_BOOK2 = 0x0fbe,



        ITEMID_BOOK3 = 0x0fef,

        ITEMID_BOOK8 = 0x0ff4,



        ITEMID_PITCHER_WATER = 0x0ff8,



        ITEMID_ARCHERYBUTTE_E = 0x100a,

        ITEMID_ARCHERYBUTTE_S = 0x100b,



        ITEMID_FRUIT_HAY2 = 0x100c,

        ITEMID_FRUIT_HAY3 = 0x100d,



        ITEMID_KEY_COPPER = 0x100e,

        // ...

        ITEMID_KEY_RING0 = 0x1011,

        ITEMID_KEY_MAGIC = 0x1012,

        ITEMID_KEY_RUSTY = 0x1013,



        ITEMID_SPINWHEEL1 = 0x1015,

        ITEMID_SPINWHEEL2 = 0x1019,

        ITEMID_WOOL2 = 0x101f,



        ITEMID_SHAFTS3a = 0x1024,

        ITEMID_SHAFTS3b = 0x1025,



        ITEMID_CHISELS_1 = 0x1026,

        ITEMID_CHISELS_2 = 0x1027,

        ITEMID_DOVETAIL_SAW_1 = 0x1028,

        ITEMID_DOVETAIL_SAW_2 = 0x1029,

        ITEMID_HAMMER_1 = 0x102a,

        ITEMID_HAMMER_2 = 0x102b,

        ITEMID_SAW_1 = 0x1034,

        ITEMID_SAW_2 = 0x1035,



        ITEMID_FOOD_BREAD = 0x103b,

        ITEMID_FOOD_DOUGH_RAW = 0x103d,

        ITEMID_FOOD_COOKIE_RAW = 0x103f,

        ITEMID_FLOUR = 0x1039,

        ITEMID_FOOD_PIZZA = 0x1040,

        ITEMID_FOOD_PIE = 0x1041,

        ITEMID_FOOD_PIE_RAW = 0x1042,



        ITEMID_CLOCK1 = 0x104B,

        ITEMID_CLOCK2 = 0x104C,



        ITEMID_SEXTANT_1 = 0x1057,

        ITEMID_SEXTANT_2 = 0x1058,

        ITEMID_LOOM1 = 0x105f,

        ITEMID_LOOM2 = 0x1063,



        ITEMID_LEATHER_1 = 0x1067,

        ITEMID_LEATHER_2 = 0x1068,



        ITEMID_DUMMY1 = 0x1070, // normal training dummy.

        ITEMID_FX_DUMMY1 = 0x1071,

        ITEMID_DUMMY2 = 0x1074,

        ITEMID_FX_DUMMY2 = 0x1075,



        ITEMID_HIDES = 0x1078,

        ITEMID_HIDES_2 = 0x1079,

        ITEMID_FOOD_PIZZA_RAW = 0x1083,



        ITEMID_WEB2_1 = 0x10b8,

        ITEMID_WEB2_x = 0x10d7,



        ITEMID_TRAP_FACE1 = 0x10f5,

        ITEMID_TRAP_FX_FACE1 = 0x10f6,

        ITEMID_TRAP_FACE2 = 0x10fc,

        ITEMID_TRAP_FX_FACE2 = 0x10fd,



        ITEMID_FX_SPARKLES = 0x1153,    // magic sparkles.



        ITEMID_GRAVE_1 = 0x1165,

        ITEMID_GRAVE_32 = 0x1184,



        ITEMID_TRAP_CRUMBLEFLOOR = 0x11c0,



        ITEMID_BED3_1 = 0x11ce,

        ITEMID_BED3_X = 0x11d5,



        ITEMID_FURS = 0x11fa,



        ITEMID_BLOOD1 = 0x122a,

        ITEMID_BLOOD6 = 0x122f,



        ITEMID_ROCK_B_LO = 0x134f,  // boulder.

        ITEMID_ROCK_B_HI = 0x1362,

        ITEMID_ROCK_2_LO = 0x1363,

        ITEMID_ROCK_2_HI = 0x136d,



        ITEMID_BOW1 = 0x13b1,

        ITEMID_BOW2,



        ITEMID_SMITH_HAMMER = 0x13E4,



        ITEMID_HERD_CROOK3 = 0x13f4,

        ITEMID_HERD_CROOK4 = 0x13f5,



        ITEMID_BEE_WAX = 0x1423,

        ITEMID_GRAIN = 0x1449,



        ITEMID_BONE_ARMS = 0x144e,

        ITEMID_BONE_ARMOR = 0x144f,

        ITEMID_BONE_GLOVES = 0x1450,

        ITEMID_BONE_HELM = 0x1451,

        ITEMID_BONE_LEGS = 0x1452,



        ITEMID_TELESCOPE1 = 0x1459, // Big telescope

        ITEMID_TELESCOPEX = 0x149a,



        ITEMID_MAP = 0x14EB,

        ITEMID_MAP_2 = 0x14ec,



        ITEMID_DEED1 = 0x14ef,

        ITEMID_DEED2 = 0x14f0,

        ITEMID_SHIP_PLANS1 = 0x14f1,

        ITEMID_SHIP_PLANS2 = 0x14f2,



        ITEMID_SPYGLASS_1 = 0x14f5,

        ITEMID_SPYGLASS_2 = 0x14f6,



        ITEMID_LOCKPICK1 = 0x14fb,

        ITEMID_LOCKPICK4 = 0x14fe,



        ITEMID_SHIRT1 = 0x1517,

        ITEMID_PANTS1 = 0x152E,

        ITEMID_PANTS_FANCY = 0x1539,

        ITEMID_SASH = 0x1541,



        ITEMID_HELM_BEAR = 0x1545,

        ITEMID_HELM_DEER = 0x1547,

        ITEMID_MASK_TREE = 0x1549,

        ITEMID_MASK_VOODOO = 0x154b,



        ITEMID_FOOD_LEG1_RAW = 0x1607,

        ITEMID_FOOD_LEG1 = 0x1608,

        ITEMID_FOOD_LEG2_RAW = 0x1609,

        ITEMID_FOOD_LEG2 = 0x160a,



        ITEMID_FOOD_COOKIES = 0x160b,

        ITEMID_BLOOD_SPLAT = 0x1645,



        ITEMID_LIGHT_SRC = 0x1647,



        ITEMID_WHIP1 = 0x166e,

        ITEMID_WHIP2 = 0x166f,



        ITEMID_SANDALS = 0x170d,

        ITEMID_SHOES = 0x170F,



        ITEMID_HAT_WIZ = 0x1718,

        ITEMID_HAT_JESTER = 0x171c,



        ITEMID_FRUIT_BANANA1 = 0x171f,

        ITEMID_FRUIT_BANANA2 = 0x1720,

        ITEMID_FRUIT_COCONUT2 = 0x1723,

        ITEMID_FRUIT_COCONUT3 = 0x1724,

        ITEMID_FRUIT_COCONUT1 = 0x1726,

        ITEMID_FRUIT_DATE1 = 0x1727,

        ITEMID_FRUIT_LEMON = 0x1728,

        ITEMID_FRUIT_LIME = 0x172a,

        ITEMID_FRUIT_PEACH2 = 0x172c,

        ITEMID_FRUIT_PEAR = 0x172d,



        ITEMID_CLOTH1 = 0x175d,

        ITEMID_CLOTH8 = 0x1764,



        ITEMID_KEY_RING1 = 0x1769,

        ITEMID_KEY_RING3 = 0x176a,

        ITEMID_KEY_RING5 = 0x176b,



        ITEMID_ROCK_3_LO = 0x1771,

        ITEMID_ROCK_3_HI = 0x177c,



        ITEMID_ALCH_SYM_1 = 0x181d,

        ITEMID_ALCH_SYM_2 = 0x181e,

        ITEMID_ALCH_SYM_3 = 0x181f,

        ITEMID_ALCH_SYM_4 = 0x1820,

        ITEMID_ALCH_SYM_5 = 0x1821,

        ITEMID_ALCH_SYM_6 = 0x1822,

        ITEMID_ALCH_SYM_7 = 0x1823,

        ITEMID_ALCH_SYM_8 = 0x1824,

        ITEMID_ALCH_SYM_9 = 0x1825,

        ITEMID_ALCH_SYM_10 = 0x1826,

        ITEMID_ALCH_SYM_11 = 0x1827,

        ITEMID_ALCH_SYM_12 = 0x1828,



        ITEMID_FRUIT_MANDRAKE_ROOT1 = 0x18dd,

        ITEMID_FRUIT_MANDRAKE_ROOT2 = 0x18de,



        ITEMID_PLANT_MANDRAKE1 = 0x18df,

        ITEMID_PLANT_MANDRAKE2 = 0x18e0,

        ITEMID_PLANT_GARLIC1 = 0x18e1,

        ITEMID_PLANT_GARLIC2 = 0x18e2,

        ITEMID_FRUIT_GARLIC1 = 0x18e3,

        ITEMID_FRUIT_GARLIC2 = 0x18e4,

        ITEMID_PLANT_NIGHTSHADE1 = 0x18e5,

        ITEMID_PLANT_NIGHTSHADE2 = 0x18e6,

        ITEMID_FRUIT_NIGHTSHADE1 = 0x18e7,

        ITEMID_FRUIT_NIGHTSHADE2 = 0x18e8,

        ITEMID_PLANT_GENSENG1 = 0x18e9,

        ITEMID_PLANT_GENSENG2 = 0x18ea,

        ITEMID_FRUIT_GENSENG1 = 0x18eb,

        ITEMID_FRUIT_GENSENG2 = 0x18ec,



        ITEMID_Keg2 = 0x1940,



        ITEMID_BELLOWS_1 = 0x197a,

        ITEMID_BELLOWS_2 = 0x197b,

        ITEMID_BELLOWS_3 = 0x197c,

        ITEMID_BELLOWS_4 = 0x197d,

        ITEMID_FORGE_2 = 0x197e,

        ITEMID_FORGE_3 = 0x197f,

        ITEMID_FORGE_4 = 0x1980,

        ITEMID_FORGE_5 = 0x1981,

        ITEMID_FORGE_6 = 0x1982,

        ITEMID_FORGE_7 = 0x1983,

        ITEMID_FORGE_8 = 0x1984,

        ITEMID_FORGE_9 = 0x1985,

        ITEMID_BELLOWS_5 = 0x1986,

        ITEMID_BELLOWS_6 = 0x1987,

        ITEMID_BELLOWS_7 = 0x1988,

        ITEMID_BELLOWS_8 = 0x1989,

        ITEMID_FORGE_10 = 0x198a,

        ITEMID_FORGE_11 = 0x198b,

        ITEMID_FORGE_12 = 0x198c,

        ITEMID_FORGE_13 = 0x198d,

        ITEMID_FORGE_14 = 0x198e,

        ITEMID_FORGE_15 = 0x198f,

        ITEMID_FORGE_16 = 0x1990,

        ITEMID_FORGE_17 = 0x1991,

        ITEMID_BELLOWS_9 = 0x1992,

        ITEMID_BELLOWS_10 = 0x1993,

        ITEMID_BELLOWS_11 = 0x1994,

        ITEMID_BELLOWS_12 = 0x1995,

        ITEMID_FORGE_18 = 0x1996,

        ITEMID_FORGE_19 = 0x1997,

        ITEMID_FORGE_20 = 0x1998,

        ITEMID_FORGE_21 = 0x1999,

        ITEMID_FORGE_22 = 0x199a,

        ITEMID_FORGE_23 = 0x199b,

        ITEMID_FORGE_24 = 0x199c,

        ITEMID_FORGE_25 = 0x199d,

        ITEMID_BELLOWS_13 = 0x199e,

        ITEMID_BELLOWS_14 = 0x199f,

        ITEMID_BELLOWS_15 = 0x19a0,

        ITEMID_BELLOWS_16 = 0x19a1,

        ITEMID_FORGE_26 = 0x19a2,

        ITEMID_FORGE_27 = 0x19a3,

        ITEMID_FORGE_28 = 0x19a4,

        ITEMID_FORGE_29 = 0x19a5,

        ITEMID_FORGE_30 = 0x19a6,

        ITEMID_FORGE_31 = 0x19a7,

        ITEMID_FORGE_32 = 0x19a8,

        ITEMID_FORGE_33 = 0x19a9,



        ITEMID_FIRE = 0x19ab,



        ITEMID_ORE_1 = 0x19b7, // can't mine this, it's just leftover from smelting

        ITEMID_ORE_3 = 0x19b8,

        ITEMID_ORE_4 = 0x19b9,

        ITEMID_ORE_2 = 0x19ba,



        ITEMID_PLANT_HAY1 = 0x1a92,

        ITEMID_PLANT_HAY2,

        ITEMID_PLANT_HAY3,

        ITEMID_PLANT_HAY4,

        ITEMID_PLANT_HAY5 = 0x1a96,



        ITEMID_PLANT_FLAX1 = 0x1a99,

        ITEMID_PLANT_FLAX2 = 0x1a9a,

        ITEMID_PLANT_FLAX3 = 0x1a9b,



        ITEMID_FRUIT_FLAX1 = 0x1a9c,

        ITEMID_FRUIT_FLAX2 = 0x1a9d,

        ITEMID_FRUIT_HOPS = 0x1aa2,



        ITEMID_PLANT_HOPS1 = 0x1a9e,

        ITEMID_PLANT_HOPS2 = 0x1a9f,

        ITEMID_PLANT_HOPS3 = 0x1aa0,

        ITEMID_PLANT_HOPS4 = 0x1aa1,



        ITEMID_MOONGATE_FX_RED = 0x1ae5,

        ITEMID_MOONGATE_FX_BLUE = 0x1af3,



        ITEMID_LEAVES1 = 0x1b1f,

        ITEMID_LEAVES2 = 0x1b20,

        ITEMID_LEAVES3 = 0x1b21,

        ITEMID_LEAVES4 = 0x1b22,



        ITEMID_HOLE = 0x1b71,



        ITEMID_FEATHERS1 = 0x1bd1,

        ITEMID_FEATHERS3 = 0x1bd2,

        ITEMID_FEATHERS6 = 0x1bd3,

        ITEMID_SHAFTS1 = 0x1bd4,

        ITEMID_SHAFTS3 = 0x1bd5,

        ITEMID_SHAFTSX = 0x1bd6,

        ITEMID_LUMBER1 = 0x1bd7,    // boards



        ITEMID_LOG_1 = 0x1bdd,

        ITEMID_LOG_2 = 0x1bde,

        ITEMID_LOG_3 = 0x1bdf,

        ITEMID_LOG_4 = 0x1be0,

        ITEMID_LOG_5 = 0x1be1,

        ITEMID_LOG_6 = 0x1be2,



        ITEMID_INGOT_COPPER = 0x1be3,

        ITEMID_INGOT_GOLD = 0x1be9,

        ITEMID_INGOT_IRON = 0x1bef,

        ITEMID_INGOT_SILVER = 0x1bf5,   //

        ITEMID_INGOTX = 0x1BFA,

        ITEMID_XBolt = 0x1bfb,

        ITEMID_XBoltX = 0x1BFE,



        ITEMID_BOOK_OFTRUTH = 0x1c13,



        ITEMID_FOOD_FISH2 = 0x1e1c, // ??? whole fish ?

        ITEMID_BOOK_X = 0x1e20,



        ITEMID_PICKPOCKET_NS2 = 0x1e2c,

        ITEMID_PICKPOCKET_EW2 = 0x1e2d,



        ITEMID_Bulletin1 = 0x1e5e,  // secure trades are here also. bboard

        ITEMID_Bulletin2 = 0x1e5f,

        ITEMID_WorldGem = 0x1ea7,   // Typically a spawn item

        ITEMID_CRATE6 = 0x1e80,

        ITEMID_CRATE6_2 = 0x1e81,   // flipped crate



        ITEMID_TINKER = 0x1ebc,

        ITEMID_FRUIT_WHEAT = 0x1ebd,

        ITEMID_SPROUT_WHEAT1 = 0x1ebe,

        ITEMID_SPROUT_WHEAT2 = 0x1ebf,



        ITEMID_PICKPOCKET_NS = 0x1ec0,

        ITEMID_PICKPOCKET_NS_FX,

        ITEMID_PICKPOCKET_EW = 0x1ec3,

        ITEMID_PICKPOCKET_EW_FX,



        ITEMID_GEM_LIGHT1 = 0x1ecd,

        ITEMID_GEM_LIGHT6 = 0x1ed2,



        ITEMID_DOOR_MAGIC_SI_NS = 0x1ed9,

        ITEMID_DOOR_MAGIC_GR_NS = 0x1ee2,

        ITEMID_DOOR_MAGIC_SI_EW = 0x1EEc,   // not 1eeb

        ITEMID_DOOR_MAGIC_GR_EW = 0x1ef4,



        ITEMID_SHIRT_FANCY = 0x1EFD,



        ITEMID_ROBE = 0x1F03,

        ITEMID_HELM_ORC = 0x1f0b,

        ITEMID_RECALLRUNE = 0x1f14,



        ITEMID_BEDROLL_O_W = 0x1f24,    // open

        ITEMID_BEDROLL_O_E,

        ITEMID_BEDROLL_O_N,

        ITEMID_BEDROLL_O_S = 0x1f27,



        ITEMID_SCROLL_1 = 0x1f2d,   // Reactive armor.

        ITEMID_SCROLL_2,

        ITEMID_SCROLL_3,

        ITEMID_SCROLL_4,

        ITEMID_SCROLL_5,

        ITEMID_SCROLL_6,

        ITEMID_SCROLL_7,

        ITEMID_SCROLL_8,

        ITEMID_SCROLL_64 = 0x1f6c,  // summon water



        ITEMID_SCROLL_A = 0x1f6d,

        ITEMID_SCROLL_B = 0x1f6e,

        ITEMID_SCROLL_C = 0x1f6f,

        ITEMID_SCROLL_D = 0x1f70,

        ITEMID_SCROLL_E = 0x1f71,

        ITEMID_SCROLL_F = 0x1f72,



        ITEMID_BOOZE_LIQU_G1 = 0x1f85,

        ITEMID_BOOZE_LIQU_G2 = 0x1f86,

        ITEMID_BOOZE_LIQU_G3 = 0x1f87,

        ITEMID_BOOZE_LIQU_G4 = 0x1f88,

        ITEMID_BOOZE_WINE_G1 = 0x1f8d,

        ITEMID_BOOZE_WINE_G2 = 0x1f8e,

        ITEMID_BOOZE_WINE_G3 = 0x1f8f,

        ITEMID_BOOZE_WINE_G4 = 0x1f90,

        ITEMID_BOOZE_ALE_P1 = 0x1f95,

        ITEMID_BOOZE_ALE_P2 = 0x1f96,

        ITEMID_BOOZE_LIQU_P1 = 0x1f99,

        ITEMID_BOOZE_LIQU_P2 = 0x1f9a,

        ITEMID_BOOZE_WINE_P1 = 0x1f9b,

        ITEMID_BOOZE_WINE_P2 = 0x1f9c,



        ITEMID_MOONGATE_SPHERE = 0x1fd4,



        ITEMID_DOOR_BAR_METAL = 0x1fed,



        ITEMID_CORPSE = 0x2006, // This is all corpses.



        ITEMID_MEMORY = 0x2007, // NonGen Marker.

        ITEMID_NPC_1 = 0x2008,  // NPC peasant object ?

        ITEMID_NPC_X = 0x2036,  // NPC



        ITEMID_HAIR_SHORT = 0x203B,

        ITEMID_HAIR_LONG = 0x203C,

        ITEMID_HAIR_PONYTAIL = 0x203D,

        ITEMID_HAIR_MOHAWK = 0x2044,

        ITEMID_HAIR_PAGEBOY = 0x2045,

        ITEMID_HAIR_BUNS = 0x2046,

        ITEMID_HAIR_AFRO = 0x2047,

        ITEMID_HAIR_RECEDING = 0x2048,

        ITEMID_HAIR_2_PIGTAILS = 0x2049,

        ITEMID_HAIR_TOPKNOT = 0x204A,   // KRISNA



        ITEMID_BEARD_LONG = 0x203E,

        ITEMID_BEARD_SHORT = 0x203F,

        ITEMID_BEARD_GOATEE = 0x2040,

        ITEMID_BEARD_MOUSTACHE = 0x2041,

        ITEMID_BEARD_SH_M = 0x204B,

        ITEMID_BEARD_LG_M = 0x204C,

        ITEMID_BEARD_GO_M = 0x204D, // VANDYKE



        ITEMID_DEATHSHROUD = 0x204E,

        ITEMID_GM_ROBE = 0x204f,



        ITEMID_RHAND_POINT_NW = 0x2053, // point nw on the map.

        ITEMID_RHAND_POINT_W = 0x205a,



        ITEMID_HAND_POINT_NW = 0x206a,  // point nw on the map.

        ITEMID_HAND_POINT_W = 0x2071,



        ITEMID_SPELL_1 = 0x2080,

        ITEMID_SPELL_6 = 0x2085,    // light or night sight.

        ITEMID_SPELL_64 = 0x20bf,



        ITEMID_SPELL_CIRCLE1 = 0x20c0,

        ITEMID_SPELL_CIRCLE8 = 0x20c7,



        // Item equiv of creatures.

        ITEMID_TRACK_BEGIN = 0x20c8,

        ITEMID_TRACK_ETTIN = 0x20c8,

        ITEMID_TRACK_MAN_NAKED = 0x20cd,

        ITEMID_TRACK_ELEM_EARTH = 0x20d7,

        ITEMID_TRACK_OGRE = 0x20df,

        ITEMID_TRACK_TROLL = 0x20e9,

        ITEMID_TRACK_ELEM_AIR = 0x20ed,

        ITEMID_TRACK_ELEM_FIRE = 0x20f3,

        ITEMID_TRACK_SEASERP = 0x20fe,

        ITEMID_TRACK_WISP = 0x2100,

        ITEMID_TRACK_MAN = 0x2106,

        ITEMID_TRACK_WOMAN = 0x2107,

        ITEMID_TRACK_ELEM_WATER = 0x210b,

        ITEMID_TRACK_HORSE = 0x2120,

        ITEMID_TRACK_RABBIT = 0x2125,

        ITEMID_TRACK_PACK_HORSE = 0x2126,

        ITEMID_TRACK_PACK_LLAMA = 0x2127,

        ITEMID_TRACK_END = 0x213e,



        ITEMID_VENDOR_BOX = 0x2af8, // Vendor container



        ITEMID_ROCK_4_LO = 0x3421,

        ITEMID_ROCK_4_HI = 0x3435,

        ITEMID_ROCK_5_LO = 0x3486,

        ITEMID_ROCK_5_HI = 0x348f,

        ITEMID_ROCK_6_LO = 0x34ac,

        ITEMID_ROCK_6_HI = 0x34b4,



        // effects.

        ITEMID_FX_SPLASH = 0x352d,



        ITEMID_GAME1_CHECKER = 0x3584,  // white

        ITEMID_GAME1_BISHOP = 0x3585,

        ITEMID_GAME1_ROOK = 0x3586,

        ITEMID_GAME1_QUEEN = 0x3587,

        ITEMID_GAME1_KNIGHT = 0x3588,

        ITEMID_GAME1_PAWN = 0x3589,

        ITEMID_GAME1_KING = 0x358a,



        ITEMID_GAME2_CHECKER = 0x358b,  // brown

        ITEMID_GAME2_BISHOP = 0x358c,

        ITEMID_GAME2_ROOK = 0x358d,

        ITEMID_GAME2_QUEEN = 0x358e,

        ITEMID_GAME2_KNIGHT = 0x358f,

        ITEMID_GAME2_PAWN = 0x3590,

        ITEMID_GAME2_KING = 0x3591,



        ITEMID_GAME_HI = 0x35a1,    // ?



        ITEMID_FX_EXPLODE_3 = 0x36b0,

        ITEMID_FX_EXPLODE_2 = 0x36bd,

        ITEMID_FX_EXPLODE_1 = 0x36ca,

        ITEMID_FX_FIRE_BALL = 0x36d4,

        ITEMID_FX_MAGIC_ARROW = 0x36e4,

        ITEMID_FX_FIRE_BOLT = 0x36f4, // fire snake

        ITEMID_FX_EXPLODE_BALL = 0x36fe, // Not used.

        ITEMID_FX_FLAMESTRIKE = 0x3709,

        ITEMID_FX_TELE_VANISH = 0x372A, // teleport vanish

        ITEMID_FX_SPELL_FAIL = 0x3735,

        ITEMID_FX_BLESS_EFFECT = 0x373A,

        ITEMID_FX_CURSE_EFFECT = 0x374A,

        ITEMID_FX_SPARK_EFFECT = 0x375A,    // UNUSED

        ITEMID_FX_HEAL_EFFECT = 0x376A,

        ITEMID_FX_ADVANCE_EFFECT = 0x3779,  // sparkle.

        ITEMID_FX_BLUEMOONSTART = 0x3789,   // ? swirl "death vortex"

        ITEMID_FX_ENERGY_BOLT = 0x379f,

        ITEMID_FX_BLADES_EMERGE = 0x37a0,   // unused

        ITEMID_FX_GLOW = 0x37b9,    // unused

        ITEMID_FX_GLOW_SPIKE = 0x37c3,  // unused "glow" spike

        ITEMID_FX_DEATH_FUNNEL = 0x37cc,    // "Death votex" funnel

        ITEMID_FX_BLADES = 0x37eb,

        ITEMID_FX_STATIC = 0x3818,  // unused. pink static.

        ITEMID_FX_POISON_F_EW = 0x3914,

        ITEMID_FX_POISON_F_1 = 0x3915,

        ITEMID_FX_POISON_F_NS = 0x3920,

        ITEMID_FX_ENERGY_F_EW = 0x3947,

        ITEMID_FX_ENERGY_F_NS = 0x3956,

        ITEMID_FX_PARA_F_EW = 0x3967,

        ITEMID_FX_PARA_F_NS = 0x3979,

        ITEMID_FX_FIRE_F_EW = 0x398c,   // E/W

        ITEMID_FX_FIRE_F_NS = 0x3996,   // N/S



        ITEMID_SHIP_TILLER_1 = 0x3e4a,

        ITEMID_SHIP_TILLER_2 = 0x3e4b,

        ITEMID_SHIP_TILLER_3 = 0x3e4c,

        ITEMID_SHIP_TILLER_4 = 0x3e4d,

        ITEMID_SHIP_TILLER_5 = 0x3e4e,

        ITEMID_SHIP_TILLER_6,

        ITEMID_SHIP_TILLER_7 = 0x3e50,

        ITEMID_SHIP_TILLER_8,

        ITEMID_SHIP_TILLER_12 = 0x3e55,



        ITEMID_SHIP_PLANK_S2_O = 0x3e84,

        ITEMID_SHIP_PLANK_S2_C = 0x3e85,

        ITEMID_SHIP_PLANK_S_O = 0x3e86,

        ITEMID_SHIP_PLANK_S_C = 0x3e87,

        ITEMID_SHIP_PLANK_N_O = 0x3e89,

        ITEMID_SHIP_PLANK_N_C = 0x3e8a,



        ITEMID_M_FIRE_STEED = 0x3e9e,



        ITEMID_M_HORSE1 = 0x3E9F,   // horse item when ridden

        ITEMID_M_HORSE2 = 0x3EA0,

        ITEMID_M_HORSE3 = 0x3EA1,

        ITEMID_M_HORSE4 = 0x3EA2,

        ITEMID_M_OSTARD_DES = 0x3ea3,   // t2A

        ITEMID_M_OSTARD_Frenz = 0x3ea4, // t2A

        ITEMID_M_OSTARD_For = 0x3ea5,   // t2A

        ITEMID_M_LLAMA = 0x3ea6,    // t2A



        ITEMID_M_DARK_STEED = 0x3ea7,

        ITEMID_M_SILVER_STEED = 0x3ea8,

        ITEMID_M_ETHEREAL_HORSE = 0x3eaa,

        ITEMID_M_ETHEREAL_LLAMA = 0x3eab,

        ITEMID_M_ETHEREAL_OSTARD = 0x3eac,

        ITEMID_M_KIRIN = 0x3ead,

        ITEMID_M_MINAX_WARHORSE = 0x3eaf,



        ITEMID_M_SHADOWLORD_WARHORSE = 0x3eb0,

        ITEMID_M_MAGECOUNCIL_WARHORSE = 0x3eb1,

        ITEMID_M_BRITANNIAN_WARHORSE = 0x3eb2,

        ITEMID_M_SEAHORSE = 0x3eb3, // 3D client only.  What's the ID?

        ITEMID_M_UNICORN = 0x3eb4,

        ITEMID_M_NIGHTMARE = 0x3eb7,

        ITEMID_M_RIDGEBACK2 = 0x3eb8,

        ITEMID_M_RIDGEBACK1 = 0x3eba,

        ITEMID_M_SKELETAL_MOUNT = 0x3ebb,

        ITEMID_M_BEETLE = 0x3ebc,

        ITEMID_M_SWAMPDRAGON1 = 0x3ebd,

        ITEMID_M_SWAMPDRAGON2 = 0x3ebe,



        ITEMID_SHIP_PLANK_E_C = 0x3ea9,

        ITEMID_SHIP_PLANK_W_C = 0x3eb1,

        ITEMID_SHIP_PLANK_E2_C = 0x3eb2,

        ITEMID_SHIP_PLANK_E_O = 0x3ed3,

        ITEMID_SHIP_PLANK_E2_O = 0x3ed4,

        ITEMID_SHIP_PLANK_W_O = 0x3ed5,



        ITEMID_SHIP_HATCH_E = 0x3e65,   // for an east bound ship.

        ITEMID_SHIP_HATCH_W = 0x3e93,

        ITEMID_SHIP_HATCH_N = 0x3eae,

        ITEMID_SHIP_HATCH_S = 0x3eb9,



        ITEMID_CORPSE_1 = 0x3d64,   // 'dead orc captain'

        ITEMID_CORPSE_2,    // 'corpse of orc'

        ITEMID_CORPSE_3,    // 'corpse of skeleton

        ITEMID_CORPSE_4,    // 'corpse'

        ITEMID_CORPSE_5,    // 'corpse'

        ITEMID_CORPSE_6,    // 'deer corpse'

        ITEMID_CORPSE_7,    // 'wolf corpse'

        ITEMID_CORPSE_8,    // 'corpse of rabbit'



        // Large composite objects here.

        ITEMID_MULTI = 0x4000,

        ITEMID_SHIP1_N = 0x4000,

        ITEMID_SHIP1_E = 0x4001,

        ITEMID_SHIP1_S = 0x4002,

        ITEMID_SHIP1_W = 0x4003,

        ITEMID_SHIP2_N = 0x4004,

        ITEMID_SHIP2_E,

        ITEMID_SHIP2_S,

        ITEMID_SHIP2_W,

        ITEMID_SHIP3_N = 0x4008,

        ITEMID_SHIP4_N = 0x400c,

        ITEMID_SHIP5_N = 0x4010,

        ITEMID_SHIP6_N = 0x4014,

        ITEMID_SHIP6_E,

        ITEMID_SHIP6_S,

        ITEMID_SHIP6_W = 0x4017,



        ITEMID_HOUSE = 0x4064,

        ITEMID_HOUSE_FORGE = 0x4065,

        ITEMID_HOUSE_STONE = 0x4066,

        ITEMID_TENT_BLUE = 0x4070,

        ITEMID_TENT_GREEN = 0x4072,

        ITEMID_3ROOM = 0x4074,  // 3 room house

        ITEMID_2STORY_STUKO = 0x4076,

        ITEMID_2STORY_SAND = 0x4078,

        ITEMID_TOWER = 0x407a,

        ITEMID_KEEP = 0x407C,   // keep

        ITEMID_CASTLE = 0x407E, // castle 7f also.

        ITEMID_LARGESHOP = 0x408c,  // in verdata.mul file.

        ITEMID_MULTI_EXT_1 = 0x4BB8,

        ITEMID_MULTI_EXT_2 = 0x5388,    // minax tower.



        //ITEMID_MULTI_MAX = (ITEMID_MULTI + MULTI_QTY - 1),  // ??? this is higher than next !



        // These overlap for now damnit !!! fix this.

        // Special scriptable objects defined after this.

        ITEMID_SCRIPT = 0x4100, // Script objects beyond here.

        // ITEMID_SCRIPT1		= 0x5000,	// This should be the first scripted object really

        // ITEMID_M_CAGE		= 0x50a0,

        ITEMID_SCRIPT2 = 0x6000,    // Safe area for server admins.



        ITEMID_QTY = 0xFFFE,



        // Put named items here.



        ITEMID_TEMPLATE = 0xFFFF,   // container item templates are beyond here.

    };

    public struct COLOR_TYPE
    {
        public ushort Value { get; }

        public COLOR_TYPE(ushort val)
        {
            Value = val;
        }

        public static implicit operator COLOR_TYPE(ushort val) => new COLOR_TYPE(val);
        public static implicit operator COLOR_TYPE(int val) => new COLOR_TYPE((ushort)val);
        public static implicit operator int(COLOR_TYPE val) => (int)val.Value;
        public static implicit operator bool(COLOR_TYPE val) => val.Value != 0;

        public static COLOR_TYPE operator &(COLOR_TYPE val1, COLOR_TYPE val2) => val1.Value & val2.Value;
    }

    public struct HUE_TYPE
    {
        public ushort Value { get; }

        public HUE_TYPE(ushort val)
        {
            Value = val;
        }

        public static implicit operator HUE_TYPE(ushort val) => new HUE_TYPE(val);
        public static implicit operator HUE_TYPE(int val) => new HUE_TYPE((ushort)val);
        public static implicit operator int(HUE_TYPE val) => (int)val.Value;
        public static implicit operator HUE_TYPE(HUE_CODE val)
        {
            int i = (int)val;
            return i;
        }

        public static implicit operator bool(HUE_TYPE val) => val.Value != 0;

        public static HUE_TYPE operator &(HUE_TYPE val1, HUE_TYPE val2) => val1.Value & val2.Value;
    }

    public enum HUE_CODE

    {

        HUE_DEFAULT = 0x0000,



        HUE_BLACK = 0x0001,

        HUE_BLUE_LOW = 0x0002,  // lowest dyeable color.

        HUE_BLUE_NAVY = 0x0003,

        HUE_INDIGO_DARK = 0x0007,

        HUE_INDIGO = 0x0008,

        HUE_INDIGO_LIGHT = 0x0009,

        HUE_VIOLET_DARK = 0x000c,

        HUE_VIOLET = 0x000d,

        HUE_VIOLET_LIGHT = 0x000e,

        HUE_MAGENTA_DARK = 0x0011,

        HUE_MAGENTA = 0x0012,

        HUE_MAGENTA_LIGHT = 0x0013,

        HUE_RED_DARK = 0x0020,

        HUE_RED = 0x0022,

        HUE_RED_LIGHT = 0x0023,

        HUE_ORANGE_DARK = 0x002a,

        HUE_ORANGE = 0x002b,

        HUE_ORANGE_LIGHT = 0x002c,

        HUE_YELLOW_DARK = 0x0034,

        HUE_YELLOW = 0x0035,

        HUE_YELLOW_LIGHT = 0x0036,

        HUE_GREEN_DARK = 0x003e,

        HUE_GREEN = 0x003f,

        HUE_GREEN_LIGHT = 0x0040,

        HUE_CYAN_DARK = 0x0057,

        HUE_CYAN = 0x0058,

        HUE_CYAN_LIGHT = 0x0059,



        HUE_BLUE_DARK = 0x0061,

        HUE_BLUE = 0x0062,

        HUE_BLUE_LIGHT = 0x0063,



        HUE_GRAY_DARK = 0x0386, // sphere range.

        HUE_GRAY = 0x0387,

        HUE_GRAY_LIGHT = 0x0388,



        HUE_TEXT_DEF = 0x03b2,  // light sphere color.



        HUE_DYE_HIGH = 0x03e9,  // highest dyeable color = 1z001



        HUE_SKIN_LOW = 0x03EA,

        HUE_SKIN_HIGH = 0x0422,



        HUE_SPECTRAL1_LO = 0x0423,  // unassigned color range i think.

        HUE_SPECTRAL1_HI = 0x044d,



        // Strange mixed colors.

        HUE_HAIR_LOW = 0x044e,  // valorite

        HUE_BLACKMETAL = 0x044e,    // Sort of a dark sphere.

        HUE_GOLDMETAL = 0x046e,

        HUE_BLUE_SHIMMER = 0x0480,  // is a shimmering blue...like a water elemental blue...

        HUE_WHITE = 0x0481, // white....yup! a REAL white...

        HUE_STONE = 0x0482, // kinda like rock when you do it to a monster.....mabey for the Stone Harpy?

        HUE_HAIR_HIGH = 0x04ad,



        HUE_SPECTRAL = 0x0631, // Add more colors!



        HUE_MASK_LO = 0x07FF,   // mask for items. (not really a valid thing to do i know)



        HUE_QTY = 3000, // 0x0bb8 Number of valid colors in hue table.

        HUE_MASK_HI = 0x0FFF,



        HUE_TRANSLUCENT = 0x4000,   // almost invis. may crash if not equipped ?

        HUE_UNDERWEAR = 0x8000, // Only can be used on humans.



        HUE_MATCH_HAIR = 0xFFFD, // match_hair

        HUE_MATCH_SHIRT = 0xFFFE,   // special code.

    };

    public enum FONT_TYPE

    {

        FONT_BOLD,      // 0 - Bold Text = Large plain filled block letters.

        FONT_SHAD,      // 1 - Text with shadow = small sphere

        FONT_BOLD_SHAD, // 2 - Bold+Shadow = Large Sphere block letters.

        FONT_NORMAL,    // 3 - Normal (default) = Filled block letters.

        FONT_GOTH,      // 4 - Gothic = Very large blue letters.

        FONT_ITAL,      // 5 - Italic Script

        FONT_SM_DARK,   // 6 - Small Dark Letters = small Blue

        FONT_COLOR,     // 7 - Colorful Font (Buggy?) = small Sphere (hazy)

        FONT_RUNE,      // 8 - Rune font (Only use capital letters with this!)

        FONT_SM_LITE,   // 9 - Small Light Letters = small roman sphere font.

        FONT_QTY,

    }


    public enum SKILL_TYPE // List of skill numbers (things that can be done at a given time)

    {

        SKILL_NONE = -1,



        SKILL_First = 0,

        SKILL_ALCHEMY = SKILL_First,

        SKILL_ANATOMY,

        SKILL_ANIMALLORE,

        SKILL_ITEMID,

        SKILL_ARMSLORE,

        SKILL_PARRYING,

        SKILL_BEGGING,

        SKILL_BLACKSMITHING,

        SKILL_BOWCRAFT,

        SKILL_PEACEMAKING,

        SKILL_CAMPING,  // 10

        SKILL_CARPENTRY,

        SKILL_CARTOGRAPHY,

        SKILL_COOKING,

        SKILL_DETECTINGHIDDEN,

        SKILL_ENTICEMENT,

        SKILL_EVALINT,

        SKILL_HEALING,

        SKILL_FISHING,

        SKILL_FORENSICS,

        SKILL_HERDING,  // 20

        SKILL_HIDING,

        SKILL_PROVOCATION,

        SKILL_INSCRIPTION,

        SKILL_LOCKPICKING,

        SKILL_MAGERY,       // 25

        SKILL_MAGICRESISTANCE,

        SKILL_TACTICS,

        SKILL_SNOOPING,

        SKILL_MUSICIANSHIP,

        SKILL_POISONING,    // 30

        SKILL_ARCHERY,

        SKILL_SPIRITSPEAK,

        SKILL_STEALING,

        SKILL_TAILORING,

        SKILL_TAMING,

        SKILL_TASTEID,

        SKILL_TINKERING,

        SKILL_TRACKING,

        SKILL_VETERINARY,

        SKILL_SWORDSMANSHIP,    // 40

        SKILL_MACEFIGHTING,

        SKILL_FENCING,

        SKILL_WRESTLING,        // 43

        SKILL_LUMBERJACKING,

        SKILL_MINING,

        SKILL_MEDITATION,

        SKILL_Stealth,          // 47

        SKILL_RemoveTrap,       // 48

        SKILL_NECROMANCY,

        SKILL_Last = SKILL_NECROMANCY,

//        SKILL_QTY,  // 50



        // Actions a npc will perform. (no need to track skill level for these)

        NPCACT_FOLLOW_TARG = 100,   // 100 = following a char.

        NPCACT_STAY,            // 101

        NPCACT_GOTO,            // 102 = Go to a location x,y. Pet command

        NPCACT_WANDER,          // 103 = Wander aimlessly.

        NPCACT_LOOKING,         // 104 = just look around intently.

        NPCACT_FLEE,            // 105 = Run away from target. m_Act.m_Targ. (Fear spell?)

        NPCACT_TALK,            // 106 = Talking to my target. m_Act.m_Targ

        NPCACT_TALK_FOLLOW,     // 107 = m_Act.m_Targ

        NPCACT_GUARD_TARG,      // 108 = Guard a targetted object. m_Act.m_Targ

        NPCACT_GO_HOME,         // 109 =

        NPCACT_GO_FETCH,        // 110 = Pick up a thing. Command as pet. m_Act.m_Targ

        NPCACT_BREATH,          // 111 = Using breath weapon. on m_Act.m_Targ

        NPCACT_RIDDEN,          // 112 = Being ridden or shrunk as figurine.

        NPCACT_EATING,          // 113 = Animate the eating action.

        NPCACT_LOOTING,         // 114 = Looting a corpse. m_Act.m_Targ

        NPCACT_THROWING,        // 115 = Throwing a stone at m_Act.m_Targ.

        NPCACT_GO_OBJ,          // 116 = walking to an object i want. m_Act.m_Targ

        NPCACT_TRAINING,        // 117 = using a training dummy etc.

        NPCACT_Napping,         // 118 = just snoozong a little bit, but not sleeping.

        NPCACT_UNConscious,     // 119 = injured in some way.

        NPCACT_Sleeping,        // 120 = just normal sleep.

        NPCACT_Healing,         // 121 = we have a bandage on and we can really do much.

        NPCACT_OneClickCmd,     // 122 = everything i touch will get this verb

        NPCACT_ScriptBook,      // 123 = running a command from the script book.

        NPCACT_Stunned,         // 124 = momentarily stunned. (Cant do anything else)

        NPCACT_QTY,

    };

}
