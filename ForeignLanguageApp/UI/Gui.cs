namespace ForeignLanguageApp.UI
{
    public static class Gui
    {
        public const string AuthorizationMenuHeader = "Авторизация";

        public const string AdminMenuHeader = "Модуль администратора";

        public const string UserMenuHeader = "Модуль пользователя";

        public const string AuthorizationHeader = "\t\tАвторизация";

        public const string WelcomeMessage = "\tВас приветствует средство для изучения иностранного языка \"Just learn\"\nЗдесь вы прокачаете свой уровень владения иностранным языком с помощью упражнений :)\n\tПрограмму разработал Войтов Владислав. Есть вопросы? Пишите dobrihlopez@gmail.com";

        public const string AuthorizeToContinue = "Для продолжения необходимо войти в систему";

        public static readonly string AccountTableHeader = $"{"Логин", -20} {"Пароль", -20} {"Роль", -10}";

        public static readonly string AccountTableHeaderWithId = $"{"Id", 4} {"Логин", -20} {"Пароль", -20} {"Роль", -10}";

        public const string TopicOutputForm = "Тема: {0, -34} [{1:dd MMMM yyyy HH:mm}]";

        public const string TopicOutputFormWithoutCreationTime = "{0, 4}) Тема: {1, 20}";

        public const string CardOutputForm = "{0, 4}) {1}";
               
        public const string AccountOutputForm = "{0, 4} {1, -20} {2, -20} {3, -10}";
               
        public const string AccountOutputFormWithoutId = "{0, -20} {1, -20} {2, -10}";
        
        public const string StartMessageBeforeTestStarts = "Вам предстоит дать ответ на {0} вопросов, вы готовы?";

        public const string FileOverwriteWarning = "Существующий файл будет перезаписан";

        public const string UserDeletionWarning = "Вы точно хотите удалить данного пользователя?\nОтменить изменения будет невозможно\n1) Да\n2) Нет";

        public const string StorageDeletionWarning = "Вы точно хотите удалить файл?\nОтменить изменения будет невозможно\n1) Да\n2) Нет";

        public const string TopicDeletionWarning = "Вы точно хотите удалить данную тему?\nОтменить изменения будет невозможно\n1) Да\n2) Нет";

        public const string CardDeletionWarning = "Вы точно хотите удалить данную карточку?\nОтменить изменения будет невозможно\n1) Да\n2) Нет";

        public const string Succeed = "Операция прошла успешно";

        public const string TryAgain = "Повторите попытку";

        public static readonly string[] YesNoOptions = new string[]
        {
            "1. Да",

            "2. Нет",
        };

        public const string AskForEnteringUserNumber = "Введите номер пользователя";

        public const string AskForEnteringTopicNumber = "Введите номер темы";

        public const string AskForEnteringCardNumber = "Введите номер карточки";

        public const string AskForLogin = "Введите логин: ";

        public const string AskForPassword = "Введите пароль: ";

        public const string AskForUserData = "Введите через пробел логин, пароль, роль (User, Admin)";

        public const string AskForTopicData = "Введите название темы";

        public const string AskForCardData = "Введите через пробел слово и перевод";

        public const string AskForSearchMode = "Введите тип поиска";

        public const string AskForSearchModel = "Введите, что вы хотите искать из предложенного";

        public const string AskForLevelOfDifficulty = "Введите уровень сложности";

        public const string ChooseLevelOfDifficulty = "Выберите уровень сложности";

        public const string AskForEnteringDate = "Введите день, месяц, год";

        public const string AskForEnteringStartDate = "Введите через пробел начальный день, месяц, год";

        public const string AskForEnteringEndDate = "Введите через пробел конечный день, месяц, год";

        public const string PressAnyKey = "Нажмите любую кнопку для продолжения...";

        public const string UserNotFoundError = "Пользователь не найден, повторите попытку";

        public const string UserFound = "Пользователь найден";

        public const string FileOpened = "Был открыт файл {0}";

        public static readonly string[] OptionsForLevelOfDifficulty = new string[]
        {
            "1. Легкий",

            "2. Средний",

            "3. Мастер",
        };

        public static readonly string[] OptionsForAdminMainPage = new string[]
        {
            "1. Управление учётными записями пользователей",

            "2. Работа с файлом данных пользователей",

            "3. Модуль пользователя",

            "4. <--",
        };

        public static readonly string[] OptionsForUserAccountManagementPage = new string[]
        {
            "1. Просмотр всех учётных записей",

            "2. Добавление новой учётной записи",

            "3. Редактирование учётной записи",

            "4. Удаление учётной записи",

            "5. <--",
        };

        public static readonly string[] OptionsForFileManagementPage = new string[]
        {
            "1. Создание файла",
                
            "2. Удаление файла",

            "3. <--",
        };

        public static readonly string[] OptionsForUserMainPage = new string[]
        {
            "1. Работа со словарным запасом",

            "2. <--",
        };

        public static readonly string[] OptionsForSearchPageFirst = new string[]
        {
            "1. Поиск среди тем",

            "2. Поиск среди карточек по всем темам",

            "3. <--",
        };

        public static readonly string[] OptionsForSearchPageSecond = new string[]
        {
            "1. Поиск по дате",

            "2. Поиск по промежутку",
        };

        public static readonly string[] OptionsForVocabularyTrainingPage = new string[]
        {
            "1. Просмотреть все темы",

            "2. Поиск",

            "3. Добавить новую тему",

            "4. Редактировать тему",

            "5. Удалить тему",

            "6. Начать упражнение \"Вспомнить всё\"",

            "7. Начать упражнение \"Поле чудес\"",

            "8. <--",
        };

        public static readonly string[] TopicEditingOptions = new string[]
        {
            "1. Добавить карточку",

            "2. Редактировать карточку",

            "3. Удалить карточку",

            "4. <--",
        };

        public static readonly string[] CardEditingOptions = new string[]
        {
            "1. Изменить слово",

            "2. Изменить перевод",
        };

        public static readonly string[] PresentingTopicsWithCardsOptions = new string[]
        {
            $"{"1. Режим упорядочивания темы по времени создания", -55}" + "{0}",

            $"{"2. Режим упорядочивания карточек по времени создания", -55}" + "{0}",

            "3. <--",
        };

        public const string SearchResults = "Здесь вы можете видеть результаты поиска";

        public static readonly string Ascending = $"{"по возрастанию", -20}";

        public static readonly string Descending = $"{"по убыванию", -20}";

        public const string FileNotFoundForDeletion = "Не найден файл {0} для удаления";

        public const string AskUserIfHeSurrenders = "Вы сдаётесь?";

        public const string AskForFrontSideOfCard = "Введите слово";

        public const string AskForBackSideOfCard = "Введите перевод слова";

        public const string EmptyTopicCardsError = "Действие невозможно,так как отсутствуют карточки в теме";

        public const string EmptyTopicsError = "Действие невозможно,так как отсутствуют темы";

        public const string DamagedFile = "По всей видимости, файл {0} был повреждён.\nНекоторые данные могли быть потеряны.";

        public const string InvalidDataInputedError = "Введено некорректное значение, попробуйте заново";

        public const string GeneralError = "Что-то пошло не так, попробуйте позже. \n Причина: {0}";

        public const string NotUniqueLoginError = "Пользователь с таким логином уже существует в системе.\nЛогин должен быть уникален";

        public const string StorageIsUsedByAnotherProgrammError = "Хранилище используется другой программой";

        public const string FileIsUsedByAnotherProgrammError = "Файл используется другой программой";

        public const string FileAlreadyExists = "Файл уже существует";

        public const string LoginNotMeetRequirementsError = "Логин должен быть по длине не менее 3 букв";

        public const string PasswordNotMeetRequirementsError = "Пароль должен быть по длине не менее 3 символов";

        public const string CardNotMeetRequirementsError = "Слово и его перевод должны быть длинной не менее 2 символов";

        public const string TopicNotMeetRequirementsError = "Название темы должно быть длинной не менее 4 символов";

        public const string IncorrectNumberOfParametersForUserError = "Введено некорректное число параметров для пользователя";

        public const string IncorrectNumberOfParametersForCardError = "Введено некорректное число параметров для карточки";

        public const string IncorrectNumberOfParametersForDateError = "Введено некорректное число параметров для даты";

        public const string CorrectAnswer = "Правильно!";

        public const string CorrectAnswerIs = "Корректный ответ: {0}";

        public const string CorrectAnswersGiven = "Дано правильных ответов: {0}/{1}";

        public const string TaskMessageForWordCompositionTest = "Слово {0} означает {1}, вспомните это слово и напишите его";

        public const string TaskMessageForVocabularyMemoryTest = "Вам дан перевод слова: {0}, вспомните это слово";

        public const string WrongAnswer = "Неправильно, попробуйте ещё";
    }
}
