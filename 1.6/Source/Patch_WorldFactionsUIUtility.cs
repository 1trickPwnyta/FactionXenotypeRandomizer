using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(WorldFactionsUIUtility))]
    [HarmonyPatch(nameof(WorldFactionsUIUtility.DoRow))]
    public static class Patch_WorldFactionsUIUtility_DoRow
    {
        public static void Postfix(Rect rect, FactionDef factionDef)
        {
            if (CustomFactionXenotypes.ShouldShow())
            {
                if (factionDef is CustomFactionDef customDef)
                {
                    if (Widgets.ButtonImage(new Rect(rect.width - 24f - 6f - 24f - 6f, rect.y, 24f, 24f), customDef.OptionIcon, true, "FactionXenotypeRandomizer_FactionXenotype".Translate(customDef.OptionName)))
                    {
                        Find.WindowStack.Add(new FloatMenu(Utility.CustomXenotypes.Select(c => new FloatMenuOption(c.name, () =>
                        {
                            customDef.option = CustomFactionOption.CustomXenotype;
                            customDef.customXenotype = c;
                        }, c.IconDef.Icon, Color.white))
                        .Prepend(new FloatMenuOption(CustomFactionOption.RandomXenotype.GetLabel(), Utility.CustomXenotypes.Any() ? () =>
                        {
                            customDef.option = CustomFactionOption.RandomXenotype;
                            customDef.customXenotype = null;
                        } : (Action)null, CustomFactionOption.RandomXenotype.GetIcon(), Color.white))
                        .Prepend(new FloatMenuOption(CustomFactionOption.RandomMutant.GetLabel(), () =>
                        {
                            customDef.option = CustomFactionOption.RandomMutant;
                            customDef.customXenotype = null;
                        }, CustomFactionOption.RandomMutant.GetIcon(), Color.white))
                        .Append(new FloatMenuOption("XenotypeEditor".Translate() + "...", () =>
                        {
                            Find.WindowStack.Add(new Dialog_CreateXenotype(-1, null));
                        })).ToList()));
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(WorldFactionsUIUtility))]
    [HarmonyPatch(nameof(WorldFactionsUIUtility.DoWindowContents))]
    public static class Patch_WorldFactionsUIUtility_DoWindowContents
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = instructions.ToList();
            int index = list.FindIndex(i => i.opcode == OpCodes.Stfld && (FieldInfo)i.operand == typeof(WorldFactionsUIUtility).GetNestedType("<>c__DisplayClass8_2", BindingFlags.NonPublic).Field("localDef"));
            list.Insert(index, new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.GetPossibleCustomFactionDef))));
            return list;
        }
    }
}
