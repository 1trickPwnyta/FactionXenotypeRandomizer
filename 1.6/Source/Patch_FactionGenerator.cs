using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace FactionXenotypeRandomizer
{
    public static class PatchUtility_FactionGenerator
    {
        public static void SetFactionXenotype(Faction faction)
        {
            if (!faction.IsPlayer && faction.def.displayInFactionSelection)
            {
                if (faction.def is CustomFactionDef customDef)
                {
                    customDef.SetFactionXenotype(faction);
                }
            }
        }
    }

    [HarmonyPatch(typeof(FactionGenerator))]
    [HarmonyPatch(nameof(FactionGenerator.NewGeneratedFaction))]
    [HarmonyPatch(new[] { typeof(PlanetLayer), typeof(FactionGeneratorParms) })]
    public static class Patch_FactionGenerator_NewGeneratedFaction
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Stfld && (FieldInfo)instruction.operand == AccessTools.Field(typeof(Faction), nameof(Faction.def)))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchUtility_FactionGenerator), nameof(PatchUtility_FactionGenerator.SetFactionXenotype)));
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
