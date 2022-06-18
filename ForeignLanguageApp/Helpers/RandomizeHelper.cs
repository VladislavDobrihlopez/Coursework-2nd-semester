using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.Helpers
{
    public static class RandomizeHelper
    {
        public static readonly Random generator;

        static RandomizeHelper()
        {
            generator = new Random();
        }

        public static List<T> GetShuffledItems<T>(List<T> items) 
        {
            List<T> shuffledCards = new List<T>(items);

            for (int i = shuffledCards.Count - 1; i >= 1; i--)
            {
                int j = generator.Next(i + 1);

                (shuffledCards[i], shuffledCards[j]) = (shuffledCards[j], shuffledCards[i]);
            }

            return shuffledCards;
        }

        public static int GetRandomInteger()
        {
            return generator.Next();
        }
    }
}
