using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using ForeignLanguageApp.UI;
using System;
using System.Collections.Generic;

namespace ForeignLanguageApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        private const string LoginForDefaultAccount = "admin";

        private const string PasswordForDefaultAccount = "admin";

        private const Role RoleForDefaultAccount = Role.Admin;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User GetUserWithReplacedFields(User oldPerson, string login, string password, Role role)
        {
            User editedUser = (User)oldPerson.Clone();

            if (((login != oldPerson.Login) && (Login(login) == null)) || (login == oldPerson.Login))
            {
                editedUser.Update(login, password, role);

                return editedUser;
            }
            else
            {
                throw new ArgumentException(Gui.NotUniqueLoginError);
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = repository.GetAll();

            if (users.Count == 0)
            {
                AddDefaultAccount(users);
            }

            return users;
        }

        public void AddDefaultAccount(List<User> users)
        {
            User admin = new User(LoginForDefaultAccount, PasswordForDefaultAccount, RoleForDefaultAccount);

            users.Add(admin);

            AddUser(admin);
        }

        public void AddUser(User user)
        {
            repository.Add(user);
        }

        public void DeleteUser(User user)
        {
            repository.Delete(user);
        }

        public User Login(string login)
        {
            return repository.FindUser(login);
        }

        public User Login(string login, string password)
        {
            List<User> users = GetAllUsers();

            if (users.Count == 0)
            {
                AddDefaultAccount(users);
            }

            return repository.FindUser(login, password);
        }

        public User FindUser(string login, string password)
        {
            return repository.GetAll().Find((User p) => (p.Login == login) && (p.Password == password));
        }

        public static User FindUser(string login, string password, Role personRole, List<User> users)
        {
            return users.Find((User p) => (p.Login == login) && (p.Password == password) && (p.Role == personRole));
        }

        public void UpdateExistingUserBy(User user)
        {
            repository.Update(user);
        }
    }
}
