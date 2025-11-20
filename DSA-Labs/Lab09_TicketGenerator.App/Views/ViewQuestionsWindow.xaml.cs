using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab09_TicketGenerator.Models;
using Lab09_TicketGenerator.Repositories;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class ViewQuestionsWindow : Window
    {
        private readonly QuestionRepository _questionRepository;
        private List<Question> _allQuestions;

        public ViewQuestionsWindow(QuestionRepository questionRepository)
        {
            InitializeComponent();
            _questionRepository = questionRepository;
            LoadQuestions();
            LoadTopics();
        }

        private void LoadQuestions()
        {
            try
            {
                _allQuestions = _questionRepository.GetAllQuestions();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}", "Ошибка");
            }
        }

        private void LoadTopics()
        {
            try
            {
                var topics = _questionRepository.GetAllTopics();
                CmbFilterTopic.Items.Clear();
                CmbFilterTopic.Items.Add("Все темы");
                foreach (var topic in topics)
                {
                    CmbFilterTopic.Items.Add(topic);
                }
                CmbFilterTopic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тем: {ex.Message}", "Ошибка");
            }
        }

        private void ApplyFilters()
        {
            if (_allQuestions == null) return;

            var filtered = _allQuestions.AsEnumerable();

            // Фильтр по типу
            var selectedType = (CmbFilterType.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedType != null && selectedType != "Все")
            {
                var type = (QuestionType)Enum.Parse(typeof(QuestionType), selectedType);
                filtered = filtered.Where(q => q.Type == type);
            }

            // Фильтр по теме
            var selectedTopic = CmbFilterTopic.SelectedItem as string;
            if (selectedTopic != null && selectedTopic != "Все темы")
            {
                filtered = filtered.Where(q => q.Topic == selectedTopic);
            }

            QuestionsGrid.ItemsSource = filtered.ToList();
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            CmbFilterType.SelectedIndex = 0;
            CmbFilterTopic.SelectedIndex = 0;
            ApplyFilters();
        }
    }
}