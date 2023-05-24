namespace DisneyCharacters
{
    using System;

    /// <summary>
    /// Set flags to assist when debugging; sanity check the correct values in Release builds
    /// </summary>
    public static class DebugConstants
    {
        public const string SAMPLEDATA_FILE = "sampledata.json";

        public static bool UseDefaultCarouselAnimation = false;
        public static bool UseSampleData = false;
        public static bool AutoLoadData = true;
        public static bool LogPasswords = false;

        private static void LogConstant(string name, bool value)
        {
            if (value)
            {
                Console.WriteLine($"DebugConstant {name} is {value}");
            }
        }

        // Sanity check - ensure settings are not active in release
        public static void SanityCheck()
        {
#if DEBUG
            LogConstant("LogPasswords", LogPasswords);
            LogConstant("UseSampleData", UseSampleData);
#else
            Debug.Assert(LogPasswords == false);
            Debug.Assert(UseSampleData == false);
#endif
        }
    }
}