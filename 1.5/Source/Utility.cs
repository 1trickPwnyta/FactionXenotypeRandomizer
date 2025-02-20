using RimWorld;

namespace FactionXenotypeRandomizer
{
    public static class Utility
    {
        public static bool IsMutant(this FactionDef def)
        {
            return def.categoryTag == "Mutants";
        }
    }
}
