using ForeignLanguageApp.Models;
using System.Collections.Generic;
namespace ForeignLanguageApp.Interfaces
{
    public interface IUserService
    {
        public User GetUserWithReplacedFields(User oldPerson, string login, string password, Role role);

        public List<User> GetAllUsers();

        public void AddDefaultAccount(List<User> users);

        public void AddUser(User user);

        public void DeleteUser(User user);

        public User Login(string login);

        public User Login(string login, string password);

        public User FindUser(string login, string password);

        public void UpdateExistingUserBy(User user);
    }
}
