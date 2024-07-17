using HarmonyLib;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class FactionXenotypeRandomizerMod : Mod
    {
        public const string PACKAGE_ID = "factionxenotyperandomizer.1trickPwnyta";
        public const string PACKAGE_NAME = "Faction Xenotype Randomizer";

        public FactionXenotypeRandomizerMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
