using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class FactionXenotypeRandomizerSettings : ModSettings
    {
        public static bool AllowMixedXenotypes = false;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("FactionXenotypeRandomizer_AllowMixedXenotypes".Translate(), ref AllowMixedXenotypes, "FactionXenotypeRandomizer_AllowMixedXenotypesDesc".Translate());

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref AllowMixedXenotypes, "AllowMixedXenotypes", false);
        }
    }
}
