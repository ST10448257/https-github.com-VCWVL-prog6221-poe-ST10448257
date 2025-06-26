using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatBot
{
    public partial class MainWindow : Window
    {
        private DisplayService displayService;
        private ResponseService responseService;

        private string userName = "Guest";
        private bool isAskingName = true;
        private bool quizActive = false;
        private int quizIndex = 0;
        private int quizScore = 0;
        private QuizQuestion[] quizQuestions;
        private bool waitingForTopic = false;
        private bool postQuizPrompt = false;

        public MainWindow()
        {
            InitializeComponent();

            displayService = new DisplayService();
            responseService = new ResponseService();

            displayService.AttachOutput(OutputTextBox); // Connect DisplayService to TextBox

            displayService.ShowAsciiArt();
            displayService.ShowWelcome(userName);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
                e.Handled = true;
            }
        }

        private void ProcessUserInput()
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddUserMessage(input);
            InputTextBox.Clear();

            if (isAskingName)
            {
                // Set user name (simple validation example)
                if (input.Length < 2)
                {
                    AddBotMessage("Please enter a valid name with at least 2 characters.");
                    return;
                }
                userName = input;
                isAskingName = false;
                AddBotMessage($"🎉 Welcome {userName}! Would you like to learn about cybersecurity topics? (yes/no)");
                return;
            }

            string lowInput = input.ToLower();

            if (quizActive)
            {
                HandleQuizAnswer(input);
                return;
            }

            if (lowInput == "quiz")
            {
                StartQuiz();
                return;
            }

            if (lowInput == "menu")
            {
                ShowMenu();
                return;
            }

            if (postQuizPrompt)
            {
                postQuizPrompt = false;
                if (lowInput == "yes" || lowInput == "y")
                {
                    ShowMenu();
                    return;
                }
                else
                {
                    AddBotMessage("Alright! You can always type 'menu' or a topic name later.");
                    return;
                }
            }

            if (waitingForTopic)
            {
                HandleTopicRequest(input);
                return;
            }

            if (lowInput == "yes" || lowInput == "y")
            {
                AddBotMessage("Great! Please type the cybersecurity topic you want to learn about (e.g., phishing, firewalls):");
                waitingForTopic = true;
                return;
            }
            else if (lowInput == "no" || lowInput == "n")
            {
                AddBotMessage("Okay! You can always type 'quiz' to start the cybersecurity quiz or 'menu' to see topic list.");
                return;
            }

            // Default fallback response
            string response = responseService.GetResponse(input);
            AddBotMessage(response);
        }

        // ==== Helper Methods ====

        private void AddBotMessage(string message)
        {
            displayService.ShowBotMessage(message);
        }

        private void AddUserMessage(string message)
        {
            displayService.ShowUserMessage(message);
        }

        private void ShowMenu()
        {
            displayService.ShowAsciiArt();

            string[] topics = new string[]
            {
                "1. Phishing",
                "2. Password Safety",
                "3. Suspicious Links",
                "4. Privacy Settings",
                "5. Firewalls"
            };

            displayService.ShowBox(topics, Brushes.DarkCyan);
            AddBotMessage("Please select a topic to learn more by typing its name or number.");
            waitingForTopic = true;
        }

        // ==== Quiz logic ====

        private void StartQuiz()
        {
            quizQuestions = CybersecurityQuestions.GetQuizSet(); // See below for example
            quizIndex = 0;
            quizScore = 0;
            quizActive = true;
            waitingForTopic = false;
            postQuizPrompt = false;

            AddBotMessage("🧠 Starting the Cybersecurity Quiz! Let's begin:");
            AskQuizQuestion();
        }

        private void AskQuizQuestion()
        {
            if (quizIndex < quizQuestions.Length)
            {
                AddBotMessage($"Q{quizIndex + 1}: {quizQuestions[quizIndex].Question}");
                for (int i = 0; i < quizQuestions[quizIndex].Options.Length; i++)
                {
                    AddBotMessage($"{i + 1}. {quizQuestions[quizIndex].Options[i]}");
                }
            }
            else
            {
                quizActive = false;
                AddBotMessage($"Quiz completed! Your score: {quizScore} / {quizQuestions.Length}");
                AddBotMessage("Would you like to learn more? (yes/no)");
                postQuizPrompt = true;
            }
        }

        private void HandleQuizAnswer(string input)
        {
            if (int.TryParse(input, out int answer))
            {
                int correct = quizQuestions[quizIndex].CorrectOptionIndex + 1;
                if (answer == correct)
                {
                    AddBotMessage("Correct! 🎉");
                    quizScore++;
                }
                else
                {
                    AddBotMessage($"Incorrect. The correct answer was: {correct}. {quizQuestions[quizIndex].Options[quizQuestions[quizIndex].CorrectOptionIndex]}");
                }
                quizIndex++;
                AskQuizQuestion();
            }
            else
            {
                AddBotMessage("Please answer with the number corresponding to your choice.");
            }
        }

        // ==== Topic handling ====

        private void HandleTopicRequest(string input)
        {
            string lowInput = input.ToLower();
            waitingForTopic = false;

            string response = responseService.GetResponse(lowInput);
            AddBotMessage(response);
        }
    }

    // ==== Helper classes ====

    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }

    public static class CybersecurityQuestions
    {
        public static QuizQuestion[] GetQuizSet()
        {
            return new QuizQuestion[]
            {
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    Options = new string[]
                    {
                        "A type of malware",
                        "A social engineering attack to steal information",
                        "A firewall technique",
                        "An antivirus software"
                    },
                    CorrectOptionIndex = 1
                },
                new QuizQuestion
                {
                    Question = "Which of these is a strong password?",
                    Options = new string[]
                    {
                        "123456",
                        "password",
                        "P@ssw0rd!2025",
                        "qwerty"
                    },
                    CorrectOptionIndex = 2
                },
                new QuizQuestion
                {
                    Question = "What should you do when you see a suspicious link?",
                    Options = new string[]
                    {
                        "Click it immediately",
                        "Hover over it to check destination before clicking",
                        "Share it with friends",
                        "Ignore and delete the message"
                    },
                    CorrectOptionIndex = 1
                },
                // Add more questions as needed
            };
        }
    }
}
