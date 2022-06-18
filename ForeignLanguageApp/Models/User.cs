using ForeignLanguageApp.UI;
using System;

namespace ForeignLanguageApp.Models
{
    public class User : ICloneable
    {
        public int Id { get; }

        private string login;

        public string Login 
        {
            get { return login; }
            private set
            {
                if (!(value.Length >= 3))
                {
                    throw new ArgumentException(Gui.LoginNotMeetRequirementsError);
                }

                login = value;
            }
        }

        private string password;

        public string Password 
        { 
            get { return password; }
            private set
            {
                if (!(value.Length >= 3))
                {
                    throw new ArgumentException(Gui.PasswordNotMeetRequirementsError);
                }

                password = value;
            }
        }

        public Role Role { get; private set; }

        private const int DefaultId = 0;

        public User(string login, string password, Role role = Role.User, int id = DefaultId)
        {
            Id = id;

            Login = login;

            Password = password;

            Role = role;
        }

        public void Update(string login, string password, Role role)
        {
            Login = login;

            Password = password;

            Role = role;
        }

        public object Clone()
        {
            return new User(Login, Password, Role, Id);
        }
    }
}
