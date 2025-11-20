using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Lab09_TicketGenerator.Models;
using Lab09_TicketGenerator.Services;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class GenerateTicketsWindow : Window
    {
        private readonly TicketGeneratorService _ticketGenerator;
        private List<Ticket> _currentTickets;

        public GenerateTicketsWindow(TicketGeneratorService ticketGenerator)
        {
            InitializeComponent();
            _ticketGenerator = ticketGenerator;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtTicketCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Введите корректное количество билетов", "Ошибка");
                    return;
                }

                string mode = "standard";
                if (CmbMode.SelectedIndex == 1) mode = "balanced";
                else if (CmbMode.SelectedIndex == 2) mode = "mixed";

                _currentTickets = _ticketGenerator.GenerateTickets(count, mode);

                // Обновляем интерфейс
                TicketsList.ItemsSource = _currentTickets;
                UpdatePreview();

                TxtStatus.Text = $"Успешно сгенерировано {_currentTickets.Count} билетов";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации билетов: {ex.Message}", "Ошибка");
            }
        }

        private void UpdatePreview()
        {
            if (_currentTickets == null) return;

            var sb = new StringBuilder();
            foreach (var ticket in _currentTickets)
            {
                sb.AppendLine($"=== {ticket.Name} (Сложность: {ticket.TotalDifficulty}) ===");
                foreach (var question in ticket.Questions)
                {
                    sb.AppendLine($"  - {question.Text} [{question.Type}] (Сложность: {question.Difficulty})");
                }
                sb.AppendLine();
            }

            TxtPreview.Text = sb.ToString();
        }
    }
}