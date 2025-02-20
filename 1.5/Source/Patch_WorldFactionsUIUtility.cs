using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(WorldFactionsUIUtility))]
    [HarmonyPatch(nameof(WorldFactionsUIUtility.DoWindowContents))]
    public static class Patch_WorldFactionsUIUtility_DoWindowContents
    {
        public static void Prefix()
        {
            CustomFactionXenotypes.ResetIndex();
        }
    }

    [HarmonyPatch(typeof(WorldFactionsUIUtility))]
    [HarmonyPatch(nameof(WorldFactionsUIUtility.DoRow))]
    public static class Patch_WorldFactionsUIUtility_DoRow
    {
        public static void Postfix(Rect rect, FactionDef factionDef)
        {
            if (CustomFactionXenotypes.ShouldShow())
            {
                int index = CustomFactionXenotypes.CurrentIndex;
                CustomXenotype xenotype = CustomFactionXenotypes.GetCurrentXenotype();
                if (factionDef.IsMutant())
                {
                    if (Widgets.ButtonImage(new Rect(rect.width - 24f - 6f - 24f - 6f, rect.y, 24f, 24f), (xenotype?.IconDef ?? XenotypeIconDefOf.Basic).Icon, true, "FactionXenotypeRandomizer_FactionXenotype".Translate(xenotype?.name ?? "FactionXenotypeRandomizer_RandomMutant".Translate())))
                    {
                        Find.WindowStack.Add(new FloatMenu((typeof(CharacterCardUtility).PropertyGetter("CustomXenotypes").Invoke(null, new object[] { }) as List<CustomXenotype>).Select(c => new FloatMenuOption(c.name, () =>
                        {
                            CustomFactionXenotypes.SetXenotype(index, c);
                        }, c.IconDef.Icon, Color.white)).Prepend(new FloatMenuOption("FactionXenotypeRandomizer_RandomMutant".Translate(), () =>
                        {
                            CustomFactionXenotypes.SetXenotype(index, null);
                        }, XenotypeIconDefOf.Basic.Icon, Color.white)).ToList()));
                    }
                }
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(List<FactionDef>).Method(nameof(List<FactionDef>.RemoveAt)))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(CustomFactionXenotypes).Method(nameof(CustomFactionXenotypes.Delete)));
                }

                yield return instruction;
            }
        }
    }

    // Patched manually in mod constructor
    public static class Patch_WorldFactionsUIUtility_DoWindowContents_b__4
    {
        public static void Postfix()
        {
            CustomFactionXenotypes.Add();
        }
    }
}
