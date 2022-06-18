using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.Helpers
{
    public static class SearchHelper
    {
        public static List<Entity> FindAllMatchingDate(IEnumerable<Entity> data, DateTime date)
        {
            List<Entity> results = new List<Entity>();

            foreach(Entity item in data)
            {
                int itemDay = item.CreationTime.Day;

                int itemMonth = item.CreationTime.Month;

                int itemYear = item.CreationTime.Year;

                if (itemDay == date.Day && itemMonth == date.Month && itemYear == date.Year)
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public static List<Entity> FindAllMatchingInterval(IEnumerable<Entity> data, DateTime firstDate, DateTime secondDate)
        {
            List<Entity> results = new List<Entity>();

            foreach(Entity item in data)
            {
                if ((item.CreationTime >= firstDate) && (item.CreationTime <= secondDate))
                {
                    results.Add(item);
                }
            }

            return results;
        }
    }
}
