namespace ForeignLanguageApp.Interfaces
{
    public interface IFileStorageService
    {
        public void DeleteStorage();

        public void CreateStorage();

        public string GetStorageName();

        public bool IsStorageCreated();
    }
}
