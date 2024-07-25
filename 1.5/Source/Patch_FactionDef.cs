using HarmonyLib;
using RimWorld;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(FactionDef))]
    [HarmonyPatch("get_Description")]
    public static class Patch_FactionDef
    {
        public static void Prefix(FactionDef __instance, ref string ___cachedDescription)
        {
            if (__instance.categoryTag == "Mutants" && ___cachedDescription == null)
            {
                ___cachedDescription = __instance.description;
            }
        }
    }
}
