using HarmonyLib;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(Root_Play))]
    [HarmonyPatch(nameof(Root_Play.SetupForQuickTestPlay))]
    public static class Patch_Root_Play
    {
        public static void Prefix()
        {
            CustomFactionXenotypes.Initialize(0);
        }
    }
}
