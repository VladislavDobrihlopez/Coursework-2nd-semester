using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.UI;
using System.IO;

namespace ForeignLanguageApp.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IFileRepository file;

        public FileStorageService(IFileRepository file)
        {
            this.file = file; 
        }

        public void DeleteStorage()
        {
            try
            {
                file.DeleteStorage();
            }
            catch
            {
                throw new IOException(Gui.FileIsUsedByAnotherProgrammError);
            }
        }

        public void CreateStorage()
        {
            try
            {
                file.CreateStorage();
            }
            catch
            {
                throw new IOException(Gui.FileAlreadyExists);
            }
        }

        public string GetStorageName()
        {
            return file.GetStorageName();
        }

        public bool IsStorageCreated()
        {
            return file.CheckIfStorageExists();
        }
    }
}
