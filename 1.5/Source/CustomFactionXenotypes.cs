namespace FactionXenotypeRandomizer
{
    public static class CustomFactionXenotypes
    {
        private static bool initialized = false;

        public static void Initialize()
        {
            initialized = true;
        }

        public static void Uninitialize()
        {
            initialized = false;
        }

        // For compatibility with mods that use the factions UI outside of new colony creation, only show these 
        // options when initialized
        public static bool ShouldShow()
        {
            return initialized;
        }

        /*private static List<CustomXenotype> xenotypes;

        public static int CurrentIndex { get; private set; } = 0;

        public static CustomXenotype GetCurrentXenotype()
        {
            if (CurrentIndex >= xenotypes.Count)
            {
                return null;
            }
            else
            {
                return xenotypes[CurrentIndex++];
            }
        }

        public static void SetXenotype(int index, CustomXenotype xenotype)
        {
            xenotypes[index] = xenotype;
        }

        public static void Add()
        {
            if (xenotypes != null)
            {
                xenotypes.Add(null);
            }
        }

        public static void Delete()
        {
            if (xenotypes != null && xenotypes.Count > CurrentIndex)
            {
                xenotypes.RemoveAt(CurrentIndex);
            }
        }

        public static void Initialize(int count)
        {
            xenotypes = new List<CustomXenotype>();
            xenotypes.AddRange(Enumerable.Repeat<CustomXenotype>(null, count));
        }

        public static void Uninitialize()
        {
            xenotypes = null;
        }

        public static void ResetIndex()
        {
            CurrentIndex = 0;
        }

        // For compatibility with mods that use the factions UI outside of new colony creation, only show these 
        // options when initialized
        public static bool ShouldShow()
        {
            return xenotypes != null;
        }*/
    }
}
