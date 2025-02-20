using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(Page_CreateWorldParams))]
    [HarmonyPatch("ResetFactionCounts")]
    public static class Patch_Page_CreateWorldParams
    {
        public static void Postfix(Page_CreateWorldParams __instance)
        {
            CustomFactionXenotypes.Initialize((typeof(Page_CreateWorldParams).Field("factions").GetValue(__instance) as List<FactionDef>).Where(f => f.displayInFactionSelection).Count());
        }
    }
}
