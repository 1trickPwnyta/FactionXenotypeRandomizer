using RimWorld;
using System.Reflection;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class CustomFactionDef : FactionDef, IExposable
    {
        private FactionDef parent;
        public CustomXenotype customXenotype;

        public CustomFactionDef() { }

        public CustomFactionDef(FactionDef parent)
        {
            this.parent = parent;
            PostLoad();
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
            Scribe_Deep.Look(ref customXenotype, "customXenotype");
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                PostLoad();
            }
        }

        public override bool Equals(object obj) => base.Equals(obj) || obj == parent;

        public override int GetHashCode() => parent.GetHashCode();
    }
}
