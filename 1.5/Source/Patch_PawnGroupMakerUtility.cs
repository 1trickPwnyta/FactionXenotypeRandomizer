using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(PawnGroupMakerUtility))]
    [HarmonyPatch("CanUseOption")]
    public static class Patch_PawnGroupMakerUtility_CanUseOption
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(PawnGenOption).Method("get_Cost"))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(Utility).Field(nameof(Utility.lastFaction)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetCustomXenotype), new[] { typeof(Faction) }));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetCombatPowerFactor)));
                    yield return new CodeInstruction(OpCodes.Mul);
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(PawnGroupMakerUtility))]
    [HarmonyPatch(nameof(PawnGroupMakerUtility.GetOptions))]
    public static class Patch_PawnGroupMakerUtility_GetOptions
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Newobj && instruction.operand is ConstructorInfo info && info == typeof(PawnGenOptionWithXenotype).Constructor())
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(PawnGroupMakerParms).Field(nameof(PawnGroupMakerParms.faction)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetCustomXenotype), new[] { typeof(Faction) }));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.SetCustomXenotype), new[] { typeof(PawnGenOptionWithXenotype), typeof(CustomXenotype) }));
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
