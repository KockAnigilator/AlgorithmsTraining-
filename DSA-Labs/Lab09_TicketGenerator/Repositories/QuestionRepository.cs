using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Lab09_TicketGenerator.Database;
using Lab09_TicketGenerator.Interfaces;
using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Repositories
{
    /// <summary>
    /// Репозиторий для работы с вопросами в базе данных
    /// </summary>
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        public QuestionRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public void AddQuestion(Question question)
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = @"INSERT INTO Questions (Text, Type, Topic, Difficulty, IsActive) 
                              VALUES (@text, @type, @topic, @difficulty, 1)";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@text", question.Text);
                    cmd.Parameters.AddWithValue("@type", question.Type.ToString());
                    cmd.Parameters.AddWithValue("@topic", question.Topic);
                    cmd.Parameters.AddWithValue("@difficulty", question.Difficulty);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateQuestion(Question question)
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = @"UPDATE Questions 
                              SET Text = @text, Type = @type, Topic = @topic, 
                                  Difficulty = @difficulty, IsActive = @isActive
                              WHERE Id = @id";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@text", question.Text);
                    cmd.Parameters.AddWithValue("@type", question.Type.ToString());
                    cmd.Parameters.AddWithValue("@topic", question.Topic);
                    cmd.Parameters.AddWithValue("@difficulty", question.Difficulty);
                    cmd.Parameters.AddWithValue("@isActive", question.IsActive);
                    cmd.Parameters.AddWithValue("@id", question.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteQuestion(int id)
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "DELETE FROM Questions WHERE Id = @id";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Question> GetAllQuestions()
        {
            var questions = new List<Question>();

            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM Questions ORDER BY Topic, Type, Difficulty";
                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Text = reader["Text"].ToString(),
                            Type = (QuestionType)Enum.Parse(typeof(QuestionType), reader["Type"].ToString()),
                            Topic = reader["Topic"].ToString(),
                            Difficulty = Convert.ToInt32(reader["Difficulty"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        });
                    }
                }
            }

            return questions;
        }

        public List<Question> GetActiveQuestions()
        {
            var questions = new List<Question>();

            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM Questions WHERE IsActive = 1 ORDER BY Topic, Type, Difficulty";
                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Text = reader["Text"].ToString(),
                            Type = (QuestionType)Enum.Parse(typeof(QuestionType), reader["Type"].ToString()),
                            Topic = reader["Topic"].ToString(),
                            Difficulty = Convert.ToInt32(reader["Difficulty"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        });
                    }
                }
            }

            return questions;
        }

        public List<Question> GetQuestionsByType(QuestionType type)
        {
            var questions = new List<Question>();

            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM Questions WHERE Type = @type AND IsActive = 1 ORDER BY Topic, Difficulty";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@type", type.ToString());

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new Question
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Text = reader["Text"].ToString(),
                                Type = (QuestionType)Enum.Parse(typeof(QuestionType), reader["Type"].ToString()),
                                Topic = reader["Topic"].ToString(),
                                Difficulty = Convert.ToInt32(reader["Difficulty"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }

            return questions;
        }

        public List<Question> GetQuestionsByTopic(string topic)
        {
            var questions = new List<Question>();

            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM Questions WHERE Topic = @topic AND IsActive = 1 ORDER BY Type, Difficulty";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@topic", topic);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new Question
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Text = reader["Text"].ToString(),
                                Type = (QuestionType)Enum.Parse(typeof(QuestionType), reader["Type"].ToString()),
                                Topic = reader["Topic"].ToString(),
                                Difficulty = Convert.ToInt32(reader["Difficulty"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }

            return questions;
        }

        public List<string> GetAllTopics()
        {
            var topics = new List<string>();

            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT DISTINCT Topic FROM Questions WHERE IsActive = 1 ORDER BY Topic";
                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topics.Add(reader["Topic"].ToString());
                    }
                }
            }

            return topics;
        }

        public void SetQuestionActive(int id, bool isActive)
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "UPDATE Questions SET IsActive = @isActive WHERE Id = @id";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}