using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    // QuestionService class: Handles asking and answering predefined questions
    class QuestionService
    {
        // Dictionary to hold keywords and their responses
        private readonly Dictionary<string, string> _keywordResponses = new Dictionary<string, string>
        {
            { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords." },
            { "scam", "Be cautious of unsolicited messages or emails that ask for personal information. Always verify the source before clicking on links." },
            { "privacy", "Protect your privacy by adjusting your social media settings and being mindful of the information you share online." }
        };

        // AskPredefinedQuestions method: Asks the user predefined questions and provides answers
        public void AskPredefinedQuestions(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nHello {userName}! How are you today?");
            Console.ResetColor();

            Console.Write("Type a response (or just press Enter to skip): ");
            string response = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("\nOkay! Let's move on.");
            }
            else
            {
                Console.WriteLine("\nThat's great to hear! I'm here to help with any cybersecurity questions you may have.");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nBefore we begin {userName}, you can ask me about cybersecurity topics like password safety, scams, or privacy.");
            Console.WriteLine("Or just press Enter to skip.\n");
            Console.ResetColor();

            Console.Write("Ask me a question: ");
            string question = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrEmpty(question))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOkay! Let's jump straight into it!");
                Console.ResetColor();
                return;
            }

            // Check for keywords in the user's question
            bool keywordFound = false;

            foreach (var keyword in _keywordResponses.Keys)
            {
                if (question.Contains(keyword))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(_keywordResponses[keyword]);
                    Console.ResetColor();
                    keywordFound = true;
                    return; // Exit after responding to first matched keyword
                }
            }

            // Default response if no keywords matched
            if (!keywordFound)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Hmm... I can only provide information on password safety, scams, and privacy for now. Sorry {userName} :(");
                Console.ResetColor();
            }
        }
    }
}