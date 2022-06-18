using System;

namespace ForeignLanguageApp.Interfaces
{
    public interface IUserInterfaceProvider
    {
        public void PrintLine(string line = "");

        public void Print(string line = "");

        public int GetInteger(string message = "");

        public string GetString(string message = "");

        public ConsoleKey GetPushedButton(string message = "");

        public void RequestKeyPressToContinue(string message = "");

        public void ClearPage();

        public void SetTextColor(ConsoleColor color);

        public void SetDefaultPageTextColor();
    }
}
