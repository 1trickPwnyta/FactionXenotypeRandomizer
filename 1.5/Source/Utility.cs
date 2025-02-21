using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace FactionXenotypeRandomizer
{
    public static class Utility
    {
        private static Dictionary<PawnGenOptionWithXenotype, CustomXenotype> pawnGenOptionCustomXenotypes = new Dictionary<PawnGenOptionWithXenotype, CustomXenotype>();
        public static FactionDef lastFactionDef;

        public static bool IsMutant(this FactionDef def)
        {
            return def.categoryTag == "Mutants";
        }

        public static float GetCombatPowerFactor(this CustomXenotype xenotype)
        {
            return xenotype?.genes.Select(g => g.GetModExtension<GeneDefModExtension>()?.combatPowerFactor ?? 1f).Aggregate((x, y) => x * y) ?? 1f;
        }

        public static CustomXenotype GetCustomXenotype(this FactionDef def)
        {
            return (def as CustomFactionDef)?.customXenotype;
        }

        public static CustomXenotype GetCustomXenotype(this PawnGenOptionWithXenotype option)
        {
            return pawnGenOptionCustomXenotypes.ContainsKey(option) ? pawnGenOptionCustomXenotypes[option] : null;
        }

        public static void SetCustomXenotype(this Faction faction, CustomXenotype xenotype)
        {
            faction.def = new CustomFactionDef(faction.def)
            {
                customXenotype = xenotype
            };
        }

        public static void SetCustomXenotype(this PawnGenOptionWithXenotype option, CustomXenotype xenotype)
        {
            pawnGenOptionCustomXenotypes[option] = xenotype;
        }
    }
}
