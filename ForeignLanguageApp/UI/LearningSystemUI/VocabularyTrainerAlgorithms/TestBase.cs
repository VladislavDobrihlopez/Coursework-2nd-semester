using ForeignLanguageApp.Interfaces;
using System;

namespace ForeignLanguageApp.UI.LearningSystemUI.VocabularyTrainerAlgorithms
{
    public abstract class TestBase
    {
        protected const int UnsuccessfulAttemptsToAskForSurrender = 3;

        protected readonly IUserInterfaceProvider UIProvider;

        public TestBase(IUserInterfaceProvider UIProvider)
        {
            this.UIProvider = UIProvider;
        }

        protected void OutputPositiveAnswer()
        {
            UIProvider.SetTextColor(ConsoleColor.Green);

            UIProvider.PrintLine(Gui.CorrectAnswer);

            UIProvider.SetDefaultPageTextColor();
        }

        protected void OutputNegativeAnswer()
        {
            UIProvider.SetTextColor(ConsoleColor.Red);

            UIProvider.PrintLine(Gui.WrongAnswer);

            UIProvider.SetDefaultPageTextColor();
        }
    }
}
