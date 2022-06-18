using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ForeignLanguageApp.Data
{
    public class UserRepository : IUserRepository, IFileRepository
    {

        private const string DbPath = "users.db";

        private static string dataSource = $"Data Source={DbPath}";

        private const string CreateStorageCommandText = "CREATE TABLE Users(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Login TEXT NOT NULL UNIQUE, Password TEXT NOT NULL, Role TEXT NOT NULL)";

        private const string FindUserCommandText = "SELECT * FROM Users WHERE Login=@login";

        private const string FindUserCommandText2 = "SELECT * FROM Users WHERE Login=@login AND Password=@password";

        private const string InsertCommandText = "INSERT INTO Users (Login, Password, Role) VALUES (@login, @password, @role)";

        private const string UpdateCommandText = "UPDATE Users SET Login=@login, Password=@password, Role=@role WHERE Id=@Id";

        private const string GetAllCommandText = "SELECT * FROM Users";

        private const string DeleteCommandText = "DELETE FROM Users WHERE Id=@Id";

        public UserRepository()
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }
        }

        public void CreateStorage()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = CreateStorageCommandText;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStorage()
        {
            File.Delete(DbPath);
        }

        public bool CheckIfStorageExists()
        {
            return File.Exists(DbPath);
        }

        public string GetStorageName()
        {
            return DbPath;
        }

        public User FindUser(string login)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            User user = null;

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(FindUserCommandText, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@login", login));

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User((string)reader["Login"], (string)reader["Password"], (Role)Enum.Parse(typeof(Role), (string)reader["Role"]), Convert.ToInt32(reader["Id"]));
                        }
                    }
                }
            }

            return user;
        }

        public User FindUser(string login, string password)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            User user = null;

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(FindUserCommandText2, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@login", login));

                    command.Parameters.Add(new SQLiteParameter("@password", password));

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User((string)reader["Login"], (string)reader["Password"], (Role)Enum.Parse(typeof(Role), (string)reader["Role"]), Convert.ToInt32(reader["Id"]));
                        }
                    }
                }
            }

            return user;
        }

        public List<User> GetAll()
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            List<User> users = new List<User>();

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(GetAllCommandText, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User((string)reader["Login"], (string)reader["Password"], (Role)Enum.Parse(typeof(Role), (string)reader["Role"]), Convert.ToInt32(reader["Id"]));

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public void Update(User user)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                string sqlExpression = UpdateCommandText;

                using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@login", user.Login));

                    command.Parameters.Add(new SQLiteParameter("@password", user.Password));

                    command.Parameters.Add(new SQLiteParameter("@role", user.Role));

                    command.Parameters.Add(new SQLiteParameter("@Id", user.Id));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Add(User user)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                string sqlExpression = InsertCommandText;

                using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@login", user.Login));

                    command.Parameters.Add(new SQLiteParameter("@password", user.Password));

                    command.Parameters.Add(new SQLiteParameter("@role", user.Role.ToString()));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(User user)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                string sqlExpression = DeleteCommandText;

                using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", user.Id));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
