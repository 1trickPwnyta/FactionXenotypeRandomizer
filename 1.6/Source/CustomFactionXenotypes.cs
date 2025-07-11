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
    }
}
