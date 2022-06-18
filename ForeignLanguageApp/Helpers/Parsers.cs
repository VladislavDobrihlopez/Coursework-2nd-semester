using ForeignLanguageApp.Models;
using ForeignLanguageApp.UI;
using System;

namespace ForeignLanguageApp
{
    public static class Parsers
    {
        public static DateTime ParseDate(string input)
        {
            string[] dateData = ParseStringSequence(input);

            if (dateData.Length != 3)
            {
                throw new IndexOutOfRangeException(Gui.IncorrectNumberOfParametersForDateError);
            }

            try
            {
                int day = Convert.ToInt32(dateData[0]);

                int month = Convert.ToInt32(dateData[1]);

                int year = Convert.ToInt32(dateData[2]);

                return new DateTime(year, month, day);
            }
            catch
            {
                throw new ArgumentException(Gui.InvalidDataInputedError);
            }
        }

        public static string[] ParseStringSequence(string input)
        {
            return input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static User ParseUser(string input)
        {
            string[] userData = ParseStringSequence(input);

            if (userData.Length != 3)
            {
                throw new IndexOutOfRangeException(Gui.IncorrectNumberOfParametersForUserError);
            }

            string login = userData[0];

            string password = userData[1];

            Role role;

            try
            {
                role = Enum.Parse<Role>(userData[2], ignoreCase:true);
            }
            catch
            {
                throw new ArgumentException(Gui.InvalidDataInputedError);
            }

            return new User(login, password, role);
        }
    }
}
