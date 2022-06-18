using ForeignLanguageApp.Interfaces;
using ForeignLanguageApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;

namespace ForeignLanguageApp.Data
{
    public class TopicRepository : IBaseRepository<Topic>, IFileRepository
    {
        private readonly string DbPath = "UserTopics.db";

        private readonly string dataSource;  

        private const string CreateStorageCommandText = "CREATE TABLE Topics(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, CreationTime TEXT, Title TEXT NOT NULL UNIQUE, Cards TEXT NOT NULL)";

        private const string GetAllCommandText = "SELECT * FROM Topics";

        private const string UpdateCommandText = "UPDATE Topics SET Title=@title, Cards=@cards WHERE Id=@id";

        private const string DeleteCommandText = "DELETE FROM Topics WHERE Id=@id";

        private const string AddCommandText = "INSERT INTO Topics (Title, Cards, creationTime) VALUES (@title, @cards, @creationTime)";

        public TopicRepository(int userId)
        {
            DbPath = DbPath.Insert(4, Convert.ToString(userId));

            dataSource = $"Data Source={DbPath}";

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

        public List<Topic> GetAll()
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            List<Topic> topics = new List<Topic>();

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(GetAllCommandText, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                List<Card> cards = JsonSerializer.Deserialize<List<Card>>((string)reader["Cards"]);

                                Topic topic = new Topic((string)reader["Title"], cards, Convert.ToInt32(reader["Id"]), Convert.ToDateTime(reader["CreationTime"]));

                                topics.Add(topic);
                            }
                            catch
                            {
                                new SQLiteCommand($"DELETE FROM Topics WHERE ID={Convert.ToInt32(reader["Id"])}", connection).ExecuteNonQuery();

                                break;
                            }
                        }
                    }
                }
            }

            return topics;
        }

        public void Update(Topic topic)
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
                    command.Parameters.Add(new SQLiteParameter("@title", topic.Title));

                    command.Parameters.Add(new SQLiteParameter("@cards", JsonSerializer.Serialize(topic.Cards)));

                    command.Parameters.Add(new SQLiteParameter("@Id", topic.Id));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Add(Topic topic)
        {
            if (!CheckIfStorageExists())
            {
                CreateStorage();
            }

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                string sqlExpression = AddCommandText;

                using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@title", topic.Title));

                    command.Parameters.Add(new SQLiteParameter("@cards", JsonSerializer.Serialize(topic.Cards)));

                    command.Parameters.Add(new SQLiteParameter("@creationTime", topic.CreationTime));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Topic topic)
        {
            if (!CheckIfStorageExists())
            {
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(dataSource))
            {
                connection.Open();

                string sqlExpression = DeleteCommandText;

                using (SQLiteCommand command = new SQLiteCommand(sqlExpression, connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@id", topic.Id));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
