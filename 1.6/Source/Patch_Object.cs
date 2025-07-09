using HarmonyLib;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(object))]
    [HarmonyPatch(nameof(Equals))]
    [HarmonyPatch(new[] { typeof(object) })]
    public static class Patch_Object
    {
        public static void Postfix(object __instance, object obj, ref bool __result)
        {
            __result |= __instance is CustomFactionDef && __instance.Equals(obj);
            __result |= obj is CustomFactionDef && obj.Equals(__instance);
        }
    }
}
