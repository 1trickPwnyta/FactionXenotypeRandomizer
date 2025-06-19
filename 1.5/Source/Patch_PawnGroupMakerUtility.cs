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
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(Utility).Field(nameof(Utility.lastFactionDef)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetCustomXenotype), new[] { typeof(FactionDef) }));
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
        public static void Prefix(FactionDef faction)
        {
            Utility.lastFactionDef = faction;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Newobj && instruction.operand is ConstructorInfo info && info == typeof(PawnGenOptionWithXenotype).GetConstructor(new[] { typeof(PawnGenOption), typeof(XenotypeDef), typeof(float) }))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetCustomXenotype), new[] { typeof(FactionDef) }));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.SetCustomXenotype), new[] { typeof(PawnGenOptionWithXenotype), typeof(CustomXenotype) }));
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(PawnGroupMakerUtility))]
    [HarmonyPatch(nameof(PawnGroupMakerUtility.AnyOptions))]
    public static class Patch_PawnGroupMakerUtility_AnyOptions
    {
        public static void Prefix(FactionDef faction)
        {
            Utility.lastFactionDef = faction;
        }
    }
}
