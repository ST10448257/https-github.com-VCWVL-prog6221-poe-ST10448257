namespace CyberSecurityChatBot
{
    public class ResponseService
    {
        public string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("phishing"))
            {
                return "Phishing is a type of scam where attackers impersonate legitimate institutions via email or messages.";
            }
            else if (input.Contains("password"))
            {
                return "Use strong, unique passwords and consider using a password manager for safety.";
            }
            else if (input.Contains("suspicious links"))
            {
                return "Never click on links you don’t recognize. Hover over them to see where they lead.";
            }
            else if (input.Contains("privacy"))
            {
                return "Protect your privacy by limiting personal info shared online and adjusting privacy settings.";
            }
            else if (input == "exit")
            {
                return "Goodbye! Stay safe online.";
            }
            else
            {
                return "I'm sorry, I can help with phishing, password safety, suspicious links, or privacy.";
            }
        }
    }
}
