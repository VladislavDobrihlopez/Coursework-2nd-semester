using ForeignLanguageApp.Interfaces;
using System;
using System.IO;

namespace ForeignLanguageApp.UI.AdminUI
{
    public class FileManagementPage
    {
        private readonly IUserInterfaceProvider UIProvider;

        private readonly IFileStorageService fileService;

        public FileManagementPage(IUserInterfaceProvider UIProvider, IFileStorageService fileService)
        {
            this.UIProvider = UIProvider;

            this.fileService = fileService;
        }

        private void RenderHeader()
        {
            UIProvider.ClearPage();

            foreach (string option in Gui.OptionsForFileManagementPage)
            {
                UIProvider.PrintLine(option);
            }
        }

        private void ShowCreatingFile()
        {
            UIProvider.ClearPage();

            try
            {
                fileService.CreateStorage();

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (IOException ex)
            {
                UIProvider.PrintLine(ex.Message);
            }
            catch (Exception ex)
            {
                UIProvider.PrintLine(string.Format(Gui.GeneralError, ex.Message));
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        private void ExecuteDeletion()
        {
            try
            {
                fileService.DeleteStorage();

                UIProvider.PrintLine(Gui.Succeed);
            }
            catch (IOException ex)
            {
                UIProvider.PrintLine(ex.Message);
            }
        }

        private void ShowDeletingFile()
        {
            UIProvider.ClearPage();

            if (fileService.IsStorageCreated())
            {
                UIProvider.PrintLine(Gui.StorageDeletionWarning);

                ConsoleKey button = Console.ReadKey().Key;

                switch (button)
                {
                    case ConsoleKey.D1:
                        ExecuteDeletion();
                        break;
                    default:
                        return;
                }
            }
            else
            {
                UIProvider.PrintLine(string.Format(Gui.FileNotFoundForDeletion, fileService.GetStorageName()));
            }

            UIProvider.PrintLine(Gui.PressAnyKey);

            UIProvider.RequestKeyPressToContinue();
        }

        public void HandlePageActivities()
        {
            while (true)
            {
                RenderHeader();

                ConsoleKey button = Console.ReadKey().Key;

                switch (button)
                {
                    case ConsoleKey.D1:
                        ShowCreatingFile();
                        break;
                    case ConsoleKey.D2:
                        ShowDeletingFile();
                        break;
                    case ConsoleKey.D3:
                        return;
                }
            }
        }
    }
}
