using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatBot
{
    public partial class MainWindow : Window
    {
        // Quiz state variables
        private List<QuizQuestion> _quizQuestions;
        private QuizQuestion _currentQuizQuestion;
        private int _quizScore = 0;
        private int _currentQuestionIndex = 0;
        private bool _quizInProgress = false;

        // Services
        private readonly TopicService _topicService = new TopicService();
        private readonly DisplayService _displayService = new DisplayService();
        private readonly ResponseService _responseService = new ResponseService();
        private readonly GreetingService _greetingService = new GreetingService();

        public MainWindow()
        {
            InitializeComponent();
            _displayService.AttachOutput(TipsOutputContentBlock);
            _greetingService.PlayWelcomeSound();
        }

        #region Quiz Game Methods

        private void CyberSecurityQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_quizInProgress)
            {
                StartNewQuiz();
                CyberSecurityQuestionsButton.Content = "End Quiz";
            }
            else
            {
                EndQuiz();
                CyberSecurityQuestionsButton.Content = "Start Quiz";
            }
        }

        private void StartNewQuiz()
        {
            _quizQuestions = CybersecurityQuestions.GetQuizSet();
            _quizScore = 0;
            _currentQuestionIndex = 0;
            _quizInProgress = true;

            ScorePanel.Visibility = Visibility.Visible;
            UpdateScoreDisplay();

            _currentQuizQuestion = _quizQuestions[_currentQuestionIndex];
            ShowQuestion(_currentQuizQuestion);
        }

        private void ShowQuestion(QuizQuestion question)
        {
            TipsOutputTitleBlock.Text = $"Question {_currentQuestionIndex + 1}/{_quizQuestions.Count}";
            TipsOutputContentBlock.Text = question.Question;

            QuizAnswersPanel.Children.Clear();
            QuizAnswersPanel.Visibility = Visibility.Visible;

            for (int i = 0; i < question.Options.Length; i++)
            {
                var radioButton = new RadioButton
                {
                    Content = $"{Convert.ToChar('A' + i)}) {question.Options[i]}",
                    Tag = i,
                    Style = (Style)FindResource("QuizAnswerRadioButton"),
                    Margin = new Thickness(5),
                    FontSize = 14
                };
                QuizAnswersPanel.Children.Add(radioButton);
            }

            UserFeedback.Text = "Select your answer and press Submit";
            SubmitAnswerButton.IsEnabled = true;
            SubmitAnswerButton.Visibility = Visibility.Visible;
        }

        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = -1;
            foreach (RadioButton rb in QuizAnswersPanel.Children)
            {
                if (rb.IsChecked == true)
                {
                    selectedIndex = (int)rb.Tag;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                UserFeedback.Text = "Please select an answer!";
                return;
            }

            EvaluateAnswer(selectedIndex);
        }

        private void EvaluateAnswer(int selectedIndex)
        {
            bool isCorrect = selectedIndex == _currentQuizQuestion.CorrectOptionIndex;

            if (isCorrect)
            {
                _quizScore++;
                UserFeedback.Text = $"✅ Correct! {_currentQuizQuestion.Explanation}";
            }
            else
            {
                UserFeedback.Text = $"❌ Incorrect. The correct answer is: {_currentQuizQuestion.GetCorrectAnswerLetter()}) {_currentQuizQuestion.GetCorrectAnswerText()}\n\n{_currentQuizQuestion.Explanation}";
            }

            UpdateScoreDisplay();
            SubmitAnswerButton.IsEnabled = false;

            // Move to next question or end quiz
            _currentQuestionIndex++;
            if (_currentQuestionIndex < _quizQuestions.Count)
            {
                _currentQuizQuestion = _quizQuestions[_currentQuestionIndex];
                ShowQuestion(_currentQuizQuestion);
            }
            else
            {
                EndQuiz();
            }
        }

        private void UpdateScoreDisplay()
        {
            ScoreTextBlock.Text = $"{_quizScore}/{_quizQuestions.Count}";
        }

        private void EndQuiz()
        {
            double percentage = (double)_quizScore / _quizQuestions.Count;
            string feedback;

            if (percentage >= 0.9) feedback = "Excellent! You're a cybersecurity expert!";
            else if (percentage >= 0.7) feedback = "Great job! You're knowledgeable about cybersecurity.";
            else if (percentage >= 0.5) feedback = "Good effort! You have some cybersecurity knowledge.";
            else feedback = "Keep learning! Cybersecurity is important for staying safe online.";

            TipsOutputTitleBlock.Text = "Quiz Complete!";
            TipsOutputContentBlock.Text = $"{feedback}\n\nYour final score: {_quizScore}/{_quizQuestions.Count}";
            QuizAnswersPanel.Visibility = Visibility.Collapsed;
            SubmitAnswerButton.Visibility = Visibility.Collapsed;
            _quizInProgress = false;
        }

        #endregion

        #region Existing Features

        private void QuickTipsButton_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            int tipType = random.Next(4);

            switch (tipType)
            {
                case 0:
                    _topicService.ShowPhishingTip(TipsOutputTitleBlock, TipsOutputContentBlock);
                    break;
                case 1:
                    _topicService.ShowPasswordTip(TipsOutputTitleBlock, TipsOutputContentBlock);
                    break;
                case 2:
                    _topicService.ShowSuspiciousLinkTip(TipsOutputTitleBlock, TipsOutputContentBlock);
                    break;
                case 3:
                    _topicService.ShowPrivacyTip(TipsOutputTitleBlock, TipsOutputContentBlock);
                    break;
            }

            UserFeedback.Text = "";
        }

        private void AskButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(userInput))
            {
                if (_quizInProgress)
                {
                    UserFeedback.Text = "Please use the quiz controls to answer questions.";
                    return;
                }

                string response = _responseService.GetResponse(userInput);
                _displayService.ShowUserMessage(userInput);
                _displayService.ShowBotMessage(response);
                UserInputTextBox.Text = "";
            }
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AskButton_Click(sender, e);
            }
        }

        private void UserInputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserInputTextBox.Text == "Type your message here...")
            {
                UserInputTextBox.Text = "";
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string taskText = TaskInputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(taskText))
            {
                TaskListBox.Items.Add(new ListBoxItem
                {
                    Content = taskText,
                    Foreground = Brushes.White,
                    Background = Brushes.Transparent
                });
                TaskInputTextBox.Text = "";
            }
        }

        #endregion

        #region Helper Methods

        private void ShowWelcomeMessage(string userName)
        {
            _displayService.ShowWelcome(userName);
            _displayService.ShowAsciiArt();
            _displayService.ShowBotMessage("Type any cybersecurity topic or click 'Quick Tips' for advice. Try 'phishing' or 'password safety' to start!");
        }

        #endregion
    }
}