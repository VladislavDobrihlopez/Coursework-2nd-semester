using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.UI.LearningSystemUI;
using System;

namespace ForeignLanguageApp.UI.UserMenuUI
{
    public class UserMenu
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly LearningSystem learningSystem;

        public UserMenu(IUserInterfaceProvider UIProvider, LearningSystem learningSystem)
        {
            this.UIProvider = UIProvider;

            this.learningSystem = learningSystem;
        }

        private void RenderHeader()
        {
            UIProvider.PrintLine(Gui.UserMenuHeader);

            foreach (string option in Gui.OptionsForUserMainPage)
            {
                UIProvider.PrintLine(option);
            }
        }

        public void HandlePageActivities()
        {
            while (true)
            {
                UIProvider.ClearPage();

                RenderHeader();

                ConsoleKey button = UIProvider.GetPushedButton();

                switch (button)
                {
                    case ConsoleKey.D1:
                        learningSystem.HandlePageActivities();
                        break;
                    case ConsoleKey.D2:
                        return;
                }
            }
        }
    }
}
