using ForeignLanguageApp.Data;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using ForeignLanguageApp.Services;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.UI.AdminUI
{
    public class AccountManagementPage
    {
        private readonly IUserService userService;

        private readonly IUserInterfaceProvider UIProvider;

        public AccountManagementPage(IUserInterfaceProvider UIProvider, IUserService userService)
        {
            this.UIProvider = UIProvider;

            this.userService = userService;
        }

        private void ShowAccount(User user)
        {
            UIProvider.PrintLine(string.Format(Gui.AccountOutputFormWithoutId, user.Login, user.Password, user.Role));

        }

        private void ShowAllAccounts(List<User> users)
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(Gui.AccountTableHeaderWithId);

            for (int i = 0; i < users.Count; i++)
            {
                UIProvider.PrintLine(string.Format(Gui.AccountOutputForm, i + 1, users[i].Login, users[i].Password, users[i].Role));
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowAddingNewAccount()
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(Gui.AskForUserData);

            try
            {
                User newPerson = Parsers.ParseUser(UIProvider.GetString());
            
                if (userService.Login(newPerson.Login) == null)
                {
                    userService.AddUser(newPerson);

                    UIProvider.PrintLine(Gui.Succeed);
                }
                else
                {
                    UIProvider.PrintLine(Gui.NotUniqueLoginError);
                }
            }
            catch (Exception ex)
            {
                UIProvider.PrintLine(ex.Message);
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowAccountToBeEdited(User oldPerson)
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(Gui.AccountTableHeader);

            ShowAccount(oldPerson);

            User updatedPerson;

            try
            {
                UIProvider.PrintLine(Gui.AskForUserData);

                updatedPerson = Parsers.ParseUser(UIProvider.GetString());
            }
            catch (Exception ex)
            {
                UIProvider.PrintLine(ex.Message);

                UIProvider.PrintLine(Gui.PressAnyKey);

                UIProvider.RequestKeyPressToContinue();

                return;
            }

            try
            {
                oldPerson = userService.GetUserWithReplacedFields(oldPerson, updatedPerson.Login, updatedPerson.Password, updatedPerson.Role);

                userService.UpdateExistingUserBy(oldPerson);

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (Exception ex)
            {
               UIProvider.PrintLine(ex.Message);
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowAccountEditingPage(List<User> users)
        {
            UIProvider.ClearPage();

            ShowAllAccounts(users);

            int id;

            do
            {
                id = UIProvider.GetInteger(Gui.AskForEnteringUserNumber) - 1;
            }
            while (!Validators.IsIndexValid(id, users.Count));

            ShowAccountToBeEdited(users[id]);
        }

        private void ShowAccountToBeDeleted(User person)
        {
            UIProvider.ClearPage();

            UIProvider.PrintLine(Gui.AccountTableHeader);

            ShowAccount(person);

            FileStorageService fileService = new FileStorageService(new TopicRepository(person.Id));

            try
            {
                userService.DeleteUser(person);

                fileService.DeleteStorage();

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (Exception ex)
            {
                UIProvider.PrintLine(string.Format(Gui.GeneralError, ex.Message));
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ShowAccountDeletionPage(List<User> users)
        {
            UIProvider.ClearPage();

            ShowAllAccounts(users);

            int id;

            do
            {
                id = UIProvider.GetInteger(Gui.AskForEnteringUserNumber) - 1;
            }
            while (!Validators.IsIndexValid(id, users.Count));

            UIProvider.PrintLine(Gui.UserDeletionWarning);

            ConsoleKey button = UIProvider.GetPushedButton();

            switch (button)
            {
                case ConsoleKey.D1:
                    ShowAccountToBeDeleted(users[id]);
                    break;
                default:
                    return;
            }
        }

        private void RenderHeader()
        {
            UIProvider.ClearPage();

            foreach (string option in Gui.OptionsForUserAccountManagementPage)
            {
                UIProvider.PrintLine(option);
            }
        }

        public void HandlePageActivities()
        {
            List<User> users = userService.GetAllUsers();

            while (true)
            {
                RenderHeader();

                ConsoleKey button = UIProvider.GetPushedButton();

                switch (button)
                {
                    case ConsoleKey.D1:
                        ShowAllAccounts(users);
                        break;
                    case ConsoleKey.D2:
                        ShowAddingNewAccount();
                        users = userService.GetAllUsers();
                        break;
                    case ConsoleKey.D3:
                        ShowAccountEditingPage(users);
                        users = userService.GetAllUsers();
                        break;
                    case ConsoleKey.D4:
                        ShowAccountDeletionPage(users);
                        users = userService.GetAllUsers();
                        break;
                    case ConsoleKey.D5:
                        return;
                }
            }
        }
    }
}
