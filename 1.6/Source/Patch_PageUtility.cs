using HarmonyLib;
using RimWorld;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(PageUtility))]
    [HarmonyPatch(nameof(PageUtility.InitGameStart))]
    public static class Patch_PageUtility
    {
        public static void Prefix()
        {
            CustomFactionXenotypes.Uninitialize();
        }
    }
}
