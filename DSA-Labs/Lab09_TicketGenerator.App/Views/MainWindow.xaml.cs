using System;
using System.Windows;
using Lab09_TicketGenerator.App.Views;
using Lab09_TicketGenerator.Repositories;
using Lab09_TicketGenerator.Services;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class MainWindow : Window
    {
        private readonly QuestionRepository _questionRepository;
        private readonly TicketSettingsRepository _settingsRepository;
        private readonly TicketGeneratorService _ticketGenerator;

        public MainWindow(QuestionRepository questionRepository,
                         TicketSettingsRepository settingsRepository,
                         TicketGeneratorService ticketGenerator)
        {
            InitializeComponent();

            _questionRepository = questionRepository;
            _settingsRepository = settingsRepository;
            _ticketGenerator = ticketGenerator;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            try
            {
                var stats = _ticketGenerator.GetQuestionsStatistics();

                TxtPracticeCount.Text = $"Практика: {stats["Practice"]}";
                TxtLectureCount.Text = $"Лекции: {stats["Lecture"]}";
                TxtBlitzCount.Text = $"Блиц: {stats["Blitz"]}";
                TxtTotalCount.Text = $"Всего вопросов: {stats["Total"]}";
                TxtTopicsCount.Text = $"Тем: {stats["Topics"]}";

                bool canGenerate = _ticketGenerator.CanGenerateTickets();
                TxtGenerationStatus.Text = canGenerate ?
                    "Можно генерировать билеты" :
                    "Недостаточно вопросов для генерации";
                TxtGenerationStatus.Foreground = canGenerate ?
                    System.Windows.Media.Brushes.DarkGreen :
                    System.Windows.Media.Brushes.Red;

                TxtStatus.Text = "Статистика обновлена";
            }
            catch (Exception ex)
            {
                TxtStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void BtnManageQuestions_Click(object sender, RoutedEventArgs e)
        {
            var window = new ManageQuestionsWindow(_questionRepository);
            window.Owner = this;
            window.QuestionChanged += (s, args) => UpdateStatistics();
            window.ShowDialog();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow(_settingsRepository);
            window.Owner = this;
            window.SettingsChanged += (s, args) => UpdateStatistics();
            window.ShowDialog();
        }

        private void BtnGenerateTickets_Click(object sender, RoutedEventArgs e)
        {
            var window = new GenerateTicketsWindow(_ticketGenerator);
            window.Owner = this;
            window.ShowDialog();
        }

        private void BtnViewQuestions_Click(object sender, RoutedEventArgs e)
        {
            var window = new ViewQuestionsWindow(_questionRepository);
            window.Owner = this;
            window.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Генератор экзаменационных билетов\nВерсия 1.0", "О программе");
        }
    }
}