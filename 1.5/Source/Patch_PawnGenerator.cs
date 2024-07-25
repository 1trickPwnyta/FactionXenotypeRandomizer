﻿using HarmonyLib;
using Verse;

namespace FactionXenotypeRandomizer
{
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("TryGenerateNewPawnInternal")]
    public static class Patch_PawnGenerator_TryGenerateNewPawnInternal
    {
        public static void Prefix(ref PawnGenerationRequest request)
        {
            if (request.Faction != null && request.Faction.def.categoryTag == "Mutants")
            {
                request.ForcedCustomXenotype = FactionXenotypeRandomizer.Current.factionXenotypes[request.Faction];
            }
        }
    }
}
