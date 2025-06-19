using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace FactionXenotypeRandomizer
{
    // Patched manually in mod initializer
    public static class Patch_PawnApparelGenerator_GenerateStartingApparelFor
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo info && info == typeof(FloatRange).Method("get_RandomInRange"))
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_PawnApparelGenerator).Method(nameof(PatchUtility_PawnApparelGenerator.GetRandomApparelMoney)));
                    continue;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_PawnApparelGenerator
    {
        public static float GetRandomApparelMoney(this Pawn pawn, PawnGenerationRequest request)
        {
            CustomXenotype xenotype = request.ForcedCustomXenotype ?? pawn.genes?.CustomXenotype;
            if (xenotype != null && xenotype.genes.Contains(DefDatabase<GeneDef>.GetNamed("NakedSpeed")))
            {
                return 0f;
            }
            else
            {
                return pawn.kindDef.apparelMoney.RandomInRange;
            }
        }
    }
}
