using System;
using System.Windows;
using System.Windows.Controls;
using Lab09_TicketGenerator.Models;
using Lab09_TicketGenerator.Repositories;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class ManageQuestionsWindow : Window
    {
        private readonly QuestionRepository _questionRepository;

        public event EventHandler QuestionChanged;

        public ManageQuestionsWindow(QuestionRepository questionRepository)
        {
            InitializeComponent();
            _questionRepository = questionRepository;
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            try
            {
                var questions = _questionRepository.GetAllQuestions();
                QuestionsGrid.ItemsSource = questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}", "Ошибка");
            }
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new QuestionEditDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _questionRepository.AddQuestion(dialog.Question);
                    LoadQuestions();
                    QuestionChanged?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("Вопрос добавлен успешно!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления вопроса: {ex.Message}", "Ошибка");
                }
            }
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsGrid.SelectedItem is Question selectedQuestion)
            {
                var dialog = new QuestionEditDialog(selectedQuestion);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _questionRepository.UpdateQuestion(dialog.Question);
                        LoadQuestions();
                        QuestionChanged?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Вопрос обновлен успешно!", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обновления вопроса: {ex.Message}", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите вопрос для редактирования", "Внимание");
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsGrid.SelectedItem is Question selectedQuestion)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот вопрос?",
                    "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _questionRepository.DeleteQuestion(selectedQuestion.Id);
                        LoadQuestions();
                        QuestionChanged?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show("Вопрос удален успешно!", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления вопроса: {ex.Message}", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите вопрос для удаления", "Внимание");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestions();
        }
    }
}