using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class CustomFactionDef : FactionDef, IExposable
    {
        private FactionDef parent;
        public CustomXenotype customXenotype;
        public CustomFactionOption option;

        private CustomFactionDef() { }

        public CustomFactionDef(FactionDef parent)
        {
            this.parent = parent;
            PostLoad();
        }

        public string OptionName => option == CustomFactionOption.CustomXenotype ? customXenotype.name : option.GetLabel();

        public Texture2D OptionIcon => option == CustomFactionOption.CustomXenotype ? customXenotype.iconDef.Icon : option.GetIcon();

        public void SetFactionXenotype(Faction faction)
        {
            CustomXenotype xenotype;
            switch (option)
            {
                case CustomFactionOption.RandomMutant:
                    xenotype = new CustomXenotype();
                    xenotype.inheritable = true;
                    xenotype.genes = new List<GeneDef>();
                    XenotypeRandomizer.XenotypeRandomizer.Randomize(xenotype.genes, ref xenotype.iconDef, false);
                    xenotype.name = GeneUtility.GenerateXenotypeNameFromGenes(xenotype.genes);
                    break;
                case CustomFactionOption.RandomXenotype:
                    Utility.CustomXenotypes.TryRandomElement(out xenotype);
                    break;
                case CustomFactionOption.CustomXenotype:
                    return;
                default: throw new Exception("Invalid custom faction option: " + option);
            }
            faction.SetCustomXenotype(xenotype);
        }

        public override void PostLoad()
        {
            foreach (FieldInfo field in typeof(FactionDef).GetFields())
            {
                field.SetValue(this, field.GetValue(parent));
            }
            base.PostLoad();
        }

        public void ExposeData()
        {
            Scribe_Defs.Look(ref parent, "parent");
            Scribe_Values.Look(ref option, "option");
            Scribe_Deep.Look(ref customXenotype, "customXenotype");
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                PostLoad();
            }
        }

        public override bool Equals(object obj) => obj == this || obj == parent;

        public override int GetHashCode() => parent.GetHashCode();
    }
}
