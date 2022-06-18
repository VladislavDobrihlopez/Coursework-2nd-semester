namespace ForeignLanguageApp
{
    public static class Validators
    {
        public static bool IsIndexValid(int index, int size)
        {
            return (index >= 0) && (index < size);
        }

        public static bool IsLevelOfDifficultyValid(int level)
        {
            return (level >= 1) && (level <= 3);
        }
    }
}
