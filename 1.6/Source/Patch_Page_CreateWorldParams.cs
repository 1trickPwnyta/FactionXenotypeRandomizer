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
        public static void Postfix(ref List<FactionDef> ___factions, List<FactionDef> ___initialFactions)
        {
            CustomFactionXenotypes.Initialize();
            ___factions = ___factions.Select(f => Utility.GetPossibleCustomFactionDef(f)).ToList();
            ___initialFactions.Clear();
            ___initialFactions.AddRange(___factions);
        }
    }
}
