using ForeignLanguageApp.Helpers;
using ForeignLanguageApp.Interfaces;
using System;
using System.Text;

namespace ForeignLanguageApp.Services
{
    public class VocabularyTrainingService : IVocabularyTrainingService
    {
        private int levelOfDifficulty;

        private const double CorrectionFactor = 0.3;

        public string GetMaskedWord(string word)
        {
            StringBuilder stringBuilder = new StringBuilder(word);

            double percentageToEncrypt = (levelOfDifficulty * CorrectionFactor);

            int toReplace = Math.Max(1, (int)(percentageToEncrypt * word.Length));

            if (RandomizeHelper.GetRandomInteger() % 2 == 0)
            {
                int count = Math.Min(word.Length, toReplace);

                stringBuilder.Remove(0, count);

                stringBuilder.Insert(0, "*", count);
            }
            else
            {
                int count = Math.Min(word.Length, toReplace);

                stringBuilder.Remove(word.Length - count, count);

                stringBuilder.Insert(stringBuilder.Length, "*", count);
            }

            return stringBuilder.ToString();
        }

        public void UpdateDifficulty(int difficulty)
        {
            levelOfDifficulty = difficulty;
        }
    }
}
