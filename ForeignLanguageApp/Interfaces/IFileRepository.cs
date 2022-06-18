namespace ForeignLanguageApp.Interfaces
{
    public interface IFileRepository
    {
        public bool CheckIfStorageExists(); 

        public void CreateStorage(); 

        public void DeleteStorage(); 

        public string GetStorageName();
    }
}
