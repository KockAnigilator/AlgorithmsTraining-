using System;
using System.Data.SQLite;
using System.IO;

namespace Lab09_TicketGenerator.Database
{
    /// <summary>
    /// Сервис для работы с базой данных SQLite
    /// </summary>
    public class DatabaseService
    {
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор сервиса базы данных
        /// </summary>
        /// <param name="databasePath">Путь к файлу базы данных</param>
        public DatabaseService(string databasePath = "tickets.db")
        {
            _connectionString = $"Data Source={databasePath};Version=3;";
            InitializeDatabase();
        }

        /// <summary>
        /// Инициализирует базу данных и создает таблицы при необходимости
        /// </summary>
        private void InitializeDatabase()
        {
            if (!File.Exists("tickets.db"))
            {
                SQLiteConnection.CreateFile("tickets.db");
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Таблица заданий
                string createQuestionsTable = @"
                    CREATE TABLE IF NOT EXISTS Questions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Text TEXT NOT NULL,
                        Type TEXT NOT NULL CHECK(Type IN ('Practice', 'Lecture', 'Blitz')),
                        Topic TEXT NOT NULL,
                        Difficulty INTEGER NOT NULL CHECK(Difficulty >= 1 AND Difficulty <= 10),
                        IsActive BOOLEAN NOT NULL DEFAULT 1
                    )";

                // Таблица настроек билетов
                string createTicketSettingsTable = @"
                    CREATE TABLE IF NOT EXISTS TicketSettings (
                        Id INTEGER PRIMARY KEY,
                        QuestionsPerTicket INTEGER NOT NULL DEFAULT 4,
                        MaxTotalDifficulty INTEGER NOT NULL DEFAULT 20,
                        PracticeCount INTEGER NOT NULL DEFAULT 1,
                        LectureCount INTEGER NOT NULL DEFAULT 2,
                        BlitzCount INTEGER NOT NULL DEFAULT 1,
                        LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                using (var cmd1 = new SQLiteCommand(createQuestionsTable, connection))
                using (var cmd2 = new SQLiteCommand(createTicketSettingsTable, connection))
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }

                // Добавляем настройки по умолчанию
                string insertSettings = @"
                    INSERT OR IGNORE INTO TicketSettings (Id, QuestionsPerTicket, MaxTotalDifficulty, PracticeCount, LectureCount, BlitzCount)
                    VALUES (1, 4, 20, 1, 2, 1)";

                using (var cmd3 = new SQLiteCommand(insertSettings, connection))
                {
                    cmd3.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Возвращает соединение с базой данных
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}