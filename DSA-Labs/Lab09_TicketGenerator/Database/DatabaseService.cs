using System;
using System.Data.SQLite;
using System.IO;

namespace Lab09_TicketGenerator.Database
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            // ПРЯМОЙ ПУТЬ к вашей базе данных
            string databasePath = @"C:\Users\user\Documents\GitHub\AlgorithmsTraining-\DSA-Labs\Lab09_TicketGenerator\Database\tickets.db";

            // Создаем директорию если не существует
            string directory = Path.GetDirectoryName(databasePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Создана директория: {directory}");
            }

            // Проверяем существует ли файл
            bool databaseExists = File.Exists(databasePath);

            _connectionString = $"Data Source={databasePath};Version=3;";

            Console.WriteLine($"Подключаемся к БД: {databasePath}");
            Console.WriteLine($"Файл существует: {databaseExists}");

            // Инициализируем базу данных при первом запуске
            if (!databaseExists)
            {
                InitializeDatabase();
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        private void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Создаем таблицы
                string createQuestionsTable = @"
                    CREATE TABLE IF NOT EXISTS Questions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Text TEXT NOT NULL,
                        Type TEXT NOT NULL,
                        Topic TEXT NOT NULL,
                        Difficulty INTEGER NOT NULL,
                        IsActive BOOLEAN NOT NULL DEFAULT 1
                    )";

                string createSettingsTable = @"
                    CREATE TABLE IF NOT EXISTS TicketSettings (
                        Id INTEGER PRIMARY KEY,
                        QuestionsPerTicket INTEGER NOT NULL DEFAULT 4,
                        MaxTotalDifficulty INTEGER NOT NULL DEFAULT 20,
                        PracticeCount INTEGER NOT NULL DEFAULT 1,
                        LectureCount INTEGER NOT NULL DEFAULT 2,
                        BlitzCount INTEGER NOT NULL DEFAULT 1,
                        LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                using (var cmd = new SQLiteCommand(createQuestionsTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createSettingsTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Вставляем настройки по умолчанию
                string insertDefaultSettings = @"
                    INSERT OR IGNORE INTO TicketSettings (Id, QuestionsPerTicket, MaxTotalDifficulty, PracticeCount, LectureCount, BlitzCount)
                    VALUES (1, 4, 20, 1, 2, 1)";

                using (var cmd = new SQLiteCommand(insertDefaultSettings, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("База данных инициализирована");
            }
        }

        public void TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    Console.WriteLine("Подключение к БД успешно!");

                    // Проверяем таблицы
                    using (var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Таблицы в базе:");
                        while (reader.Read())
                        {
                            Console.WriteLine($" - {reader.GetString(0)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                throw;
            }
        }
    }
}