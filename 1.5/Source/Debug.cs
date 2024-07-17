namespace FactionXenotypeRandomizer
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{FactionXenotypeRandomizerMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
