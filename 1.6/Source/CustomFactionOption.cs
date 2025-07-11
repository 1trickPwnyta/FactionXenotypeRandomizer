using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    public enum CustomFactionOption
    {
        RandomMutant,
        RandomXenotype,
        CustomXenotype
    }

    [StaticConstructorOnStartup]
    public static class CustomFactionOptionUtility
    {
        private readonly static Texture2D diceIcon = ContentFinder<Texture2D>.Get("UI/Icons/FactionXenotypeRandomizer_Dice");
        private readonly static Texture2D randomXenotypeIcon = ContentFinder<Texture2D>.Get("UI/Icons/FactionXenotypeRandomizer_RandomXenotype");

        public static string GetLabel(this CustomFactionOption option)
        {
            switch (option)
            {
                case CustomFactionOption.RandomMutant: return "FactionXenotypeRandomizer_RandomMutant".Translate();
                case CustomFactionOption.RandomXenotype: return "FactionXenotypeRandomizer_RandomXenotype".Translate();
                default: throw new Exception("Invalid custom faction option: " + option);
            }
        }

        public static Texture2D GetIcon(this CustomFactionOption option)
        {
            switch (option)
            {
                case CustomFactionOption.RandomMutant: return diceIcon;
                case CustomFactionOption.RandomXenotype: return randomXenotypeIcon;
                default: throw new Exception("Invalid custom faction option: " + option);
            }
        }
    }
}
