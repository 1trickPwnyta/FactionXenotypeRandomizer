using RimWorld;
using System.Collections.Generic;
using Verse;

namespace FactionXenotypeRandomizer
{
    public class FactionXenotypeRandomizer : GameComponent
    {
        public static FactionXenotypeRandomizer Current
        {
            get
            {
                return Verse.Current.Game.GetComponent<FactionXenotypeRandomizer>();
            }
        }
        
        public Dictionary<Faction, CustomXenotype> factionXenotypes = new Dictionary<Faction, CustomXenotype>();
        private List<Faction> workingFactionList;
        private List<CustomXenotype> workingCustomXenotypeList;

        public FactionXenotypeRandomizer(Game game)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref factionXenotypes, "factionXenotypes", LookMode.Reference, LookMode.Deep, ref workingFactionList, ref workingCustomXenotypeList);
        }
    }
}
