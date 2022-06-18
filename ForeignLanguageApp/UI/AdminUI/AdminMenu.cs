using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.UI.UserMenuUI;
using System;

namespace ForeignLanguageApp.UI.AdminUI
{
    public class AdminMenu
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly UserMenu userMenu;

        private readonly AccountManagementPage accountManagementPage;

        private readonly FileManagementPage fileManagementPage;

        public AdminMenu(IUserInterfaceProvider UIProvider, UserMenu userMenu, AccountManagementPage accountManagementPage, FileManagementPage fileManagementPage)
        {
            this.UIProvider = UIProvider;

            this.userMenu = userMenu;

            this.accountManagementPage = accountManagementPage;

            this.fileManagementPage = fileManagementPage;
        }

        private void RenderHeader()
        {
            UIProvider.PrintLine(Gui.AdminMenuHeader);

            foreach (string option in Gui.OptionsForAdminMainPage)
            {
                UIProvider.PrintLine(option);
            }
        }

        public void HandlePageActivities()
        {
            while (true)
            {
                Console.Clear();

                RenderHeader();

                ConsoleKey button = UIProvider.GetPushedButton();

                 switch (button)
                {
                    case ConsoleKey.D1:
                        accountManagementPage.HandlePageActivities();
                        break;
                    case ConsoleKey.D2:
                        fileManagementPage.HandlePageActivities();
                        break;
                    case ConsoleKey.D3:
                        userMenu.HandlePageActivities();
                        break;
                    case ConsoleKey.D4:
                        return;
                }
            }
        }
    }
}
