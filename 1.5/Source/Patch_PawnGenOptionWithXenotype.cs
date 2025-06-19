using HarmonyLib;
using RimWorld;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(PawnGenOptionWithXenotype))]
    [HarmonyPatch("get_Cost")]
    public static class Patch_PawnGenOptionWithXenotype
    {
        public static void Postfix(PawnGenOptionWithXenotype __instance, ref float __result)
        {
            CustomXenotype xenotype = __instance.GetCustomXenotype();
            if (xenotype != null )
            {
                __result *= xenotype.GetCombatPowerFactor();
            }
        }
    }
}
