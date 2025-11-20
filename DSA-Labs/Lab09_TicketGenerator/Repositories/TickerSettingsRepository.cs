using System;
using System.Data.SQLite;
using Lab09_TicketGenerator.Database;
using Lab09_TicketGenerator.Interfaces;
using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Repositories
{
    /// <summary>
    /// Репозиторий для работы с настройками билетов в базе данных
    /// </summary>
    public class TicketSettingsRepository : ITicketSettingsRepository
    {
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Конструктор репозитория настроек
        /// </summary>
        public TicketSettingsRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Получает текущие настройки билетов
        /// </summary>
        public TicketSettings GetSettings()
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM TicketSettings WHERE Id = 1";
                using (var cmd = new SQLiteCommand(sql, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TicketSettings
                        {
                            QuestionsPerTicket = Convert.ToInt32(reader["QuestionsPerTicket"]),
                            MaxTotalDifficulty = Convert.ToInt32(reader["MaxTotalDifficulty"]),
                            PracticeCount = Convert.ToInt32(reader["PracticeCount"]),
                            LectureCount = Convert.ToInt32(reader["LectureCount"]),
                            BlitzCount = Convert.ToInt32(reader["BlitzCount"])
                        };
                    }
                }
            }

            // Возвращаем настройки по умолчанию, если не найдены в БД
            return new TicketSettings();
        }

        /// <summary>
        /// Обновляет настройки билетов
        /// </summary>
        public void UpdateSettings(TicketSettings settings)
        {
            using (var connection = _dbService.GetConnection())
            {
                connection.Open();

                string sql = @"
                    UPDATE TicketSettings 
                    SET QuestionsPerTicket = @questionsPerTicket,
                        MaxTotalDifficulty = @maxTotalDifficulty,
                        PracticeCount = @practiceCount,
                        LectureCount = @lectureCount,
                        BlitzCount = @blitzCount,
                        LastModified = CURRENT_TIMESTAMP
                    WHERE Id = 1";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@questionsPerTicket", settings.QuestionsPerTicket);
                    cmd.Parameters.AddWithValue("@maxTotalDifficulty", settings.MaxTotalDifficulty);
                    cmd.Parameters.AddWithValue("@practiceCount", settings.PracticeCount);
                    cmd.Parameters.AddWithValue("@lectureCount", settings.LectureCount);
                    cmd.Parameters.AddWithValue("@blitzCount", settings.BlitzCount);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Если запись не существует, создаем новую
                    if (rowsAffected == 0)
                    {
                        InsertSettings(settings, connection);
                    }
                }
            }
        }

        /// <summary>
        /// Вставляет новые настройки в базу данных
        /// </summary>
        private void InsertSettings(TicketSettings settings, SQLiteConnection connection)
        {
            string sql = @"
                INSERT INTO TicketSettings (Id, QuestionsPerTicket, MaxTotalDifficulty, PracticeCount, LectureCount, BlitzCount)
                VALUES (1, @questionsPerTicket, @maxTotalDifficulty, @practiceCount, @lectureCount, @blitzCount)";

            using (var cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@questionsPerTicket", settings.QuestionsPerTicket);
                cmd.Parameters.AddWithValue("@maxTotalDifficulty", settings.MaxTotalDifficulty);
                cmd.Parameters.AddWithValue("@practiceCount", settings.PracticeCount);
                cmd.Parameters.AddWithValue("@lectureCount", settings.LectureCount);
                cmd.Parameters.AddWithValue("@blitzCount", settings.BlitzCount);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Сбрасывает настройки к значениям по умолчанию
        /// </summary>
        public void ResetToDefaults()
        {
            var defaultSettings = new TicketSettings();
            UpdateSettings(defaultSettings);
        }
    }
}