using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberSecurityChatBot
{
    public class TopicService
    {
        private readonly Random _random = new();

        // Track last shown index to prevent repeat tips
        private int _lastPhishingTipIndex = -1;
        private int _lastPasswordTipIndex = -1;
        private int _lastSuspiciousLinkTipIndex = -1;
        private int _lastPrivacyTipIndex = -1;

        // Main method to show tip in UI
        private void ShowTip(TextBlock titleBlock, TextBlock tipBlock, string title, string[] tips, ref int lastIndex, Brush titleColor)
        {
            int index;
            do
            {
                index = _random.Next(tips.Length);
            } while (index == lastIndex);

            lastIndex = index;

            titleBlock.Text = title;
            titleBlock.Foreground = titleColor;
            tipBlock.Text = tips[index];
        }

        public void ShowPhishingTip(TextBlock titleBlock, TextBlock tipBlock)
        {
            string[] tips = {
                "Be cautious of emails asking for personal details. Scammers often disguise themselves as trusted organizations.",
                "Always check the sender's email address before clicking on any links.",
                "Do not click on links in unsolicited emails. Instead, visit the website directly.",
                "Look for spelling errors in the email, as many phishing attempts contain mistakes."
            };

            ShowTip(titleBlock, tipBlock, "Phishing Emails", tips, ref _lastPhishingTipIndex, Brushes.Green);
        }

        public void ShowPasswordTip(TextBlock titleBlock, TextBlock tipBlock)
        {
            string[] tips = {
                "Using strong, unique passwords helps protect your accounts.",
                "Use a mix of uppercase, lowercase, numbers, and symbols.",
                "Avoid using the same password on multiple websites.",
                "Change your passwords regularly."
            };

            ShowTip(titleBlock, tipBlock, "Safe Password Practices", tips, ref _lastPasswordTipIndex, Brushes.Blue);
        }

        public void ShowSuspiciousLinkTip(TextBlock titleBlock, TextBlock tipBlock)
        {
            string[] tips = {
                "Cybercriminals often use links to infect your device or steal your information.",
                "Hover over links to check the real URL before clicking.",
                "Don’t click links from unknown or unexpected sources.",
                "Be cautious with shortened URLs like bit.ly or tinyurl."
            };

            ShowTip(titleBlock, tipBlock, "Suspicious Links", tips, ref _lastSuspiciousLinkTipIndex, Brushes.Magenta);
        }

        public void ShowPrivacyTip(TextBlock titleBlock, TextBlock tipBlock)
        {
            string[] tips = {
                "Change settings on social media platforms to control what others see.",
                "Use a VPN to encrypt your internet connection and protect your data.",
                "Be cautious about clicking links or downloading files from suspicious sources.",
                "Avoid using the same password across multiple sites."
            };

            ShowTip(titleBlock, tipBlock, "Privacy Tips", tips, ref _lastPrivacyTipIndex, Brushes.DarkCyan);
        }
    }
}
