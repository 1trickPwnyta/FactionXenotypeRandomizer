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
    [HarmonyPatch(nameof(WorldFactionsUIUtility.DoRow))]
    [StaticConstructorOnStartup]
    public static class Patch_WorldFactionsUIUtility_DoRow
    {
        private readonly static Texture2D DiceIcon = ContentFinder<Texture2D>.Get("UI/Icons/FactionXenotypeRandomizer_Dice");

        public static void Postfix(Rect rect, FactionDef factionDef)
        {
            if (CustomFactionXenotypes.ShouldShow())
            {
                CustomXenotype xenotype = factionDef.GetCustomXenotype();
                if (factionDef.IsMutant())
                {
                    if (Widgets.ButtonImage(new Rect(rect.width - 24f - 6f - 24f - 6f, rect.y, 24f, 24f), xenotype?.IconDef?.Icon ?? DiceIcon, true, "FactionXenotypeRandomizer_FactionXenotype".Translate(xenotype?.name ?? "FactionXenotypeRandomizer_RandomMutant".Translate())))
                    {
                        Find.WindowStack.Add(new FloatMenu((typeof(CharacterCardUtility).PropertyGetter("CustomXenotypes").Invoke(null, new object[] { }) as List<CustomXenotype>).Select(c => new FloatMenuOption(c.name, () =>
                        {
                            (factionDef as CustomFactionDef).customXenotype = c;
                        }, c.IconDef.Icon, Color.white)).Prepend(new FloatMenuOption("FactionXenotypeRandomizer_RandomMutant".Translate(), () =>
                        {
                            (factionDef as CustomFactionDef).customXenotype = null;
                        }, DiceIcon, Color.white)).ToList()));
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
