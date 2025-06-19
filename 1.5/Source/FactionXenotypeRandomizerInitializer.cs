using HarmonyLib;
using RimWorld;
using Verse;

namespace FactionXenotypeRandomizer
{
    [StaticConstructorOnStartup]
    public static class FactionXenotypeRandomizerInitializer
    {
        static FactionXenotypeRandomizerInitializer()
        {
            Harmony harmony = new Harmony(FactionXenotypeRandomizerMod.PACKAGE_ID);
            harmony.Patch(typeof(PawnApparelGenerator).Method(nameof(PawnApparelGenerator.GenerateStartingApparelFor)), null, null, typeof(Patch_PawnApparelGenerator_GenerateStartingApparelFor).Method(nameof(Patch_PawnApparelGenerator_GenerateStartingApparelFor.Transpiler)));
        }
    }
}
