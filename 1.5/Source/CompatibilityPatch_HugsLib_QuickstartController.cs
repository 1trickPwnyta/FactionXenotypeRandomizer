namespace FactionXenotypeRandomizer
{
    // Patched manually in mod constructor
    public static class CompatibilityPatch_HugsLib_QuickstartController
    {
        public static void Prefix()
        {
            CustomFactionXenotypes.Initialize(0);
        }
    }
}
