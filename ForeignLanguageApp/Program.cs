using ForeignLanguageApp.Data;
using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Services;
using ForeignLanguageApp.UI;
using ForeignLanguageApp.UI.AuthMenuUI;

namespace ForeignLanguageApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();

            IUserRepository repository = userRepository;

            IUserService userService = new UserService(repository);

            IFileStorageService fileStorageService = new FileStorageService(userRepository);

            IUserInterfaceProvider interfaceProvider = new ConsoleProvider();

            new AuthMenu(interfaceProvider, userService, fileStorageService).HandlePageActivities();
        }
    }
}
