using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    public static class CybersecurityQuestions
    {
        public static List<QuizQuestion> GetQuizSet()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new string[] { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    2,
                    "Reporting phishing emails helps prevent scams.",
                    "Phishing"),

                new QuizQuestion(
                    "Which of these is the best practice for password creation?",
                    new string[] { "Use the same password everywhere", "Use personal information", "Use a long, random combination", "Write it on a sticky note" },
                    2,
                    "Long, random combinations are hardest to guess or crack.",
                    "Password Safety"),

                new QuizQuestion(
                    "True or False: You should always click on links in emails from unknown senders.",
                    new string[] { "True", "False" },
                    1,
                    "Links in emails from unknown senders may lead to malicious websites.",
                    "Safe Browsing"),

                new QuizQuestion(
                    "What is social engineering?",
                    new string[] { "A type of construction work", "Manipulating people to give up confidential information", "A programming technique", "A data analysis method" },
                    1,
                    "Social engineering exploits human psychology rather than technical hacking.",
                    "Social Engineering"),

                new QuizQuestion(
                    "When using public Wi-Fi, you should:",
                    new string[] { "Do online banking", "Access sensitive accounts", "Use a VPN", "Share personal information" },
                    2,
                    "VPNs encrypt your connection on public networks.",
                    "Safe Browsing"),

                new QuizQuestion(
                    "What does HTTPS in a website URL indicate?",
                    new string[] { "The site is colorful", "The connection is encrypted", "The site is free", "The site is fast" },
                    1,
                    "HTTPS provides secure communication over a computer network.",
                    "Safe Browsing"),

                new QuizQuestion(
                    "True or False: You should update your software regularly.",
                    new string[] { "True", "False" },
                    0,
                    "Updates often include security patches for vulnerabilities.",
                    "General Security"),

                new QuizQuestion(
                    "What is two-factor authentication?",
                    new string[] { "Using two passwords", "Verifying identity with two different methods", "Logging in twice", "Having two accounts" },
                    1,
                    "Two factors provide an extra layer of security beyond just a password.",
                    "Authentication"),

                new QuizQuestion(
                    "What should you do if you suspect your account has been hacked?",
                    new string[] { "Do nothing", "Change your password immediately", "Share the news on social media", "Create another account" },
                    1,
                    "Changing your password can stop the attacker's access.",
                    "Account Security"),

                new QuizQuestion(
                    "Why should you be careful about what you post on social media?",
                    new string[] { "It might bore people", "Attackers can use it for social engineering", "It uses too much data", "It's not important" },
                    1,
                    "Personal information can help attackers guess passwords or answer security questions.",
                    "Social Engineering")
            };
        }
    }
}