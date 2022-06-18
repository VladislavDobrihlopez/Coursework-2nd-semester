namespace ForeignLanguageApp.Interfaces
{
    public interface IVocabularyTrainingService
    {
        public string GetMaskedWord(string word);

        public void UpdateDifficulty(int difficulty);
    }
}
