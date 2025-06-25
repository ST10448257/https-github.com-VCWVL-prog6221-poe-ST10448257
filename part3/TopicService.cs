using System;

namespace CyberSecurityChatBot
{
    // TopicService class: Handles displaying information about cybersecurity topics
    class TopicService
    {
        private readonly DisplayService _displayService;
        private readonly Random _random;

        // Fields to track last displayed tip index for each topic
        private int _lastPhishingTipIndex = -1;
        private int _lastPasswordTipIndex = -1;
        private int _lastSuspiciousLinkTipIndex = -1;
        private int _lastPrivacyTipIndex = -1;

        public TopicService()
        {
            _displayService = new DisplayService();
            _random = new Random(); // Instantiate once to avoid issues with multiple Random() calls
        }

        public void PhishingInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Phishing Emails");

            string[] tips = {
                "Be cautious of emails asking for personal details. Scammers often disguise themselves as trusted organizations.",
                "Always check the sender's email address before clicking on any links.",
                "Do not click on links in unsolicited emails. Instead, visit the website directly.",
                "Look for spelling errors in the email, as many phishing attempts contain mistakes."
            };

            int index;
            do
            {
                index = _random.Next(tips.Length);
            } while (index == _lastPhishingTipIndex);

            _lastPhishingTipIndex = index;

            _displayService.DisplayTipsWithBox(new string[] { tips[index] }, ConsoleColor.Green);
        }

        public void PasswordInfo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Safe Password Practices");

            string[] tips = {
                "Using strong, unique passwords helps protect your accounts.",
                "Use a mix of uppercase, lowercase, numbers, and symbols.",
                "Avoid using the same password on multiple websites.",
                "Change your passwords regularly."
            };

            int index;
            do
            {
                index = _random.Next(tips.Length);
            } while (index == _lastPasswordTipIndex);

            _lastPasswordTipIndex = index;

            _displayService.DisplayTipsWithBox(new string[] { tips[index] }, ConsoleColor.Blue);
        }

        public void SuspiciousLinksInfo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Recognising Suspicious Links");

            string[] tips = {
                "Cybercriminals often use links to infect your device or steal your information.",
                "Hover over links to check the real URL before clicking.",
                "Don’t click links from unknown or unexpected sources.",
                "Be cautious with shortened URLs like bit.ly or tinyurl."
            };

            int index;
            do
            {
                index = _random.Next(tips.Length);
            } while (index == _lastSuspiciousLinkTipIndex);

            _lastSuspiciousLinkTipIndex = index;

            _displayService.DisplayTipsWithBox(new string[] { tips[index] }, ConsoleColor.Magenta);
        }

        public void PrivacyInfo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Privacy Tips");

            string[] tips = {
                "Change settings on social media platforms and other apps to control what information is collected about you and who can see your profile.",
                "Use a VPN to encrypt your internet connection, making it more difficult to intercept your data.",
                "Be cautious about clicking links or downloading files from unfamiliar or suspicious sources.",
                "Avoid using the same password across multiple sites, as this increases your risk if one account is compromised."
            };

            int index;
            do
            {
                index = _random.Next(tips.Length);
            } while (index == _lastPrivacyTipIndex);

            _lastPrivacyTipIndex = index;

            _displayService.DisplayTipsWithBox(new string[] { tips[index] }, ConsoleColor.Blue);
        }
    }
}