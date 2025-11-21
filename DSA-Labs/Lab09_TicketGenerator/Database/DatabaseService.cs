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

            Console.WriteLine($"Используем базу данных: {databasePath}");
            Console.WriteLine($"Файл существует: {File.Exists(databasePath)}");

            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException($"Файл базы данных не найден: {databasePath}");
            }

            _connectionString = $"Data Source={databasePath};Version=3;";

            TestConnection();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    Console.WriteLine("✓ Подключение к БД успешно!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Ошибка подключения: {ex.Message}");
                throw;
            }
        }

        public string GetDatabasePath()
        {
            return _connectionString.Replace("Data Source=", "").Replace(";Version=3;", "");
        }
    }
}