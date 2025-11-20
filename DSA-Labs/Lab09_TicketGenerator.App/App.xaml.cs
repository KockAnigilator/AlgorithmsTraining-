using System.Windows;
using Lab09_TicketGenerator.App.Views;
using Lab09_TicketGenerator.Database;
using Lab09_TicketGenerator.Repositories;
using Lab09_TicketGenerator.Services;

namespace Lab09_TicketGenerator.App
{
    public partial class App : Application
    {
        public static DatabaseService DatabaseService { get; private set; }
        public static QuestionRepository QuestionRepository { get; private set; }
        public static TicketSettingsRepository SettingsRepository { get; private set; }
        public static TicketGeneratorService TicketGenerator { get; private set; }

        /// <summary>
        /// Обработчик запуска приложения
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Инициализация сервисов
            InitializeServices();

            // Создание и отображение главного окна
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /// <summary>
        /// Инициализация сервисов приложения
        /// </summary>
        private void InitializeServices()
        {
            DatabaseService = new DatabaseService();
            QuestionRepository = new QuestionRepository(DatabaseService);
            SettingsRepository = new TicketSettingsRepository(DatabaseService);
            TicketGenerator = new TicketGeneratorService(QuestionRepository, SettingsRepository);
        }
    }
}