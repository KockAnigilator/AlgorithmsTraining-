using System.Windows;
using Lab09_TicketGenerator.App.Views;
using Lab09_TicketGenerator.Database;
using Lab09_TicketGenerator.Repositories;
using Lab09_TicketGenerator.Services;

namespace Lab09_TicketGenerator.App
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                // Создаем сервисы и репозитории
                var dbService = new DatabaseService();
                dbService.TestConnection(); // Проверяем подключение

                var questionRepository = new QuestionRepository(dbService);
                var settingsRepository = new TicketSettingsRepository(dbService);
                var ticketGenerator = new TicketGeneratorService(questionRepository, settingsRepository);

                // Создаем главное окно с передачей всех зависимостей
                var mainWindow = new MainWindow(questionRepository, settingsRepository, ticketGenerator);
                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}