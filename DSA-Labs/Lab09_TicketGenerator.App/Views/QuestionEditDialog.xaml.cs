using Lab09_TicketGenerator.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab09_TicketGenerator.App.Views
{
    public partial class QuestionEditDialog : Window
    {
        public Question Question { get; private set; }

        public QuestionEditDialog()
        {
            InitializeComponent();
            Question = new Question();
        }

        public QuestionEditDialog(Question question)
        {
            InitializeComponent();
            Question = question;
            TxtText.Text = question.Text;
            TxtTopic.Text = question.Topic;
            TxtDifficulty.Text = question.Difficulty.ToString();
            ChkActive.IsChecked = question.IsActive;

            // Устанавливаем тип
            foreach (ComboBoxItem item in CmbType.Items)
            {
                if (item.Content.ToString() == question.Type.ToString())
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtText.Text))
            {
                MessageBox.Show("Введите текст вопроса", "Ошибка");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtTopic.Text))
            {
                MessageBox.Show("Введите тему вопроса", "Ошибка");
                return;
            }

            if (!int.TryParse(TxtDifficulty.Text, out int difficulty) || difficulty < 1 || difficulty > 10)
            {
                MessageBox.Show("Сложность должна быть числом от 1 до 10", "Ошибка");
                return;
            }

            Question.Text = TxtText.Text;
            Question.Topic = TxtTopic.Text;
            Question.Difficulty = difficulty;
            Question.IsActive = ChkActive.IsChecked ?? true;

            var selectedType = (CmbType.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedType != null)
            {
                Question.Type = (QuestionType)Enum.Parse(typeof(QuestionType), selectedType);
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}