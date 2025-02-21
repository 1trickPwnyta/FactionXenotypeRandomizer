using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(Faction))]
    [HarmonyPatch(nameof(Faction.ExposeData))]
    public static class Patch_Faction
    {
        public static void Prefix(Faction __instance)
        {
            bool isCustom = __instance.def is CustomFactionDef;
            Scribe_Values.Look(ref isCustom, "useCustomDef");
            if (isCustom)
            {
                PatchUtility_Faction.factionsWithCustomDef.Add(__instance);
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo info && info == typeof(Scribe_Defs).Method(nameof(Scribe_Defs.Look), null, new[] { typeof(FactionDef) }))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    instruction.operand = typeof(PatchUtility_Faction).Method(nameof(PatchUtility_Faction.ExposeFactionDef));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Faction
    {
        public static HashSet<Faction> factionsWithCustomDef = new HashSet<Faction>();

        public static void ExposeFactionDef(ref FactionDef value, string label, Faction faction)
        {
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                if (factionsWithCustomDef.Contains(faction))
                {
                    Scribe_Deep.Look(ref value, label);
                }
                else
                {
                    Scribe_Defs.Look(ref value, label);
                }
            }
            else
            {
                if (value is CustomFactionDef)
                {
                    Scribe_Deep.Look(ref value, label);
                }
                else
                {
                    Scribe_Defs.Look(ref value, label);
                }
            }
        }
    }
}
