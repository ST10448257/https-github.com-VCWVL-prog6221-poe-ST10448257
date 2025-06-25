using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    internal class CyberBot
    {
        private readonly GreetingService _greetingService;
        private readonly QuestionService _questionService;
        private readonly TopicService _topicService;
        private readonly DisplayService _displayService;
        private readonly ResponseService _responseService;
        private UserProfile _userProfile;
        private List<string> _userInquiries;

        public CyberBot()
        {
            _greetingService = new GreetingService();
            _questionService = new QuestionService();
            _topicService = new TopicService();
            _displayService = new DisplayService();
            _responseService = new ResponseService();
            _userProfile = new UserProfile();
            _userInquiries = new List<string>();
        }

        public string GetResponse(string input)
        {
            return _responseService.GetResponse(input);
        }

        public void StartChat()
        {
            _greetingService.PlayWelcomeSound();
            _displayService.DisplayAsciiArt();

            Console.Write("Please enter your name: ");
            _userProfile.Name = Console.ReadLine();

            _displayService.DisplayWelcomeMessage(_userProfile.Name);
            _questionService.AskPredefinedQuestions(_userProfile.Name);

            bool keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("\nWhat would you like to learn about? (phishing, password safety, suspicious links, privacy)");
                Console.WriteLine("Type 'exit' at any time to quit the program.");
                string userInput = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                if (userInput == "exit")
                {
                    _displayService.DisplayGoodbyeMessage(_userProfile.Name);
                    keepGoing = false;
                    continue;
                }

                if (userInput.Contains("phishing"))
                {
                    HandleTopic("phishing", _topicService.PhishingInfo);
                }
                else if (userInput.Contains("password safety"))
                {
                    HandleTopic("password safety", _topicService.PasswordInfo);
                }
                else if (userInput.Contains("suspicious links"))
                {
                    HandleTopic("suspicious links", _topicService.SuspiciousLinksInfo);
                }
                else if (userInput.Contains("privacy"))
                {
                    HandleTopic("privacy", _topicService.PrivacyInfo);
                }
                else
                {
                    _displayService.DisplayInvalidChoiceMessage();
                    continue;
                }

                ProvideContextualFollowUp();
                AskForFollowUp();

                while (true)
                {
                    Console.WriteLine("\nIs there anything else you'd like to know about cybersecurity? (yes/no)");
                    string additionalInfo = Console.ReadLine().Trim().ToLower();
                    if (additionalInfo == "yes") break;
                    else if (additionalInfo == "no")
                    {
                        Console.WriteLine("Alright! If you have any other questions in the future, feel free to reach out.");
                        _displayService.DisplayGoodbyeMessage(_userProfile.Name);
                        keepGoing = false;
                        break;
                    }
                    else Console.WriteLine("Please input 'yes' or 'no'.");
                }
            }
        }

        private void HandleTopic(string topic, Action showTopicInfo)
        {
            _userInquiries.Add(topic);
            UpdateFavoriteTopic(topic);
            showTopicInfo();
        }

        private void UpdateFavoriteTopic(string topic)
        {
            if (_userProfile.FavoriteTopic != topic)
            {
                _userProfile.FavoriteTopic = topic;
                Console.WriteLine($"Got it! I'll remember that you're interested in {topic}.");
            }
        }

        private void ProvideContextualFollowUp()
        {
            if (_userInquiries.Count > 0)
            {
                string lastInquiry = _userInquiries[^1];
                switch (lastInquiry)
                {
                    case "phishing":
                        AskFollowUp("Would you like to know about specific phishing techniques or how to recognize them?");
                        break;
                    case "password safety":
                        AskFollowUp("Are you interested in learning about password managers or creating strong passwords?");
                        break;
                    case "suspicious links":
                        AskFollowUp("Would you like tips on how to identify suspicious links or examples of common scams?");
                        break;
                    case "privacy":
                        AskFollowUp("Are you interested in learning about privacy settings on social media or general privacy tips?");
                        break;
                }
            }
        }

        private void AskFollowUp(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine().Trim().ToLower();

            while (response != "yes" && response != "no")
            {
                Console.WriteLine("Please answer with 'yes' or 'no'.");
                response = Console.ReadLine().Trim().ToLower();
            }

            if (response == "yes")
            {
                switch (question)
                {
                    case "Would you like to know about specific phishing techniques or how to recognize them?":
                        Console.WriteLine("Spear phishing attacks are targeted to individuals. Always check for urgency and spoofed email addresses.");
                        Console.WriteLine("Phishing attacks often manipulate emotions—stay cautious!");
                        break;
                    case "Are you interested in learning about password managers or creating strong passwords?":
                        Console.WriteLine("Use password managers like Bitwarden or 1Password to generate and store secure passwords.");
                        Console.WriteLine("Good passwords are long (16+ characters) and include a mix of characters.");
                        break;
                    case "Would you like tips on how to identify suspicious links or examples of common scams?":
                        Console.WriteLine("Check for unusual domains or misspelled URLs. Common scams include fake tech support and IRS calls.");
                        break;
                    case "Are you interested in learning about privacy settings on social media or general privacy tips?":
                        Console.WriteLine("Limit what others can see on your profile. Don’t overshare personal information.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("That's all for now? Feel free to reach out if you need more.");
            }
        }

        private void AskForFollowUp()
        {
            while (true)
            {
                Console.WriteLine("\nDo you have any follow-up questions or need more details? (yes/no)");
                string followUp = Console.ReadLine().Trim().ToLower();

                if (followUp == "yes")
                {
                    Console.Write("\nPlease ask your question: ");
                    string followUpQuestion = Console.ReadLine().Trim().ToLower();
                    DetectSentiment(followUpQuestion);

                    if (!string.IsNullOrEmpty(_userProfile.FavoriteTopic) && followUpQuestion.Contains(_userProfile.FavoriteTopic))
                    {
                        Console.WriteLine($"As someone interested in {_userProfile.FavoriteTopic}, here are some additional tips:");
                        ProvidePersonalizedTips();
                    }
                    else
                    {
                        HandleFollowUpQuestions(followUpQuestion);
                    }
                    break;
                }
                else if (followUp == "no")
                {
                    Console.WriteLine("Okay! If you have any other questions, feel free to ask.");
                    break;
                }
                else
                {
                    Console.WriteLine("Please input 'yes' or 'no'.");
                }
            }
        }

        private void HandleFollowUpQuestions(string question)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (question.Contains("phishing"))
            {
                Console.WriteLine("Additional phishing tips:");
                Console.WriteLine("- Verify sender emails.");
                Console.WriteLine("- Avoid generic greetings.");
                Console.WriteLine("- Don’t open unsolicited attachments.");
            }
            else if (question.Contains("password safety"))
            {
                Console.WriteLine("Additional password safety tips:");
                Console.WriteLine("- Use two-factor authentication.");
                Console.WriteLine("- Don’t reuse passwords.");
                Console.WriteLine("- Update passwords regularly.");
            }
            else if (question.Contains("suspicious links"))
            {
                Console.WriteLine("Recognizing suspicious links:");
                Console.WriteLine("- Hover over URLs.");
                Console.WriteLine("- Be cautious of short links.");
                Console.WriteLine("- Visit official sites directly.");
            }
            else if (question.Contains("privacy"))
            {
                Console.WriteLine("Privacy tips:");
                Console.WriteLine("- Check social media settings.");
                Console.WriteLine("- Avoid oversharing.");
                Console.WriteLine("- Use strong privacy options.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, I can only help with phishing, password safety, suspicious links, or privacy.");
            }

            Console.ResetColor();
        }

        private void ProvidePersonalizedTips()
        {
            switch (_userProfile.FavoriteTopic)
            {
                case "phishing":
                    Console.WriteLine("- Always verify the sender’s email.");
                    Console.WriteLine("- Watch for urgent or threatening language.");
                    Console.WriteLine("- Don’t click unknown links.");
                    break;
                case "password safety":
                    Console.WriteLine("- Use a password manager.");
                    Console.WriteLine("- Enable two-factor authentication.");
                    Console.WriteLine("- Avoid using personal info in passwords.");
                    break;
                case "suspicious links":
                    Console.WriteLine("- Hover over links to preview the destination.");
                    Console.WriteLine("- Be suspicious of shortened URLs.");
                    Console.WriteLine("- Report scam links to your IT team or provider.");
                    break;
                case "privacy":
                    Console.WriteLine("- Use encrypted messaging apps.");
                    Console.WriteLine("- Limit permissions for mobile apps.");
                    Console.WriteLine("- Disable location tracking where unnecessary.");
                    break;
            }
        }

        private void DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("anxious"))
            {
                Console.WriteLine("It’s okay to be concerned—being cautious is a good thing.");
                Console.WriteLine("Never share sensitive info unless you’re 100% sure of the source.");
            }
            else if (input.Contains("curious") || input.Contains("want to know"))
            {
                Console.WriteLine("Curiosity is the first step to awareness—let’s learn more!");
            }
            else if (input.Contains("frustrated") || input.Contains("upset") || input.Contains("angry"))
            {
                Console.WriteLine("Cybersecurity can be frustrating. Step away and return when you're ready.");
            }
        }
    }
}
