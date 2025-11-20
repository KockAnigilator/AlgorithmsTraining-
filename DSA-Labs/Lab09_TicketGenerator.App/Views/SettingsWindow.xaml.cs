using System;
using System.Windows;
using Lab09_TicketGenerator.Models;
using Lab09_TicketGenerator.Repositories;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class SettingsWindow : Window
    {
        private readonly TicketSettingsRepository _settingsRepository;
        public event EventHandler SettingsChanged;

        public SettingsWindow(TicketSettingsRepository settingsRepository)
        {
            InitializeComponent();
            _settingsRepository = settingsRepository;
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                var settings = _settingsRepository.GetSettings();
                TxtQuestionsPerTicket.Text = settings.QuestionsPerTicket.ToString();
                TxtMaxDifficulty.Text = settings.MaxTotalDifficulty.ToString();
                TxtPracticeCount.Text = settings.PracticeCount.ToString();
                TxtLectureCount.Text = settings.LectureCount.ToString();
                TxtBlitzCount.Text = settings.BlitzCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки настроек: {ex.Message}", "Ошибка");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var settings = new TicketSettings
                {
                    QuestionsPerTicket = int.Parse(TxtQuestionsPerTicket.Text),
                    MaxTotalDifficulty = int.Parse(TxtMaxDifficulty.Text),
                    PracticeCount = int.Parse(TxtPracticeCount.Text),
                    LectureCount = int.Parse(TxtLectureCount.Text),
                    BlitzCount = int.Parse(TxtBlitzCount.Text)
                };

                _settingsRepository.UpdateSettings(settings);
                SettingsChanged?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Настройки сохранены успешно!", "Успех");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения настроек: {ex.Message}", "Ошибка");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}