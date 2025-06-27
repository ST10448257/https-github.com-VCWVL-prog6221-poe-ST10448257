using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CyberSecurityChatBot
{
    // Model representing a quiz question
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public ObservableCollection<string> Choices { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public QuizQuestion(string questionText, IEnumerable<string> choices, int correctAnswerIndex)
        {
            QuestionText = questionText;
            Choices = new ObservableCollection<string>(choices);
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }

    // Static service class to provide quiz questions
    public static class CybersecurityQuestion
    {
        private static readonly List<QuizQuestion> AllQuestions = new()
        {
            new QuizQuestion(
                "What is phishing?",
                new List<string> {
                    "A legitimate email from your bank",
                    "A type of scam pretending to be a trustworthy entity",
                    "A firewall setting",
                    "A password manager feature"
                },
                1
            ),
            new QuizQuestion(
                "What is a strong password?",
                new List<string> {
                    "Your birthdate",
                    "Password123",
                    "A long combination of letters, numbers, and symbols",
                    "The word 'password'"
                },
                2
            ),
            new QuizQuestion(
                "What should you do before clicking on a link in an email?",
                new List<string> {
                    "Click immediately",
                    "Hover to check the URL and verify the sender",
                    "Reply asking for confirmation",
                    "Ignore all emails"
                },
                1
            ),
            new QuizQuestion(
                "Which of the following helps protect your online privacy?",
                new List<string> {
                    "Sharing passwords",
                    "Using public Wi-Fi without VPN",
                    "Limiting personal info shared on social media",
                    "Clicking on all links"
                },
                2
            ),
            new QuizQuestion(
                "What is multi-factor authentication?",
                new List<string> {
                    "Using one password",
                    "Using a password and an additional verification method",
                    "Logging in without a password",
                    "Sharing passwords with friends"
                },
                1
            ),
            // Add more questions as needed
        };

        // Return a randomized quiz set of up to numberOfQuestions
        public static ObservableCollection<QuizQuestion> GetQuizSet(int numberOfQuestions)
        {
            var rnd = new Random();
            var shuffled = new List<QuizQuestion>(AllQuestions);

            for (int i = 0; i < shuffled.Count; i++)
            {
                int swapIndex = rnd.Next(i, shuffled.Count);
                (shuffled[i], shuffled[swapIndex]) = (shuffled[swapIndex], shuffled[i]);
            }

            var selected = shuffled.GetRange(0, Math.Min(numberOfQuestions, shuffled.Count));
            return new ObservableCollection<QuizQuestion>(selected);
        }
    }
}
