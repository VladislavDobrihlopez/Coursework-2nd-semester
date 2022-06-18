using ForeignLanguageApp.Data;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using ForeignLanguageApp.Services;
using ForeignLanguageApp.UI.AdminUI;
using ForeignLanguageApp.UI.LearningSystemUI;
using ForeignLanguageApp.UI.LearningSystemUI.VocabularyTrainerAlgorithms;
using ForeignLanguageApp.UI.UserMenuUI;

namespace ForeignLanguageApp.UI.AuthMenuUI
{
    public class AuthMenu
    {
        private readonly IUserInterfaceProvider UIProvider;
        
        private readonly IUserService userService;

        private readonly IFileStorageService fileService;

        public AuthMenu(IUserInterfaceProvider interfaceProvider, IUserService userService, IFileStorageService fileService)
        {
            this.UIProvider = interfaceProvider;

            this.userService = userService;

            this.fileService = fileService;
        }

        private void RunIntro()
        {
            StartLoadingAnimation();

            RenderWelcomeMessage();
        }

        private void StartLoadingAnimation()
        {
            for (int i = 0; i < 5; i++)
            {
                UIProvider.Print(".");

                System.Threading.Thread.Sleep(300);
            }
        }

        private void RenderWelcomeMessage()
        {
           UIProvider.ClearPage();
           
           UIProvider.PrintLine(Gui.WelcomeMessage);
           
           UIProvider.PrintLine(Gui.PressAnyKey);
           
           UIProvider.RequestKeyPressToContinue();
        }

        public void HandlePageActivities()
        {
            RunIntro();

            bool isAlive = true;

            while (isAlive)
            {
                UIProvider.ClearPage();

                UIProvider.PrintLine(Gui.AuthorizationHeader);

                UIProvider.PrintLine(Gui.AuthorizeToContinue);

                string login = UIProvider.GetString(Gui.AskForLogin).Trim();

                string password = UIProvider.GetString(Gui.AskForPassword).Trim();

                User person = userService.Login(login, password);

                if (person == null)
                {
                    UIProvider.PrintLine(Gui.UserNotFoundError);

                    UIProvider.RequestKeyPressToContinue();

                    continue;
                }

                UIProvider.PrintLine(Gui.UserFound);

                UIProvider.RequestKeyPressToContinue();

                IBaseRepository<Topic> topicRepository = new TopicRepository(person.Id);

                ITopicService topicService = new TopicService(topicRepository);

                LearningSystem learningSystem = new LearningSystem(UIProvider, topicService, new VocabularyTrainer(UIProvider, new VocabularyMemoryTest(UIProvider), new WordCompositionTest(UIProvider, new VocabularyTrainingService())), new Searcher(UIProvider, topicService));

                UserMenu userMenu = new UserMenu(UIProvider, learningSystem);

                switch (person.Role)
                {
                    case Role.User:
                        userMenu.HandlePageActivities();
                        break;
                    case Role.Admin:
                        new AdminMenu(UIProvider, userMenu, new AccountManagementPage(UIProvider, userService), new FileManagementPage(UIProvider, fileService)).HandlePageActivities();
                        break;
                }
            }
        }
    }
}
