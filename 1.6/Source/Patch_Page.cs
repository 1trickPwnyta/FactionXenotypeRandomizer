using HarmonyLib;
using RimWorld;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(Page))]
    [HarmonyPatch("DoBack")]
    public static class Patch_Page
    {
        public static void Prefix(Page __instance)
        {
            if (__instance.prev == null)
            {
                CustomFactionXenotypes.Uninitialize();
            }
        }
    }
}
