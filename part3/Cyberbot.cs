using System;

namespace CyberSecurityChatBot
{
    public class QuestionService
    {
        public void AskPredefinedQuestions(string userName)
        {
            Console.WriteLine($"\nHi {userName}, let's start with a few questions to learn more about you.\n");

            Console.Write("Have you ever encountered a phishing email before? (yes/no): ");
            string phishingExperience = Console.ReadLine().Trim().ToLower();
            RespondToExperience("phishing", phishingExperience);

            Console.Write("Do you currently use a password manager? (yes/no): ");
            string usesPasswordManager = Console.ReadLine().Trim().ToLower();
            RespondToExperience("password manager", usesPasswordManager);

            Console.Write("Do you regularly check the privacy settings on your accounts? (yes/no): ");
            string privacyCheck = Console.ReadLine().Trim().ToLower();
            RespondToExperience("privacy settings", privacyCheck);

            Console.WriteLine("\nThanks! Your answers will help me guide you better.");
        }

        private void RespondToExperience(string topic, string answer)
        {
            switch (answer)
            {
                case "yes":
                    Console.WriteLine($"Great! It’s good to know you're familiar with {topic}.");
                    break;
                case "no":
                    Console.WriteLine($"No worries. I can help you understand more about {topic}.");
                    break;
                default:
                    Console.WriteLine($"Got it. If you ever want to learn more about {topic}, just ask!");
                    break;
            }
        }
    }
}
