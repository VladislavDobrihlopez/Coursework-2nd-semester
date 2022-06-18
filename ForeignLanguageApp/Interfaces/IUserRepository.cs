using ForeignLanguageApp.Models;

namespace ForeignLanguageApp.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User FindUser(string login);

        public User FindUser(string login, string password);
    }
}
