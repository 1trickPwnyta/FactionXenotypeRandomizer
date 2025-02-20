using HarmonyLib;
using RimWorld.Planet;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class FactionXenotypeRandomizerMod : Mod
    {
        public const string PACKAGE_ID = "factionxenotyperandomizer.1trickPwnyta";
        public const string PACKAGE_NAME = "Faction Xenotype Randomizer";

        public static FactionXenotypeRandomizerSettings Settings;

        public FactionXenotypeRandomizerMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<FactionXenotypeRandomizerSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            Type quickstartControllerType = AccessTools.TypeByName("HugsLib.Quickstart.QuickstartController");
            if (quickstartControllerType != null)
            {
                harmony.Patch(quickstartControllerType.Method("ApplyQuickstartConfiguration"), typeof(CompatibilityPatch_HugsLib_QuickstartController).Method(nameof(CompatibilityPatch_HugsLib_QuickstartController.Prefix)));
            }
            harmony.Patch(typeof(WorldFactionsUIUtility).GetNestedType("<>c__DisplayClass8_2", BindingFlags.NonPublic).Method("<DoWindowContents>b__4"), null, typeof(Patch_WorldFactionsUIUtility_DoWindowContents_b__4).Method(nameof(Patch_WorldFactionsUIUtility_DoWindowContents_b__4.Postfix)));

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            FactionXenotypeRandomizerSettings.DoSettingsWindowContents(inRect);
        }
    }
}
