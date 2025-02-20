using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
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
                CustomXenotype xenotype = CustomFactionXenotypes.GetCurrentXenotype();
                if (faction.def.IsMutant())
                {
                    if (xenotype == null)
                    {
                        xenotype = new CustomXenotype();
                        xenotype.inheritable = true;
                        xenotype.genes = new List<GeneDef>();
                        XenotypeRandomizer.XenotypeRandomizer.Randomize(xenotype.genes, ref xenotype.iconDef, false);
                        xenotype.name = GeneUtility.GenerateXenotypeNameFromGenes(xenotype.genes);
                    }
                    FactionXenotypeRandomizer.Current.factionXenotypes[faction] = xenotype;
                }
            }
        }
    }

    [HarmonyPatch(typeof(FactionGenerator))]
    [HarmonyPatch(nameof(FactionGenerator.GenerateFactionsIntoWorld))]
    public static class Patch_FactionGenerator_GenerateFactionsIntoWorld
    {
        public static void Prefix()
        {
            CustomFactionXenotypes.ResetIndex();
        }
    }

    [HarmonyPatch(typeof(FactionGenerator))]
    [HarmonyPatch(nameof(FactionGenerator.NewGeneratedFaction))]
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
