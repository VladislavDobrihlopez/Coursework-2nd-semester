using System;
using ForeignLanguageApp.Interfaces;

namespace ForeignLanguageApp.UI
{
    public class ConsoleProvider : IUserInterfaceProvider
    {
        public void ClearPage()
        {
            Console.Clear();
        }

        public int GetInteger(string message = "")
        {
            PrintNonEmpty(message);

            int result;

            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine(Gui.TryAgain);
            }

            return result;
        }

        public ConsoleKey GetPushedButton(string message = "")
        {
            PrintNonEmpty(message);

            return Console.ReadKey().Key;
        }

        public string GetString(string message = "")
        {
            PrintNonEmpty(message);

            return Console.ReadLine();
        }

        private void PrintNonEmpty(string message)
        {
            if (message != String.Empty)
            {
                Console.WriteLine(message);
            }
        }

        public void Print(string line = "")
        {
            Console.Write(line);
        }

        public void PrintLine(string line = "")
        {
            Console.WriteLine(line);
        }

        public void RequestKeyPressToContinue(string message = "")
        {
            PrintNonEmpty(message);

            Console.ReadKey();
        }

        public void SetTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void SetDefaultPageTextColor()
        {
            Console.ResetColor();
        }
    }
}
