using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с настройками билетов
    /// </summary>
    public interface ITicketSettingsRepository
    {
        /// <summary>
        /// Получает текущие настройки билетов
        /// </summary>
        TicketSettings GetSettings();

        /// <summary>
        /// Обновляет настройки билетов
        /// </summary>
        void UpdateSettings(TicketSettings settings);
    }
}