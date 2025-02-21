using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace FactionXenotypeRandomizer
{
    public static class Utility
    {
        public static Faction lastFaction = null;

        public static bool IsMutant(this FactionDef def)
        {
            return def.categoryTag == "Mutants";
        }

        public static float GetCombatPowerFactor(this CustomXenotype xenotype)
        {
            float factor = xenotype.genes.Select(g => g.GetModExtension<GeneDefModExtension>()?.combatPowerFactor ?? 1f).Aggregate((x, y) => x * y);
            Debug.Log(xenotype.name + ": " + factor);
            return factor;
        }

        public static CustomXenotype GetCustomXenotype(this Faction faction)
        {
            Dictionary<Faction, CustomXenotype> dict = FactionXenotypeRandomizer.Current.factionXenotypes;
            return dict.ContainsKey(faction) ? dict[faction] : null;
        }

        public static CustomXenotype GetCustomXenotype(this PawnGenOptionWithXenotype option)
        {
            Dictionary<PawnGenOptionWithXenotype, CustomXenotype> dict = FactionXenotypeRandomizer.Current.pawnGenOptionCustomXenotypes;
            return dict.ContainsKey(option) ? dict[option] : null;
        }

        public static void SetCustomXenotype(this Faction faction, CustomXenotype xenotype)
        {
            FactionXenotypeRandomizer.Current.factionXenotypes[faction] = xenotype;
        }

        public static void SetCustomXenotype(this PawnGenOptionWithXenotype option, CustomXenotype xenotype)
        {
            FactionXenotypeRandomizer.Current.pawnGenOptionCustomXenotypes[option] = xenotype;
        }
    }
}
